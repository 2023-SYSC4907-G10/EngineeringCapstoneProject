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
        var quiz = new QuizState("A", new List<QuestionState>());
        Assert.DoesNotThrow(() => { var b = quiz.Percent; });
        Assert.DoesNotThrow(() => { var a = quiz.IsPassingScore; });

    }

    [Test]
    public void TestBadScore()
    {
        var questions = new List<QuestionState>();
        for (int i = 0; i < 10; i++)
        {
            var state = new CheckboxState();
            state.Selected.Add(1);
            state.CorrectOptions.Add(0);
            questions.Add(state);
        }
        var quiz = new QuizState("TestQuiz", questions);
        Assert.False(quiz.IsPassingScore);
        Assert.False(quiz.IsNoMoreQuestions);
    }
    [Test]
    public void TestGoodScore()
    {
        var questions = new List<QuestionState>();
        for (int i = 0; i < 10; i++)
        {
            var state = new CheckboxState();
            state.Selected = new HashSet<int>() { 1 };
            state.CorrectOptions = new HashSet<int>() { 1 };
            questions.Add(state);

        }
        var quiz = new QuizState("TestQuiz", questions);
        quiz.updateScore();
        Assert.True(quiz.IsPassingScore);
        Assert.False(quiz.IsNoMoreQuestions);
    }

}

//All transitions criterion
public class TestQuiz
{

    QuizState quizState;
    RadioState q1;
    CheckboxState q2;
    IList<int> questionEndCount;
    IList<int> questionNotifyCount;
    int nextQuestionCount;
    int startQuizCount;
    int submittedCount;
    int endQuizCount;
    QuizModel quiz;
    [SetUp]
    public void setup()
    {
        //reset counters
        nextQuestionCount = 0;
        startQuizCount = 0;
        endQuizCount = 0;
        submittedCount = 0;
        //make stubs
        //setup questions
        q1 = new RadioState();
        q1.Name = "Question 1";
        q1.CorrectOption = 0;
        q1.Options = new List<string> { "Option 0", "Option 1", "Option 2" };
        q2 = new CheckboxState();
        q2.Name = "Quiestopm 2";
        q2.CorrectOptions = new HashSet<int> { 1, 2 };
        q2.Options = new List<string> { "A", "B", "C", "D" };
        //make quiz state
        quizState = new QuizState("MyQuiz", new List<QuestionState> { q1, q2 });
        //make quiz
        quiz = new QuizModel(quizState);
        quiz.QuizStarted += delegate (object sender, QuizStartedEvent evt) {
            if (evt.quizName == quizState.Name)
            {
                startQuizCount++;
            }
        };

        quiz.QuestionChanged += delegate (object sender, NextQuestionEvent args) {
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
        quiz.next(); //go into the first question
        quiz.next(); //go into the second question
        quiz.next(); //show results
        quiz.next(); //submit

        Assert.AreEqual(1, startQuizCount);
        Assert.AreEqual(2, nextQuestionCount);
        Assert.AreEqual(1, submittedCount);
        Assert.AreEqual(1, endQuizCount);
    }
    [Test]
    public void TestQuizLifeCycleNoQuestions()
    {
        var qs2 = new QuizState("Empty", new List<QuestionState>());
        var quiz2 = new QuizModel(qs2);

        quiz2.QuizStarted += delegate (object sender, QuizStartedEvent evt) {
            startQuizCount++;
        };

        quiz2.QuestionChanged += delegate (object sender, NextQuestionEvent args) {
            nextQuestionCount++;
        };
        quiz2.QuizSubmitted += delegate (object sender, QuizSubmittedEvent args) {
            submittedCount++;
        };
        quiz2.QuizClosed += delegate (object sender, QuizClosedEvent args) {
            endQuizCount++;
        };


        quiz2.start();//start
        quiz2.next();//submitted
        quiz2.next();//closed


        Assert.AreEqual(1, startQuizCount);
        Assert.AreEqual(0, nextQuestionCount);
        Assert.AreEqual(1, submittedCount);
        Assert.AreEqual(1, endQuizCount);

    }

}