using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using Learning;
using NSubstitute;


public class TestQuizState{



    [Test]
    public void DivideByZero(){
        var quiz = new QuizState("A",new List<QuestionState>());
        Assert.DoesNotThrow(()=>{var b = quiz.Percent;});
        Assert.DoesNotThrow(()=>{var a= quiz.IsPassingScore;});

    }

    [Test]
    public void TestBadScore(){
        var questions = new List<QuestionState>();
        for(int i=0; i<10;i++){
            questions.Add(new CheckboxState());
        }
        var quiz = new QuizState("TestQuiz", questions);
        quiz.Score=4;
        Assert.False(quiz.IsPassingScore);
        Assert.False(quiz.IsNoMoreQuestions);
    }
    [Test]
    public void TestGoodScore(){
        var questions = new List<QuestionState>();
        for(int i=0;i<10;i++){
            questions.Add(new CheckboxState());
        }
        var quiz = new QuizState("TestQuiz", questions);
        quiz.Score = 5;
        Assert.True(quiz.IsPassingScore);
        Assert.False(quiz.IsNoMoreQuestions);
    }

}


public class TestQuiz{

    QuizObserver quizObserver;
    QuizState quizState;
    RadioState q1,q2;
    CheckboxState q3;
    IList<QuestionObserver> observers;
    IList<int> questionEndCount;
    IList<int> questionNotifyCount;
    int quiznotifyCount;
    int endQuizCount;
    Quiz quiz;
    [SetUp]
    public void setup(){
        //reset counters
        questionEndCount=new List<int>{0,0,0};
        questionNotifyCount = new List<int>{0,0,0};
        quiznotifyCount = 0;
        endQuizCount = 0;
        //make stubs
        quizObserver = Substitute.For<QuizObserver>();
        //setup questions
        q1 = new RadioState();
        q1.Name = "Question 1";
        q1.CorrectOption = 0;
        q1.Options = new List<string>{"Option 0", "Option 1", "Option 2"};
        q2 = new RadioState();
        q2.Name = "Question 2";
        q2.CorrectOption = 3;
        q2.Options = new List<string>{"0","1","2","3"};
        q3 = new CheckboxState();
        q3.Name = "Quiestopm 3";
        q3.CorrectOptions = new HashSet<int>{1,2};
        q3.Options = new List<string>{"A","B","C","D"};
        //make quiz state
        quizState = new QuizState("MyQuiz",new List<QuestionState>{q1,q2,q3});
        //make quiz
        quiz = new Quiz(quizState);
        quiz.addObserver(quizObserver);

        var ob0 =  Substitute.For<QuestionObserver>();
        quiz.questions[0].addQuestionObserver(ob0);
        
        var ob1 =  Substitute.For<QuestionObserver>();
        quiz.questions[1].addQuestionObserver(ob1);

        var ob2 =  Substitute.For<QuestionObserver>();
        quiz.questions[2].addQuestionObserver(ob2);

        quizObserver.When(x=>x.notify(quizState))
            .Do(x=>{quiznotifyCount++;});
        quizObserver.When(x=>x.quizEnded(Arg.Any<bool>()))
            .Do(x=>{endQuizCount++;});
        
        ob0.When(x=>x.notify(Arg.Any<QuestionState>()))
            .Do(x=>{questionNotifyCount[0]++;});
        ob0.When(x=>x.submitted(Arg.Any<bool>()))
            .Do(x=>{questionEndCount[0]++;});
        ob1.When(x=>x.notify(Arg.Any<QuestionState>()))
            .Do(x=>{questionNotifyCount[1]++;});
        ob1.When(x=>x.submitted(Arg.Any<bool>()))
            .Do(x=>{questionEndCount[1]++;});
        ob2.When(x=>x.notify(Arg.Any<QuestionState>()))
            .Do(x=>{questionNotifyCount[2]++;});
        ob2.When(x=>x.submitted(Arg.Any<bool>()))
            .Do(x=>{questionEndCount[2]++;});
    }


