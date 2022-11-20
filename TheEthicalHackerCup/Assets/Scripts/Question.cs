using System;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Linq;

namespace Learning {


    /// <summary>
    /// Question observer observes a question
    /// </summary>
    /// <typeparam name="State"></typeparam>
    public interface QuestionObserver{
        public void notify(QuestionState state);
        public void submitted(bool correct);
    }

    /// <summary>
    /// Question Event Class is used to send events to the Question State Machine Object
    /// </summary>
    public class QuestionEvent { }

    /// <summary>
    /// The SelectEvent is used to tell the question state machine that one of its options has been selected
    /// </summary>
    public class SelectEvent : QuestionEvent {
        public SelectEvent(int selection) {
            this.Selection = selection;
        }
        public int Selection { get; }
    }

    /// <summary>
    /// The start event is used to tell the question state machien that the question is just starting up
    /// </summary>
    public class StartEvent : QuestionEvent { }

    /// <summary>
    /// The submit event is used to tell the question that the UI is trying to submit the question
    /// </summary>
    public class SubmitEvent : QuestionEvent { }


    /// <summary>
    /// The question is a state machine used to take in Events and dispatch actions.
    /// It follows both an observable pattern and a state machine pattern
    /// </summary>
    /// <typeparam name="State">The type of state it is machining</typeparam>
    public abstract class Question {
        /// <summary>
        /// Constructs a question with a given state
        /// </summary>
        /// <param name="state">the state to ve machined</param>
        public Question(QuestionState state) {
            this.state = state;
            this.observers = new List<QuestionObserver>();

        }
        private QuestionState state {get;}
        private List<QuestionObserver> observers;


        /// <summary>
        /// Adds an observer
        /// </summary>
        /// <param name="ql">The question observer</param>
        public void addQuestionObserver(QuestionObserver ql) {
            observers.Add(ql);
        }
        /// <summary>
        /// removes an observer
        /// </summary>
        /// <param name="ql">The question observer</param>
        public void removeQuestionObserver(QuestionObserver ql) {
            observers.Remove(ql);
        }

        /// <summary>
        /// This is used to dispatch an event to the state machine
        /// </summary>
        /// <param name="ev">the event</param>
        public void invokeQuestionEvent(QuestionEvent ev) {
            if (ev is StartEvent) {
                this.notifyObservers();
            }
            else if (ev is SubmitEvent) {
                this.submitted();
            }
            else {
                this.onReceiveQuestionEvent(ev);
            }

        }

        /// <summary>
        /// This method is to be used by subclasses to handle question specific events and question specific ways
        /// </summary>
        /// <param name="ev"></param>
        protected abstract void onReceiveQuestionEvent(QuestionEvent ev);


        /// <summary>
        /// Notifies all obsevers of an internal state change
        /// </summary>
        protected void notifyObservers() {
            foreach (var observer in this.observers) {
                observer.notify(this.state);
            }
        }

        /// <summary>
        /// Notifies all observers that the question has been submitted
        /// </summary>
        private void submitted() {
            foreach (var observer in this.observers) {
                observer.submitted(this.state.isCorrect());
            }
        }

    }

    /// <summary>
    /// A question factory is used to generate Question State Machines based on question states
    /// </summary>
    public class QuestionFactory {

        /// <summary>
        /// Generates a question state machine when given a Plain Old C# Object
        /// </summary>
        /// <typeparam name="T">The type of state to monitor</typeparam>
        /// <param name="state">The instance of the state to monitor</param>
        /// <returns>Returns a new state machine for the state</returns>
        /// <exception cref="ArgumentException">Thrown when the user tries to make a state machine for a state which is not implemented into this function yet</exception>
        public static Question generate(QuestionState state){
            if (state is CheckboxState)
            {
                CheckboxState arg = (CheckboxState)state;
                return new CheckboxQuestion(arg);
            }
            else if (state is RadioState)
            {
                RadioState arg = (RadioState)state;
                return new RadioQuestion(arg);
            }
            else {
                throw new ArgumentException("Question type not implemented yet");
            }
        }
    }


    /// <summary>
    /// The Question State is the superclass of all Questions states
    /// </summary>
    public abstract class QuestionState{
        /// <summary>
        /// Constructs a Question with Sample Text as its name
        /// </summary>
        public QuestionState(){
            this.Name = "Sample Text";
        }
        /// <summary>
        /// Constructs a QuestionState where the name can be specified
        /// </summary>
        /// <param name="name"></param>
        public QuestionState(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The name/title of the Question being shown
        /// </summary>
        public string Name;
        public abstract bool isCorrect();

        public virtual XElement toXml(){
            var root = new XElement(
                "Question",
                new XAttribute("Name", this.Name),
                new XAttribute("Type", this.GetType())
            );
            attrToXml(ref root);
            return root;
        }

        protected abstract void attrToXml(ref XElement root);

        public static QuestionState fromXml(XElement element){
            QuestionState state;
            if(element.Attribute("Type").Value==typeof(CheckboxState).ToString()){
                state = CheckboxState.fromXml(element);
            }
            else if(element.Attribute("Type").Value == typeof(RadioState).ToString()){
                state = RadioState.fromXml(element);
            }
            else{
                throw new ArgumentException("Question type not added to factory yet");
            }
            state.Name = element.Attribute("Name").Value;
            return state;
        }
    }

