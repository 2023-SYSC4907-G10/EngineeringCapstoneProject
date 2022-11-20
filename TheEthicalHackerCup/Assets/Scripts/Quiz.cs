using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Linq;
namespace Learning{

    public class QuizEvent{}

    public class StartQuizEvent : QuizEvent{}
    public class SubmitQuizEvent: QuizEvent{}

    public class NextQuestionEvent: QuizEvent{
        public readonly bool correct; 
        public NextQuestionEvent(bool correct){
            this.correct = correct;
        }
    }

    public interface QuizObserver{
        public void notify(QuizState state);
        public void quizEnded(bool success);
    }

    public class Quiz{
        public QuizState state {get; set;}
        private IList<QuizObserver> observers;
        public IList<Question> questions {get; private set;}
        public Quiz(QuizState state){
            this.state = state;
            this.questions = new List<Question>();
            foreach (var question in this.state.questions) {
                var q = QuestionFactory.generate(question);
                q.addQuestionObserver(new QuestionListener(this));
                questions.Add(q);

            }
            this.observers = new List<QuizObserver>();
        }

        public void invokeQuizEvent(QuizEvent ev){
            if(ev is StartQuizEvent){
                quizStarted();
            }
            else if(ev is SubmitQuizEvent){
                quizSubmitted();
            }
            else if(ev is NextQuestionEvent){
                nextQuestion((NextQuestionEvent)ev);
            }
        }

        public void addObserver(QuizObserver observer){
            this.observers.Add(observer);
        }
        public void removeObserver(QuizObserver observer){
            this.observers.Remove(observer);
        }

        private void quizStarted(){
            this.questions[this.state.CurrentQuestion].invokeQuestionEvent(new StartEvent());
            notifyObservers();
        }

        private void quizSubmitted(){
            sendEndedToObservers();
        }

        /// <summary>
        /// Next Question called event.
        /// It will increment the players score if the previous question was right and it will invoke the submit quiz event on itself if it ran out of questions.
        /// </summary>
        /// <param name="ev">The triggering event</param>
        private void nextQuestion(NextQuestionEvent ev){
            this.state.CurrentQuestion++;
            if (ev.correct) {
                this.state.Score++;
            }

            if (this.state.IsNoMoreQuestions){
                this.invokeQuizEvent(new SubmitQuizEvent());
            }else{
                this.questions[this.state.CurrentQuestion].invokeQuestionEvent(new StartEvent());
            }
        }

        private void notifyObservers(){
            foreach(var observer in observers){
                observer.notify(this.state);
            }
        }

        private void sendEndedToObservers(){
           foreach(var obsever in observers){
                obsever.quizEnded(this.state.IsPassingScore);
           } 
        }


        private class QuestionListener : QuestionObserver
        {
            private Quiz quiz;
            public QuestionListener(Quiz quiz) {
                this.quiz = quiz;
            }

            public void notify(QuestionState state)
            {
                //ignore
            }

            public void submitted(bool correct)
            {
                this.quiz.invokeQuizEvent(new NextQuestionEvent(correct));
            }
        }


    }

    public class QuizState {
        
        public string Name;
        public IList<QuestionState> questions;
        public int CurrentQuestion {get; set;}
        public int Score {get; set;}
        public int MaxScore {get {return questions.Count;} }

        public float Percent {get {return MaxScore==0?100:(float)Score*100/(float)MaxScore;}}
        public bool IsPassingScore { get {return Percent>=50;}}

        public bool IsNoMoreQuestions {get {return CurrentQuestion == questions.Count;}}

        public QuizState(string name, IList<QuestionState> questions){
            this.Name = name;
            this.questions = questions;
            this.Score = 0;
            this.CurrentQuestion = 0;
        }
        
        public QuizState(string name, IList<QuestionState> questions, int currentQuestion, int score)
        {
            this.Name = name;
            this.questions = questions;
            this.CurrentQuestion = currentQuestion;
            this.Score = score;
        }

        public XDocument toXml(){
            var root = new XElement("Quiz", new XAttribute("Name",this.Name));
            foreach(var question in questions){
                root.Add(question.toXml());
            }
            var document = new XDocument();
            document.Add(root);
            return document;
        }

        public static QuizState fromXml(string xmlData){
            var document = XDocument.Parse(xmlData);
            var root = document.Root;
            var name = root.Attribute("Name");
            var questions = new List<QuestionState>();
            foreach(var element in root.Elements("Question")){
                questions.Add(QuestionState.fromXml(element));
            }
            
            return new QuizState(name.Value, questions);

        }
    }

}