    [Test]
    public void TestQuizLifeCycleGoodMark(){
        
        quiz.invokeQuizEvent(new StartQuizEvent());
        //TODO: tomorrow the different quiz state machines is a problem to look into
        //fail first
        quiz.questions[0].invokeQuestionEvent(new SelectEvent(1));
        quiz.questions[0].invokeQuestionEvent(new SubmitEvent());
        Assert.AreEqual(2, questionNotifyCount[0]);
        Assert.AreEqual(1, questionEndCount[0]);
        Assert.AreEqual(1, quiznotifyCount);
        Assert.AreEqual(1, quiz.state.CurrentQuestion);
        Assert.AreEqual(0, endQuizCount);
        Assert.AreEqual(1, questionNotifyCount[1]);
        Assert.AreEqual(0, questionNotifyCount[2]);
        //pass second
        quiz.questions[1].invokeQuestionEvent(new SelectEvent(3));
        quiz.questions[1].invokeQuestionEvent(new SubmitEvent());
        Assert.AreEqual(2, questionNotifyCount[0]);
        Assert.AreEqual(1, questionEndCount[0]);
        Assert.AreEqual(1, quiznotifyCount);
        Assert.AreEqual(2, quiz.state.CurrentQuestion);
        Assert.AreEqual(0, endQuizCount);
        Assert.AreEqual(2, questionNotifyCount[1]);
        Assert.AreEqual(1, questionNotifyCount[2]);


        //pass third
        quiz.questions[2].invokeQuestionEvent(new SelectEvent(1));
        quiz.questions[2].invokeQuestionEvent(new SelectEvent(2));
        quiz.questions[2].invokeQuestionEvent(new SubmitEvent());
        Assert.AreEqual(2, questionNotifyCount[0]);
        Assert.AreEqual(1, questionEndCount[0]);
        Assert.AreEqual(1, quiznotifyCount);
        Assert.AreEqual(3, quiz.state.CurrentQuestion);
        Assert.AreEqual(1, endQuizCount);
        Assert.AreEqual(2, questionNotifyCount[1]);
        Assert.AreEqual(3, questionNotifyCount[2]);


        //evaluate score to make sure it is accurate
        Assert.True(quiz.state.IsPassingScore);
        Assert.True(quiz.state.IsNoMoreQuestions);
        Assert.AreEqual(2,quiz.state.Score);
        Assert.AreEqual(2.0/3.0*100.0, quiz.state.Percent, 0.01);
    }
    [Test]
    public void TestQuizLifeCycleBadMark(){
        quiz.invokeQuizEvent(new StartQuizEvent());
        //TODO: tomorrow the different quiz state machines is a problem to look into
        //fail first
        quiz.questions[0].invokeQuestionEvent(new SelectEvent(1));
        quiz.questions[0].invokeQuestionEvent(new SubmitEvent());
        Assert.AreEqual(2, questionNotifyCount[0]);
        Assert.AreEqual(1, questionEndCount[0]);
        Assert.AreEqual(1, quiznotifyCount);
        Assert.AreEqual(1, quiz.state.CurrentQuestion);
        Assert.AreEqual(0, endQuizCount);
        Assert.AreEqual(1, questionNotifyCount[1]);
        Assert.AreEqual(0, questionNotifyCount[2]);
        //fail second
        quiz.questions[1].invokeQuestionEvent(new SelectEvent(2));
        quiz.questions[1].invokeQuestionEvent(new SubmitEvent());
        Assert.AreEqual(2, questionNotifyCount[0]);
        Assert.AreEqual(1, questionEndCount[0]);
        Assert.AreEqual(1, quiznotifyCount);
        Assert.AreEqual(2, quiz.state.CurrentQuestion);
        Assert.AreEqual(0, endQuizCount);
        Assert.AreEqual(2, questionNotifyCount[1]);
        Assert.AreEqual(1, questionNotifyCount[2]);


        //pass third
        quiz.questions[2].invokeQuestionEvent(new SelectEvent(1));
        quiz.questions[2].invokeQuestionEvent(new SelectEvent(2));
        quiz.questions[2].invokeQuestionEvent(new SubmitEvent());
        Assert.AreEqual(2, questionNotifyCount[0]);
        Assert.AreEqual(1, questionEndCount[0]);
        Assert.AreEqual(1, quiznotifyCount);
        Assert.AreEqual(3, quiz.state.CurrentQuestion);
        Assert.AreEqual(1, endQuizCount);
        Assert.AreEqual(2, questionNotifyCount[1]);
        Assert.AreEqual(3, questionNotifyCount[2]);


        //evaluate score to make sure it is accurate
        Assert.False(quiz.state.IsPassingScore);
        Assert.True(quiz.state.IsNoMoreQuestions);
        Assert.AreEqual(1,quiz.state.Score);
        Assert.AreEqual(1.0/3.0*100.0, quiz.state.Percent, 0.01);



    }



}