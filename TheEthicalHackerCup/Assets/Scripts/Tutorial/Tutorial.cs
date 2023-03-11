using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    private List<Sprite> tutorialImages;
    private Image image;

    private int tutorialStateIndex;

    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        tutorialStateIndex = 0;
        tutorialImages = TutorialSingleton.GetInstance().getTutorialImages();
    }

    // Update is called once per frame
    void Update()
    {
        initialTutorial();
        switchTutorial();
        endTutorial();
    }

    void initialTutorial()
    {
        if (tutorialStateIndex == 0)
        {
            image.sprite = tutorialImages[tutorialStateIndex];
        }
    }

    void switchTutorial()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tutorialStateIndex++;
            if (tutorialStateIndex < tutorialImages.Count)
            {
                image.sprite = tutorialImages[tutorialStateIndex];
            }
        }
    }

    void endTutorial()
    {
        if (tutorialStateIndex == tutorialImages.Count)
        {
            string endScene = TutorialSingleton.GetInstance().getEndTutorialTransitionScene();
            SceneManager.LoadScene(endScene);
        }
    }
}
