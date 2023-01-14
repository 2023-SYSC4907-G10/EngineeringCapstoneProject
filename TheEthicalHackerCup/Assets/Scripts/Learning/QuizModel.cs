using System.Collections.Generic;
using System.Xml.Linq;
using System;

namespace Learning
{

    public class NextSlideEvent : EventArgs
    {
        public Slide slide;
        public NextSlideEvent(Slide slide)
        {
            this.slide = slide;
        }
    }

    public class QuizSubmittedEvent : EventArgs
    {
        public readonly float percentScore;
        public readonly bool pass;
        public QuizSubmittedEvent(float score, bool pass)
        {
            this.percentScore = score;
            this.pass = pass;
        }

    }

    public class QuizClosedEvent : EventArgs
    {
        public readonly bool pass;
        public QuizClosedEvent(bool pass)
        {
            this.pass = pass;
        }
    }



    public class QuizModel
    {
        private Quiz state;
        public event EventHandler<NextSlideEvent> SlideChanged;
        public event EventHandler<QuizClosedEvent> QuizClosed;
        public event EventHandler<QuizSubmittedEvent> QuizSubmitted;

        public QuizModel(Quiz state)
        {
            this.state = state;
        }

        public void start()
        {
            var slide = this.state.Slides[this.state.CurrentSlideIndex];
            var nextQuestionEvent = new NextSlideEvent(slide);
            this.SlideChanged.Invoke(this, nextQuestionEvent);
        }

        public void next()
        {
            var prevState = this.state.CurrentStage;
            switch (prevState)
            {
                case Quiz.Stage.SLIDES:
                    this.handleNextSlide();
                    break;
                case Quiz.Stage.RESULT:
                    handleEndOfQuiz();
                    break;
            }

        }

        private void handleNextSlide()
        {
            this.state.CurrentSlideIndex++;
            if (this.state.IsNoMoreQuestions)
            {
                //update state
                this.state.CurrentStage = Quiz.Stage.RESULT;
                //send event to all views
                var submittedEvent = new QuizSubmittedEvent(this.state.Percent, this.state.IsPassingScore);
                this.QuizSubmitted.Invoke(this, submittedEvent);
            }
            else
            {
                //goto the next question
                this.state.CurrentStage = Quiz.Stage.SLIDES;

                //tell views about new Question Model
                var slide = this.state.Slides[this.state.CurrentSlideIndex];
                var nextQuestionEvent = new NextSlideEvent(slide);
                this.SlideChanged.Invoke(this, nextQuestionEvent);
            }

        }

        private void handleEndOfQuiz()
        {
            //let observers know that the quiz is done and if its passing or failing
            var quizClosedEvent = new QuizClosedEvent(this.state.IsPassingScore);
            this.QuizClosed.Invoke(this, quizClosedEvent);
        }

    }



    public class Quiz
    {
        private const float PASSING_PERCENT = 50;
        private const int STARTING_SLIDE = 0;
        public const string TAG_QUIZ = "Quiz";
        public enum Stage
        {
            SLIDES,
            RESULT
        };

        public Stage CurrentStage { get; set; }
        public IList<Slide> Slides { get; set; }
        public int CurrentSlideIndex { get; set; }
        public int Score { get { return calculateScore(); } }
        public int MaxScore { get; private set; }

        public float Percent { get { return MaxScore == 0 ? 100 : (float)Score * 100 / (float)MaxScore; } }
        public bool IsPassingScore { get { return Percent >= PASSING_PERCENT; } }

        public bool IsNoMoreQuestions { get { return CurrentSlideIndex == Slides.Count; } }

        public Quiz(IList<Slide> slides)
        {
            if (slides.Count == 0) { throw new Exception("Empty Quizzes are useless"); }
            this.CurrentStage = Stage.SLIDES;
            this.Slides = slides;
            this.CurrentSlideIndex = STARTING_SLIDE;
            this.MaxScore = calculateMaxScore();

            
        }

        private int calculateMaxScore() 
        {
            var count = 0;
            foreach (var slide in this.Slides)
            {
                if (slide.Content is IQuestionState) { count++; }
            }
            return count;
        }
        private int calculateScore() {
            var score = 0;
            foreach (var slide in this.Slides)
            {
                if (slide.Content is IQuestionState)
                {
                    var question = slide.Content as IQuestionState;
                    if (question.IsCorrect())
                    {
                        score++;
                    }
                }
            }
            return score;
        }

        public XDocument ToXml()
        {
            var root = new XElement(TAG_QUIZ);
            foreach (var slide in this.Slides)
            {
                root.Add(slide.toXml());
            }
            var document = new XDocument();
            document.Add(root);
            return document;
        }

        public static Quiz FromXml(string xmlData)
        {
            var document = XDocument.Parse(xmlData);
            var root = document.Root;
            var slides = new List<Slide>();
            foreach (var element in root.Elements(Slide.TAG_SLIDE))
            {
                slides.Add(Slide.FromXml(element));
            }

            return new Quiz(slides);

        }
    }

}