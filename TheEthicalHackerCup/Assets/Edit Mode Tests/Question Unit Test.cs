using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using Learning;
using NSubstitute;
using System.Xml;
using System.Xml.Linq;
public class RadioStateTest
{
    RadioState state;
    [SetUp]
    public void setup(){
        state = new RadioState();
        state.Name = "Test State Name";
    }
    [Test]
    public void TestRadioStateNoSelection(){
        state.Options = new List<string>{"Hello","World"};
        state.CorrectOption = 1;
        Assert.False(state.isCorrect());
    }

    [Test]
    public void TestRadioStateWrongSelection(){

        state.Options = new List<string>{"Hello","World"};
        state.CorrectOption = 1;
        state.Selected = 2;
        Assert.False(state.isCorrect());
    }
    [Test]
    public void RadioNoSelectionNoAnswer(){
        Assert.True(state.isCorrect());
    }
    [Test]
    public void RadioRightToWrong(){
        state.Options = new List<string>{"Hello","World"};
        state.CorrectOption = 1;
        state.Selected = 1;
        state.Selected = 2;
        Assert.False(state.isCorrect());
    }
    [Test]
    public void RadioWrongToRight(){
        state.Options = new List<string>{"Hello","World"};
        state.CorrectOption = 1;
        state.Selected = 2;
        state.Selected = 1;
        Assert.True(state.isCorrect());

    }
    [Test]
    public void RadioWrongToWrong(){
        state.Options = new List<string>{"Hello","World","!"};
        state.CorrectOption = 1;
        state.Selected = 2;
        state.Selected = 3;
        Assert.False(state.isCorrect());
    }
    [Test]
    public void TestXml(){
        var output = state.toXml().ToString();
        var element = XElement.Parse(output);
        var state2 = QuestionState.fromXml(element);

        Assert.AreEqual(output, state2.toXml().ToString());
    }
}
public class CheckboxStateTest 
{
    CheckboxState state;
    [SetUp]
    public void setup(){
        state = new CheckboxState();
        state.Name = "Test State Name";
    }
    [Test]
    public void TestCheckboxStateNoSelection(){
        state.Options = new List<string>{"Hello","World"};

        state.CorrectOptions = new HashSet<int>{1};
        Assert.False(state.isCorrect());
    }

    [Test]
    public void TestCheckboxStateWrongSelection(){

        state.Options = new List<string>{"Hello","World"};
        state.CorrectOptions = new HashSet<int>{1};
        state.Selected = new HashSet<int>{2};
        Assert.False(state.isCorrect());
    }
    [Test]
    public void CheckboxNoSelectionNoAnswer(){
        Assert.True(state.isCorrect());
    }
    [Test]
    public void CheckboxRightToWrong(){
        state.Options = new List<string>{"Hello","World"};
        state.CorrectOptions = new HashSet<int>{1};
        state.Selected = new HashSet<int>{1};
        state.Selected = new HashSet<int>{2};
        Assert.False(state.isCorrect());
    }
    [Test]
    public void CheckboxWrongToRight(){
        state.Options = new List<string>{"Hello","World"};
        state.CorrectOptions=new HashSet<int>{1};
        state.Selected =new HashSet<int>{2};
        state.Selected =new HashSet<int>{1};
        Assert.True(state.isCorrect());

    }
    [Test]
    public void CheckboxWrongToWrong(){
        state.Options = new List<string>{"Hello","World","!"};
        state.CorrectOptions=new HashSet<int>{1};
        state.Selected =new HashSet<int>{2};
        state.Selected =new HashSet<int>{3};
        Assert.False(state.isCorrect());
    }

    [Test]
    public void CheckboxSubsetOfRightAnswer(){
        state.Options = new List<string>{"Hello","World","!"};
        state.CorrectOptions=new HashSet<int>{1,2};
        state.Selected =new HashSet<int>{2};
        Assert.False(state.isCorrect());
    }

