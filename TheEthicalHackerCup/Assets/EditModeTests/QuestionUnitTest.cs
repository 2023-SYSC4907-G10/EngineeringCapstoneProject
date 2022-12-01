using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using Learning;
using System.Xml;
using System.Xml.Linq;
public class RadioStateTest
{
    RadioState state;
    [SetUp]
    public void setup()
    {
        state = new RadioState();
        state.Name = "Test State Name";
    }
    [Test]
    public void TestRadioStateNoSelection()
    {
        state.Options = new List<string> { "Hello", "World" };
        state.CorrectOption = 1;
        Assert.False(state.isCorrect());
    }

    [Test]
    public void TestRadioStateWrongSelection()
    {

        state.Options = new List<string> { "Hello", "World" };
        state.CorrectOption = 1;
        state.Selected = 2;
        Assert.False(state.isCorrect());
    }
    [Test]
    public void RadioNoSelectionNoAnswer()
    {
        Assert.True(state.isCorrect());
    }
    [Test]
    public void RadioRightToWrong()
    {
        state.Options = new List<string> { "Hello", "World" };
        state.CorrectOption = 1;
        state.Selected = 1;
        state.Selected = 2;
        Assert.False(state.isCorrect());
    }
    [Test]
    public void RadioWrongToRight()
    {
        state.Options = new List<string> { "Hello", "World" };
        state.CorrectOption = 1;
        state.Selected = 2;
        state.Selected = 1;
        Assert.True(state.isCorrect());

    }
    [Test]
    public void RadioWrongToWrong()
    {
        state.Options = new List<string> { "Hello", "World", "!" };
        state.CorrectOption = 1;
        state.Selected = 2;
        state.Selected = 3;
        Assert.False(state.isCorrect());
    }
    [Test]
    public void TestXml()
    {
        state.Options.Add("Op1");
        state.Options.Add("Op2");
        state.CorrectOption = 1;
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
    public void setup()
    {
        state = new CheckboxState();
        state.Name = "Test State Name";
    }
    [Test]
    public void TestCheckboxStateNoSelection()
    {
        state.Options = new List<string> { "Hello", "World" };

        state.CorrectOptions = new HashSet<int> { 1 };
        Assert.False(state.isCorrect());
    }

    [Test]
    public void TestCheckboxStateWrongSelection()
    {

        state.Options = new List<string> { "Hello", "World" };
        state.CorrectOptions = new HashSet<int> { 1 };
        state.Selected = new HashSet<int> { 2 };
        Assert.False(state.isCorrect());
    }
    [Test]
    public void CheckboxNoSelectionNoAnswer()
    {
        Assert.True(state.isCorrect());
    }
    [Test]
    public void CheckboxRightToWrong()
    {
        state.Options = new List<string> { "Hello", "World" };
        state.CorrectOptions = new HashSet<int> { 1 };
        state.Selected = new HashSet<int> { 1 };
        state.Selected = new HashSet<int> { 2 };
        Assert.False(state.isCorrect());
    }
    [Test]
    public void CheckboxWrongToRight()
    {
        state.Options = new List<string> { "Hello", "World" };
        state.CorrectOptions = new HashSet<int> { 1 };
        state.Selected = new HashSet<int> { 2 };
        state.Selected = new HashSet<int> { 1 };
        Assert.True(state.isCorrect());

    }
    [Test]
    public void CheckboxWrongToWrong()
    {
        state.Options = new List<string> { "Hello", "World", "!" };
        state.CorrectOptions = new HashSet<int> { 1 };
        state.Selected = new HashSet<int> { 2 };
        state.Selected = new HashSet<int> { 3 };
        Assert.False(state.isCorrect());
    }

    [Test]
    public void CheckboxSubsetOfRightAnswer()
    {
        state.Options = new List<string> { "Hello", "World", "!" };
        state.CorrectOptions = new HashSet<int> { 1, 2 };
        state.Selected = new HashSet<int> { 2 };
        Assert.False(state.isCorrect());
    }

    [Test]
    public void CheckboxSupersetOfRightAnswer()
    {
        state.Options = new List<string> { "Hello", "World", "!" };
        state.CorrectOptions = new HashSet<int> { 1 };
        state.Selected = new HashSet<int> { 1, 2 };
        Assert.False(state.isCorrect());

    }

    [Test]
    public void CheckboxOverlapOfRightAnswer()
    {
        state.Options = new List<string> { "Hello", "World", "!" };
        state.CorrectOptions = new HashSet<int> { 1, 2 };
        state.Selected = new HashSet<int> { 0, 1 };
        Assert.False(state.isCorrect());

    }
    [Test]
    public void TestXml()
    {
        state.Options.Add("Op1");
        state.Options.Add("Op2");
        state.CorrectOptions.Add(1);
        var output = state.toXml().ToString();
        var element = XElement.Parse(output);
        var state2 = QuestionState.fromXml(element);

        Assert.AreEqual(output, state2.toXml().ToString());
    }


}
class TestCheckboxQuestion
{
    CheckboxState state;
    CheckboxQuestionModel question;
    int stateChangedCount = 0;

    [SetUp]
    public void setup()
    {
        state = new CheckboxState();
        state.Name = "Mock Question";
        state.Options = new List<string> { "Mock 1", "Mock 2", "Mock 3" };
        state.CorrectOptions = new HashSet<int> { 0, 1 };
        question = new CheckboxQuestionModel(state);
        question.QuestionStateUpdated += delegate (object sender, QuestionStateUpdatedEvent args) {
            if (state == args.QuestionState)
            {
                stateChangedCount++;
            }
        };
    }


    [Test]
    public void TestLifeCycle()
    {
        var select = new SelectEvent(1);
        question.invokeQuestionEvent(select);
        Assert.AreEqual(1, stateChangedCount);

        var select2 = new SelectEvent(0);
        question.invokeQuestionEvent(select2);
        Assert.AreEqual(2, stateChangedCount);
        Assert.AreEqual(new HashSet<int>() { 1, 0 }, state.Selected);

        var select3 = new SelectEvent(1);
        question.invokeQuestionEvent(select3);
        Assert.AreEqual(3, stateChangedCount);
    }

}

class TestRadioQuestion
{
    RadioState state;
    RadioQuestionModel question;
    int changedCount = 0;
    [SetUp]
    public void setup()
    {
        state = new RadioState();
        state.Name = "Mock Question";
        state.Options = new List<string> { "Mock 1", "Mock 2", "Mock 3" };
        state.CorrectOption = 0;
        question = new RadioQuestionModel(state);
        question.QuestionStateUpdated += delegate (object sender, QuestionStateUpdatedEvent args) {
            if (state == args.QuestionState)
            {
                changedCount++;
            }
        };
    }


    [Test]
    public void TestLifeCycle()
    {
        //Start the life cycle
        var start = new StartEvent();
        question.invokeQuestionEvent(new SelectEvent(2));
        Assert.AreEqual(1, changedCount);
        Assert.AreEqual(2, state.Selected);

        //Check box 1
        var box1 = new SelectEvent(1);
        question.invokeQuestionEvent(box1);
        Assert.AreEqual(2, changedCount);
        Assert.AreEqual(1, state.Selected);

        //Check box 2
        var box2 = new SelectEvent(0);
        question.invokeQuestionEvent(box2);
        Assert.AreEqual(3, changedCount);
        Assert.AreEqual(0, state.Selected);

    }



}