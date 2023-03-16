using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialSingleton
{
    private List<Sprite> tutorialImages;
    private TextAsset textFile;
    private string endTutorialTransitionScene;

    // Static singleton
    private static TutorialSingleton _instance;

    public static TutorialSingleton GetInstance()
    {
        if (_instance == null)
        {
            _instance = new TutorialSingleton();
            _instance.InitializeGameState();
        }
        return _instance;
    }

    private TutorialSingleton() { } // Private constructor so new instances can't be made

    public void InitializeGameState()
    {
        this.tutorialImages = new List<Sprite>(10);
        this.textFile = null;
        this.endTutorialTransitionScene = "";
    }

    public void setTutorialImages(List<string> tutorialImagePaths) {
        this.tutorialImages = new List<Sprite>(10);
        foreach (string imagePath in tutorialImagePaths) {
            Sprite path = Resources.Load<Sprite>(imagePath);
            this.tutorialImages.Add(path);
        }
    }

    public void setTextFile(string file) {
        this.textFile = Resources.Load(file) as TextAsset;
    }

    public void setEndTutorialTransitionScene(string endScene) {
        this.endTutorialTransitionScene = endScene;
    }

    public List<Sprite> getTutorialImages() {
        return this.tutorialImages;
    }

    public TextAsset getTextFile() {
        return this.textFile;
    }

    public string getEndTutorialTransitionScene() {
        return this.endTutorialTransitionScene;
    }
}