    /// <summary>
    /// The checkbox state used for checkbox questions
    /// </summary>
    public class CheckboxState: QuestionState {
        public CheckboxState(){
            this.Selected = new HashSet<int>();
            this.Options = new List<string>();
            this.CorrectOptions = new HashSet<int>();
        }
        public CheckboxState(ISet<int> selected, IList<string> options, ISet<int> correctOptions)
        {
            Selected = selected;
            Options = options;
            CorrectOptions = correctOptions;
        }

        public ISet<int> Selected;
        public IList<string> Options;
        public ISet<int> CorrectOptions;

        public override bool isCorrect(){
            return Selected.SetEquals(CorrectOptions);
        }
        protected override void attrToXml(ref XElement root){
            foreach(var selection in Selected){
                root.Add(new XElement("Selection",selection));
            }
            foreach(var correct in CorrectOptions){
                root.Add(new XElement("Correct", correct));
            }
            foreach(var option in Options){
                root.Add(new XElement("Option", option));
            }

        }

        public static new CheckboxState fromXml(XElement element){
            CheckboxState state = new CheckboxState();
            foreach(var selectElement in element.Elements("Selection")){
                var selection = int.Parse(selectElement.Value);
                state.Selected.Add(selection);
            }
            foreach(var correctElement in element.Elements("Correct")){
                var correct = int.Parse(correctElement.Value);
                state.CorrectOptions.Add(correct);
            }
            foreach(var optionElement in element.Elements("Option")){
                var option = optionElement.Value;
                state.Options.Add(option);
            }
            return state;
        }
    }

    public class RadioState: QuestionState {

        public const int NONE_SELECTED = -1;
        public RadioState(){
            Selected = NONE_SELECTED;
            Options = new List<string>();
            CorrectOption = NONE_SELECTED;
        }
        public RadioState(string name, int selected, IList<string> options, int correctOption){
            this.Name = name;
            this.Selected = selected;
            this.Options = options;
            this.CorrectOption = correctOption;
        }

        public int Selected;
        public IList<string> Options;
        public int CorrectOption;
        public override bool isCorrect()
        {
            return this.Selected == CorrectOption;
        }
        protected override void attrToXml(ref XElement root){
            root.Add(new XElement("Selected",this.Selected));
            root.Add(new XElement("Correct", this.CorrectOption));
            foreach(var option in Options){
                root.Add(new XElement("Option", option));
            }

        }

        public static new RadioState fromXml(XElement radioElement){
            var state = new RadioState();
            foreach(XElement optionElement in radioElement.Elements("Option")){
                var option= optionElement.Value;
                state.Options.Add(option);
            }
            foreach(XElement selectedElement in radioElement.Elements("Selected")){
                var selected = int.Parse(selectedElement.Value);
                state.Selected = selected;
            }
            foreach(XElement correctElement in radioElement.Elements("Correct")){
                var correct = int.Parse(correctElement.Value);
                state.CorrectOption = correct;   
            }
            return state;
        }
    }

    public class CheckboxQuestion: Question {
        private CheckboxState state;
        public CheckboxQuestion(CheckboxState state):
            base(state) {
                this.state = state;
            }
        protected override void onReceiveQuestionEvent(QuestionEvent ev)
        {
            if(ev is SelectEvent){
                this.selection((SelectEvent)ev );
            }
        }

        private void selection(SelectEvent e){

            if(this.state.Selected.Contains(e.Selection)){
                this.state.Selected.Remove(e.Selection);
            }
            else{
                this.state.Selected.Add(e.Selection);
            }
            this.notifyObservers();
        }
    }

    public class RadioQuestion: Question {
        private RadioState state;
        public RadioQuestion(RadioState radioState):
            base(radioState){
                this.state = radioState;

        }
        protected override void onReceiveQuestionEvent(QuestionEvent ev){
            if(ev is SelectEvent){
                this.selection((SelectEvent)ev);            
            }
        }

        private void selection(SelectEvent e){
            if(e.Selection != ((RadioState)this.state).Selected) {
                this.state.Selected = e.Selection;
                this.notifyObservers();
            }
        }
    }


}