    [Test]
    public void CheckboxSupersetOfRightAnswer(){
        state.Options = new List<string>{"Hello","World","!"};
        state.CorrectOptions=new HashSet<int>{1};
        state.Selected =new HashSet<int>{1, 2};
        Assert.False(state.isCorrect());

    }

    [Test]
    public void CheckboxOverlapOfRightAnswer(){
        state.Options = new List<string>{"Hello","World","!"};
        state.CorrectOptions=new HashSet<int>{1,2};
        state.Selected =new HashSet<int>{0,1};
        Assert.False(state.isCorrect());

    }
    [Test]
    public void TestXml(){
        var output = state.toXml().ToString();
        var element = XElement.Parse(output);
        var state2 = QuestionState.fromXml(element);

        Assert.AreEqual(output, state2.toXml().ToString());
    }


}

class TestCheckboxQuestion{
    QuestionObserver observer;
    CheckboxState state;
    CheckboxQuestion question;
    [SetUp]
    public void setup(){
        observer = Substitute.For<QuestionObserver>();
        state = new CheckboxState();
        state.Name = "Mock Question";
        state.Options = new List<string>{"Mock 1", "Mock 2", "Mock 3"};
        state.CorrectOptions = new HashSet<int>{0,1};
        question = new CheckboxQuestion(state);
        question.addQuestionObserver(observer);
    }


    [Test]
    public void TestLifeCycle(){
        int notified=0;
        int submitted=0;
        observer.When(x => x.notify(state))
            .Do(x=>notified++);
        observer.When(x => x.submitted(true))
            .Do(x=>submitted++);
        //Start the life cycle
        var start = new StartEvent();
        question.invokeQuestionEvent(start);
        Assert.AreEqual(1, notified);

        //Check box 1
        var box1 = new SelectEvent(1);
        question.invokeQuestionEvent(box1);
        Assert.AreEqual(2, notified);
        Assert.True(state.Selected.Contains(1));

        //Check box 2
        var box2 = new SelectEvent(0);
        question.invokeQuestionEvent(box2);
        Assert.AreEqual(3, notified);
        Assert.True(state.Selected.Contains(0));
        Assert.True(state.Selected.Contains(1));

        //submit
        var submit = new SubmitEvent();
        question.invokeQuestionEvent(submit);
        Assert.AreEqual(1, submitted);
        Assert.AreEqual(3, notified);
    }

}

class TestRadioQuestion{
    QuestionObserver observer;
    RadioState state;
    RadioQuestion question;
    [SetUp]
    public void setup(){
        observer = Substitute.For<QuestionObserver>();
        state = new RadioState();
        state.Name = "Mock Question";
        state.Options = new List<string>{"Mock 1", "Mock 2", "Mock 3"};
        state.CorrectOption =  0;
        question = new RadioQuestion(state);
        question.addQuestionObserver(observer);
    }


    [Test]
    public void TestLifeCycle(){
        int notified=0;
        int submitted=0;
        observer.When(x => x.notify(state))
            .Do(x=>notified++);
        observer.When(x => x.submitted(true))
            .Do(x=>submitted++);
        //Start the life cycle
        var start = new StartEvent();
        question.invokeQuestionEvent(start);
        Assert.AreEqual(1, notified);

        //Check box 1
        var box1 = new SelectEvent(1);
        question.invokeQuestionEvent(box1);
        Assert.AreEqual(2, notified);
        Assert.AreEqual(1, state.Selected);

        //Check box 2
        var box2 = new SelectEvent(0);
        question.invokeQuestionEvent(box2);
        Assert.AreEqual(3, notified);
        Assert.AreEqual(0,state.Selected);

        //submit
        var submit = new SubmitEvent();
        question.invokeQuestionEvent(submit);
        Assert.AreEqual(1, submitted);
        Assert.AreEqual(3, notified);
    }



}