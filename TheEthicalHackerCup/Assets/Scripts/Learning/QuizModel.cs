using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System;
namespace Learning
{

    public class NextQuestionEvent : EventArgs
    {
        public QuestionModel question;
        public NextQuestionEvent(QuestionModel question)
        {
            this.question = question;
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

    public class QuizStartedEvent : EventArgs
    {
        public readonly string quizName;
        public QuizStartedEvent(string quizName)
        {
            this.quizName = quizName;
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
        private QuizState state;
        public event EventHandler<QuizStartedEvent> QuizStarted;
        public event EventHandler<NextQuestionEvent> QuestionChanged;
        public event EventHandler<QuizClosedEvent> QuizClosed;
        public event EventHandler<QuizSubmittedEvent> QuizSubmitted;

        public QuizModel(QuizState state)
        {
            this.state = state;
        }

        public void start()
        {
            this.QuizStarted.Invoke(this, new QuizStartedEvent(this.state.Name));
        }

        public void next()
        {
            var prevState = this.state.currentStage;
            switch (prevState)
            {
                case QuizState.Stage.INTRODUCTION:
                    this.handleNextQuestion();
                    break;
                case QuizState.Stage.QUESTIONS:
                    this.handleNextQuestion();
                    break;
                case QuizState.Stage.RESULT:
                    handleEndOfQuiz();
                    break;
            }

        }

        private void handleNextQuestion()
        {
            this.state.CurrentQuestion++;
            if (this.state.IsNoMoreQuestions)
            {
                //update state
                this.state.currentStage = QuizState.Stage.RESULT;
                this.state.updateScore();
                //send event to all views
                var submittedEvent = new QuizSubmittedEvent(this.state.Percent, this.state.IsPassingScore);
                this.QuizSubmitted.Invoke(this, submittedEvent);
            }
            else
            {
                //goto the next question
                this.state.currentStage = QuizState.Stage.QUESTIONS;

                //tell views about new Question Model
                var questionState = this.state.questions[this.state.CurrentQuestion];
                var questionModel = QuestionFactory.generate(questionState);
                var nextQuestionEvent = new NextQuestionEvent(questionModel);
                this.QuestionChanged.Invoke(this, nextQuestionEvent);
            }

        }

        private void handleEndOfQuiz()
        {
            //let observers know that the quiz is done and if its passing or failing
            var quizClosedEvent = new QuizClosedEvent(this.state.IsPassingScore);
            this.QuizClosed.Invoke(this, quizClosedEvent);
        }

    }



    public class QuizState
    {

        public enum Stage
        {
            INTRODUCTION,
            QUESTIONS,
            RESULT
        };

        public string Name { get; set; }
        public Stage currentStage { get; set; }
        public IList<QuestionState> questions;
        public int CurrentQuestion { get; set; }
        public int Score { get; private set; }
        public int MaxScore { get { return questions.Count; } }

        public float Percent { get { return MaxScore == 0 ? 100 : (float)Score * 100 / (float)MaxScore; } }
        public bool IsPassingScore { get { return Percent >= 50; } }

        public bool IsNoMoreQuestions { get { return CurrentQuestion == questions.Count; } }

        public QuizState(string name, IList<QuestionState> questions)
        {
            this.currentStage = Stage.INTRODUCTION;
            this.Name = name;
            this.questions = questions;
            this.Score = 0;
            this.CurrentQuestion = -1;
        }

        public QuizState(string name, IList<QuestionState> questions, Stage startingStage, int currentQuestion, int score)
        {
            this.currentStage = startingStage;
            this.currentStage = Stage.INTRODUCTION;
            this.Name = name;
            this.questions = questions;
            this.CurrentQuestion = currentQuestion;
            this.Score = score;
        }

        public void updateScore()
        {
            this.Score = 0;
            foreach (var question in this.questions)
            {
                if (question.isCorrect())
                {
                    this.Score++;
                }
            }
        }

        public XDocument toXml()
        {
            var root = new XElement("Quiz", new XAttribute("Name", this.Name));
            foreach (var question in questions)
            {
                root.Add(question.toXml());
            }
            var document = new XDocument();
            document.Add(root);
            return document;
        }

        public static QuizState fromXml(string xmlData)
        {
            var document = XDocument.Parse(xmlData);
            var root = document.Root;
            var name = root.Attribute("Name");
            var questions = new List<QuestionState>();
            foreach (var element in root.Elements("Question"))
            {
                questions.Add(QuestionState.fromXml(element));
            }

            return new QuizState(name.Value, questions);

        }
    }

}