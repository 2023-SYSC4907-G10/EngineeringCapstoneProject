using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using Learning;
using System.Xml;
using System.Xml.Linq;
using System.Net.Http.Headers;
using JetBrains.Annotations;

public class RadioBoxTest
{
    RadioBox radiobox;
    int selected;
    int correct;
    IList<string> options;

    [SetUp]
    public void setup()
    {

        selected = -1;
        correct = 1;
        options = new List<string>();
        options.Add("A");
        options.Add("B");
        options.Add("C");
        options.Add("D");
        radiobox = new RadioBox(selected, options, correct);
    }
    //selection

    [Test]
    public void AddSelection()
    {

        radiobox.Select(1);
        Assert.IsTrue(radiobox.isSelected(1));
    }
    [Test]
    public void changeSelection()
    {
        radiobox.Select(1);
        radiobox.Select(2);
        Assert.IsFalse(radiobox.isSelected(1));
    }
    //exceptions when creating it
    [Test]
    public void noOptions()
    {
        selected = 0;
        correct = 0;
        options = new List<string>();
        Assert.Catch(() => { radiobox = new RadioBox(selected, options, correct); });

    }
    [Test]
    public void correctOutOfRange()
    {
        selected = 0;
        correct = 5;
        options = new List<string>();
        options.Add("A");
        options.Add("B");
        options.Add("C");
        options.Add("D");
        Assert.Catch(() => { radiobox = new RadioBox(selected, options, correct); });
    }

    // is correct
    [Test]
    public void isCorrect()
    {
        radiobox.Select(1);
        Assert.True(radiobox.IsCorrect());
    }
    [Test]
    public void isInCorrect()
    {
        radiobox.Select(2);
        Assert.False(radiobox.IsCorrect());
    }

    //xml   
    [Test]
    public void xml()
    {
        var xml = radiobox.toXml();
        var r2 = RadioBox.FromXml(xml);
        Assert.AreEqual(radiobox.toXml().ToString(), r2.toXml().ToString());

    }

}

public class CheckBoxTest
{

    CheckBox checkbox;
    ISet<int> selected;
    ISet<int> correct;
    IList<string> options;

    [SetUp]
    public void setup() {

        selected = new HashSet<int>();
        correct = new HashSet<int>();
        options = new List<string>(); 
        options.Add("A");
        options.Add("B");
        options.Add("C");
        options.Add("D");
        correct.Add(1);
        checkbox = new CheckBox(selected, options, correct);
    }
    //selection

    [Test]
    public void AddSelection() {

        checkbox.Select(1);
        Assert.True(checkbox.isSelected(1));
    }
    [Test]
    public void removeSelection() {
        checkbox.Select(1); 
        checkbox.Select(1);
        Assert.False(checkbox.isSelected(1));
    }
    [Test]
    public void multiSelect() {
        checkbox.Select(1);
        checkbox.Select(2);
        Assert.True(checkbox.isSelected(1));
        Assert.True(checkbox.isSelected(2));
    }
    //exceptions when creating it
    [Test]
    public void noOptions() {
        selected = new HashSet<int>();
        correct = new HashSet<int>();
        options = new List<string>();
        correct.Add(1);
        Assert.Catch(() => { checkbox = new CheckBox(selected, options, correct); });
        
    }
    [Test]
    public void noCorrect() {

        selected = new HashSet<int>();
        correct = new HashSet<int>();
        options = new List<string>();
        options.Add("A");
        options.Add("B");
        options.Add("C");
        options.Add("D");
        Assert.Catch(() => { checkbox = new CheckBox(selected, options, correct); });
    }
    [Test]
    public void correctOutOfRange() {
        selected = new HashSet<int>();
        correct = new HashSet<int>();
        options = new List<string>();
        options.Add("A");
        options.Add("B");
        options.Add("C");
        options.Add("D");
        correct.Add(5);
        Assert.Catch(() => { checkbox = new CheckBox(selected, options, correct); });
    }
    [Test]
    public void selectionOutOfRange() {
        selected = new HashSet<int>();
        correct = new HashSet<int>();
        options = new List<string>();
        options.Add("A");
        options.Add("B");
        options.Add("C");
        options.Add("D");
        correct.Add(1);
        selected.Add(-1);
        Assert.Catch(() => { checkbox = new CheckBox(selected, options, correct); });
    }

    // is correct
    [Test]
    public void isCorrect() {
        checkbox.Select(1);
        Assert.True(checkbox.IsCorrect());
    }
    [Test]
    public void isInCorrect() {
        checkbox.Select(2);
        Assert.False(checkbox.IsCorrect());
    }

    //xml   
    [Test]
    public void xml() {
        var xml = checkbox.toXml();
        var c2 = CheckBox.FromXml(xml); 
        Assert.AreEqual(checkbox.toXml().ToString(),c2.toXml().ToString());
        
    }
}

public class InfoTest{
    InfoContent info;
    string s;

    [SetUp]
    public void setup()
    {
        s = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        info = new InfoContent(s);
    }
    [Test]
    public void alright()
    {
        var okay = InfoContent.MAX_INFO_LENGTH;
        var water = new string('a', okay);
        Assert.DoesNotThrow(() => { InfoContent info = new InfoContent(water); });
    }
    [Test]
    public void tooLong()
    {
        var overflow = InfoContent.MAX_INFO_LENGTH + 1;
        var poison = new string('a', overflow);
        Assert.Catch(()=> { InfoContent info = new InfoContent(poison); });
    }
    [Test]
    public void xml()
    {
        var xml = info.toXml();
        var c2 = InfoContent.FromXml(xml);
        Assert.AreEqual(info.toXml().ToString(), c2.toXml().ToString());

    }
}

class SlideTest {
    [Test]
    public void xml() {
        InfoContent info;
        string s;
        s = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        info = new InfoContent(s);

        Slide slide = new Slide("name", info);

        var xml = slide.toXml();
        var c2 = Slide.FromXml(xml);
        Debug.Log(xml.ToString());
        Assert.AreEqual(slide.toXml().ToString(), c2.toXml().ToString());
    }

}