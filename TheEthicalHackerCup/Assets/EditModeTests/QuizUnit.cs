using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using Learning;


public class TestQuizState
{


    [Test]
    public void DivideByZero()
    {
        var slides = new List<Slide>() { };
        slides.Add(new Slide("A", new InfoContent("B")));
        var quiz = new Quiz(slides);
        Assert.DoesNotThrow(() => { var b = quiz.Percent; });
        Assert.DoesNotThrow(() => { var a = quiz.IsPassingScore; });

    }

    [Test]
    public void TestBadScore()
    {
        var slides = new List<Slide>();
        {
            var info = new InfoContent("Welcome");
            var s = new Slide("SLIDE 1", info);
            slides.Add(s);
        }
        {
            var cq = new CheckBox(new HashSet<int>() { }, new List<string>() { "Hello", "Bruh" }, new HashSet<int>() { 1 });
            var s = new Slide("SLIDE 2", cq);
            slides.Add(s);
        }
        {
            var sq = new RadioBox(1, new List<string>() { "Hello", "Bruh" }, 0);
            var s = new Slide("SLIDE 3", sq);
            slides.Add(s);
        }
        var q = new Quiz(slides);

        var model = new QuizModel(q);
        model.QuizClosed += delegate (object sender, QuizClosedEvent e) { };
        model.QuizSubmitted += delegate (object sender, QuizSubmittedEvent e) { };
        model.SlideChanged += delegate (object sender, NextSlideEvent e) { };

        model.start();
        model.next();
        model.next();
        model.next();
        Assert.IsTrue(q.IsNoMoreQuestions); Assert.AreEqual(0, q.Percent);
        Assert.IsFalse(q.IsPassingScore);
        
    }
    [Test]
    public void TestGoodScore()
    {
        var slides = new List<Slide>();
        {
            var info = new InfoContent("Welcome");
            var s = new Slide("SLIDE 1", info);
            slides.Add(s);
        }
        {
            var cq = new CheckBox(new HashSet<int>() {1 }, new List<string>() { "Hello", "Bruh" }, new HashSet<int>() { 1 });
            var s = new Slide("SLIDE 2", cq);
            slides.Add(s);
        }
        {
            var sq = new RadioBox(1, new List<string>() { "Hello", "Bruh" }, 0);
            var s = new Slide("SLIDE 3", sq);
            slides.Add(s);
        }
        var q = new Quiz(slides);

        var model = new QuizModel(q);
        model.QuizClosed += delegate (object sender, QuizClosedEvent e) { };
        model.QuizSubmitted += delegate (object sender, QuizSubmittedEvent e) { };
        model.SlideChanged += delegate (object sender, NextSlideEvent e) { };

        model.start();
        model.next();
        model.next();
        model.next();
        Assert.IsTrue(q.IsNoMoreQuestions); Assert.AreEqual(50, q.Percent);
        Assert.IsTrue(q.IsPassingScore);

    }




    [Test]
    public void xml()
    {
        var slides = new List<Slide>();
        {
            var info = new InfoContent("Welcome");
            var s = new Slide("SLIDE 1", info);
            slides.Add(s);
        }
        {
            var cq = new CheckBox(new HashSet<int>() { }, new List<string>() { "Hello", "Bruh" }, new HashSet<int>() { 1 });
            var s = new Slide("SLIDE 2", cq);
            slides.Add(s);
        }
        {
            var sq = new RadioBox(1, new List<string>() { "Hello", "Bruh" }, 0);
            var s = new Slide("SLIDE 3", sq);
            slides.Add(s);
        }
        var q = new Quiz(slides);

        var xml = q.ToXml();

        var q2 = Quiz.FromXml(xml.ToString());

        var xml2 = q2.ToXml();

        Assert.AreEqual(xml.ToString(), xml2.ToString());

    }

}
//All transitions criterion
public class TestQuizRunThroughs
{

    Quiz quizState;
    RadioBox q1;
    CheckBox q2;
    IList<int> questionEndCount;
    IList<int> questionNotifyCount;
    int nextQuestionCount;
    int submittedCount;
    int endQuizCount;
    QuizModel quiz;
    [SetUp]
    public void setup()
    {
        //reset counters
        nextQuestionCount = 0;
        endQuizCount = 0;
        submittedCount = 0;
        //make stubs
        //setup questions
        
        q1 = new RadioBox(0, new List<string> { "Option 0", "Option 1", "Option 2" }, 0);
        var s1 = new Slide("slide 1", q1);
        
        q2 = new CheckBox(new HashSet<int>(), new List<string> { "A", "B", "C", "D" }, new HashSet<int> { 1, 2 });
        var s2 = new Slide("slide 2", q2);
        //make quiz state
        quizState = new Quiz(new List<Slide> { s1, s2 });
        //make quiz
        quiz = new QuizModel(quizState);

        quiz.SlideChanged += delegate (object sender, NextSlideEvent args) {
            nextQuestionCount++;
        };
        quiz.QuizSubmitted += delegate (object sender, QuizSubmittedEvent args) {
            submittedCount++;
        };
        quiz.QuizClosed += delegate (object sender, QuizClosedEvent args) {
            endQuizCount++;
        };
    }


    [Test]
    public void TestRunThrough()
    {
        quiz.start(); //start the quiz
        quiz.next(); //go into the second
        quiz.next(); //go into results
        quiz.next(); //submit

        Assert.AreEqual(2, nextQuestionCount);
        Assert.AreEqual(1, submittedCount);
        Assert.AreEqual(1, endQuizCount);
    }
    [Test]
    public void TestQuizLifeCycleNoQuestions()
    {
        Assert.Catch(() => { var qs2 = new Quiz(new List<Slide>()); });
        
    }
}
