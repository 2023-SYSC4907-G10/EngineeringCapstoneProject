using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Learning
{

    public class QuestionStateUpdatedEvent : EventArgs
    {
        public IQuestionState QuestionState { get; private set; }
        public QuestionStateUpdatedEvent(IQuestionState questionState)
        {
            QuestionState = questionState;
        }
    }

    /// <summary>
    /// Question Event Class is used to send events to the Question State Machine Object
    /// </summary>
    public class QuestionEvent { }

    /// <summary>
    /// Question Event Class is used to send events to the Question State Machine Object
    /// </summary>
    public class StartEvent : QuestionEvent { }


    /// <summary>
    /// The SelectEvent is used to tell the question state machine that one of its options has been selected
    /// </summary>
    public class SelectEvent : QuestionEvent
    {
        public SelectEvent(int selection)
        {
            this.Selection = selection;
        }
        public int Selection { get; }
    }

    /// <summary>
    /// The question is a state machine used to take in Events and dispatch actions.
    /// It follows both an observable pattern and a state machine pattern
    /// </summary>
    /// <typeparam name="State">The type of state it is machining</typeparam>
    public abstract class QuestionModel
    {

        public event EventHandler<QuestionStateUpdatedEvent> QuestionStateUpdated;

        /// <summary>
        /// Constructs a question with a given state
        /// </summary>
        /// <param name="state">the state to ve machined</param>
        public QuestionModel(IQuestionState state)
        {
            this.state = state;

        }
        private IQuestionState state { get; }

        protected void invokeStateUpdated()
        {
            this.QuestionStateUpdated.Invoke(this, new QuestionStateUpdatedEvent(this.state));
        }


        /// <summary>
        /// This is used to dispatch an event to the state machine
        /// </summary>
        /// <param name="ev">the event</param>
        public void invokeQuestionEvent(QuestionEvent ev)
        {
            if (ev is StartEvent)
            {
                invokeStateUpdated();
            }
            else
            {
                onReceiveQuestionEvent(ev);
            }

        }

        /// <summary>
        /// This method is to be used by subclasses to handle question specific events and question specific ways
        /// </summary>
        /// <param name="ev"></param>
        protected abstract void onReceiveQuestionEvent(QuestionEvent ev);


        public static QuestionModel GenerateModel(IQuestionState question) 
        {
            if (question is ISelectQuestionState) {
                return new SelectionModel((ISelectQuestionState)question);
            }
            throw new Exception("Question not recognized");
        }
    }

    /// <summary>
    /// Content Interface which defines what can go on a slide
    /// </summary>
    public interface IContent {
        public const string TAG_TYPE = "Type", TAG_CONTENT = "Content";
        public abstract XElement toXml();

        public static IContent fromXml(XElement element)
        {
            IContent state;
            if (element.Attribute(TAG_TYPE).Value == typeof(CheckBox).ToString())
            {
                state = CheckBox.FromXml(element);
            }
            else if (element.Attribute(TAG_TYPE).Value == typeof(RadioBox).ToString())
            {
                state = RadioBox.FromXml(element);
            }
            else if (element.Attribute(TAG_TYPE).Value == typeof(InfoContent).ToString())
            {
                state = InfoContent.FromXml(element);
            }
            else if (element.Attribute(TAG_TYPE).Value == typeof(ImageContent).ToString()) 
            {
                state = ImageContent.FromXml(element);
            }
            else if (element.Attribute(TAG_TYPE).Value == typeof(VideoContent).ToString())
            {
                state = VideoContent.FromXml(element);
            }
            else
            {
                throw new ArgumentException("Content type not added to factory yet");
            }
            return state;
        }
    }

    /// <summary>
    /// Each page in the quiz is refered to as a slide. It contains slide content and a name
    /// </summary>
    public class Slide
    {
        private const int MAX_NAME_SIZE = 340;
        public const string TAG_NAME = "Name", TAG_SLIDE = "Slide";

        public string Name { get; }
        public IContent Content { get; }

        public Slide(string name, IContent content) {
            Name = name;
            Content = content;
            if (Name.Length > MAX_NAME_SIZE) {
                throw new Exception("Slide text too long: " + this.Name);
            }
        }
        public virtual XElement toXml()
        {
            var root = new XElement(
                TAG_SLIDE,
                new XAttribute(TAG_NAME, this.Name)
            );
            root.Add(Content.toXml());
            return root;
        }

        public static Slide FromXml(XElement element)
        {
            var name = element.Attribute(TAG_NAME).Value;
            var content = IContent.fromXml(element.Element(IContent.TAG_CONTENT));
            Slide state = new Slide(name, content);
            return state;
        }
    }

    /// <summary>
    /// The Question State is the superclass of all Questions states
    /// </summary>
    public interface IQuestionState : IContent
    {
        public bool IsCorrect();
    }

    /// <summary>
    /// Added interface for easy reusability for questions where the user must select an option of many
    /// </summary>
    public interface ISelectQuestionState : IQuestionState
    {
        IList<string> GetOptions();
        void Select(int choice);
        bool isSelected(int choice);
        bool isCorrect(int choice);
    }

    /// <summary>
    /// The checkbox state used for multiple select questions
    /// </summary>
    public class CheckBox : ISelectQuestionState
    {
        private const string TAG_SELECTION = "Selection", TAG_CORRECT="Correct", TAG_OPTION="Option";
        private ISet<int> Selected { get; set; }
        private IList<string> Options { get; set; }
        private ISet<int> CorrectOptions { get; set; }
        public CheckBox(ISet<int> selected, IList<string> options, ISet<int> correctOptions)
        {
            
            Selected = selected;
            Options = options;
            CorrectOptions = correctOptions;
            if (this.Options.Count > 4 || this.Options.Count == 0)
            {
                throw new Exception("Checkbox Invalid number of options" + this.Options.Count);
            }
            foreach (var correct in CorrectOptions)
            {
                if (correct >= this.Options.Count || correct < 0 )
                {
                    throw new Exception("Checkbox Question is impossible:" + correct);
                }
            }
            if (this.CorrectOptions.Count==0)
            {
                throw new Exception("Checkbox Invalid number of answers" + this.Options.Count);
            }
            foreach (var select in Selected)
            {
                if (select >= this.Options.Count || select < 0)
                {
                    throw new Exception("Checkbox Question is impossible:" + select);
                }
            }
        }

        public bool IsCorrect()
        {
            return Selected.SetEquals(CorrectOptions);
        }

        public void Select(int choice)
        {
            var alreadySelected = this.Selected.Contains(choice);
            if (alreadySelected)
            {
                this.Selected.Remove(choice);
            }
            else
            {
                this.Selected.Add(choice);
            }
        }

        public XElement toXml()
        {
            var root = new XElement(IContent.TAG_CONTENT);
            var attr = new XAttribute(IContent.TAG_TYPE, this.GetType());
            root.Add(attr);
            foreach (var selection in Selected)
            {
                root.Add(new XElement(TAG_SELECTION, selection));
            }
            foreach (var correct in CorrectOptions)
            {
                root.Add(new XElement(TAG_CORRECT, correct));
            }
            foreach (var option in Options)
            {
                root.Add(new XElement(TAG_OPTION, option));
            }

            return root;
        }
        public static CheckBox FromXml(XElement element)
        {
            var selected = new HashSet<int>();
            foreach (var selectElement in element.Elements(CheckBox.TAG_SELECTION))
            {
                var selection = int.Parse(selectElement.Value);
                selected.Add(selection);
            }
            var options = new List<string>();
            foreach (var optionElement in element.Elements(TAG_OPTION))
            {
                var option = optionElement.Value;
                options.Add(option);
            }
            var correctOptions = new HashSet<int>();
            foreach (var correctElement in element.Elements(TAG_CORRECT))
            {
                var correct = int.Parse(correctElement.Value);

                correctOptions.Add(correct);
            }

            var state = new CheckBox(selected, options, correctOptions);
            return state;
        }

        public bool isSelected(int choice)
        {
            return this.Selected.Contains(choice);
        }

        public IList<string> GetOptions()
        {
            return this.Options;
        }

        public bool isCorrect(int choice)
        {
            return CorrectOptions.Contains(choice);
        }
    }

    /// <summary>
    /// RadioBox is for a single selection question
    /// </summary>
    public class RadioBox : ISelectQuestionState
    {
        private const string TAG_SELECTION = "Selection", TAG_CORRECT = "Correct", TAG_OPTION = "Option";
        public const int NONE_SELECTED = -1;
        private int Selected { get; set; }
        private IList<string> Options { get; set; }
        private int CorrectOption { get; set; }

        public RadioBox(int selected, IList<string> options, int correctOption)
        {
            this.Selected = selected;
            this.Options = options;
            this.CorrectOption = correctOption;
            if (this.Options.Count > 4 || this.Options.Count == 0)
            {
                throw new Exception("Invalid number of options:" + this.Options.Count);
            }
            if (this.CorrectOption >= this.Options.Count || this.CorrectOption < 0)
            {
                throw new Exception("Question is impossible:" + this.CorrectOption);
            }
        }
        public bool IsCorrect()
        {
            return this.Selected == CorrectOption;
        }

        public void Select(int choice)
        {
            this.Selected = choice;
        }

        public XElement toXml()
        {
            var root = new XElement(IContent.TAG_CONTENT);
            var attr = new XAttribute(IContent.TAG_TYPE, this.GetType());
            root.Add(attr);
            root.Add(new XElement(TAG_SELECTION, this.Selected));
            root.Add(new XElement(TAG_CORRECT, this.CorrectOption));
            foreach (var option in Options)
            {
                root.Add(new XElement(TAG_OPTION, option));
            }
            return root;
        }
        public static RadioBox FromXml(XElement radioElement)
        {
            var options = new List<string>();
            foreach (XElement optionElement in radioElement.Elements(TAG_OPTION))
            {
                var option = optionElement.Value;
                options.Add(option);
            }
            var selectedOption = 0;
            foreach (XElement selectedElement in radioElement.Elements(TAG_SELECTION))
            {
                var selected = int.Parse(selectedElement.Value);
                selectedOption = selected;
            }
            var correctOption  = 0;
            foreach (XElement correctElement in radioElement.Elements(TAG_CORRECT))
            {
                var correct = int.Parse(correctElement.Value);
                correctOption = correct;
            }
            var state = new RadioBox(selectedOption, options, correctOption);
            return state;
        }

        public IList<string> GetOptions()
        {
            return this.Options;
        }

        public bool isSelected(int choice)
        {
            return choice == this.Selected;
        }

        public bool isCorrect(int choice)
        {
            return choice == CorrectOption;
        }
    }

    /// <summary>
    /// Subsribeable selection question
    /// </summary>
    public class SelectionModel : QuestionModel {
        private ISelectQuestionState state { set; get; }

        public SelectionModel(ISelectQuestionState state) :base(state)
        {
            this.state = state;
        }
        protected override void onReceiveQuestionEvent(QuestionEvent ev)
        {
            if (ev is SelectEvent)
            {
                this.selection((SelectEvent)ev);
            }
        }

        private void selection(SelectEvent e)
        {
            this.state.Select(e.Selection);
            this.invokeStateUpdated();
        }
    }

    public class InfoContent : IContent
    {
        public const int MAX_INFO_LENGTH = 500;
        public string Info { get; private set; }
        public InfoContent(string info) 
        { 
            this.Info = info;
            if (this.Info.Length > MAX_INFO_LENGTH) { throw new Exception("Info content too long: " + this.Info); }
        }

        public XElement toXml()
        {
            var attr = new XAttribute(IContent.TAG_TYPE, this.GetType());
            var element = new XElement(IContent.TAG_CONTENT, attr, this.Info);
            return element;
        }

        public static InfoContent FromXml(XElement element)
        {
            var info = element.Value;
            return new InfoContent(info);
        }
    }

    public class ImageContent : IContent 
    {
        public string ImageLocation { get; private set; }
        public ImageContent(string imageLocation)
        {
            this.ImageLocation = imageLocation;
        }

        public XElement toXml()
        {
            var attr = new XAttribute(IContent.TAG_TYPE, this.GetType());
            var element = new XElement(IContent.TAG_CONTENT, attr, this.ImageLocation);
            return element;
        }

        public static ImageContent FromXml(XElement element)
        {
            var imageLocation = element.Value;
            return new ImageContent(imageLocation);
        }
    }

    public class VideoContent : IContent
    {
        public string VideoLocation { get; private set; }
        public VideoContent(string videoLoc)
        {
            this.VideoLocation = videoLoc;
        }

        public XElement toXml()
        {
            var attr = new XAttribute(IContent.TAG_TYPE, this.GetType());
            var element = new XElement(IContent.TAG_CONTENT, attr, this.VideoLocation);
            return element;
        }

        public static VideoContent FromXml(XElement element)
        {
            var videoLoc = element.Value;
            return new VideoContent(videoLoc);
        }
    }
}