using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Boolean tutorialActive;
    [SerializeField] private Canvas tutorialCanvas;

    [SerializeField] private GameObject[] tutorialStates;
    private int tutorialStateIndex;
    // Start is called before the first frame update
    void Start()
    {
        tutorialCanvas = GetComponent<Canvas> ();
        tutorialStateIndex = 0;
        hideTutorialScenes();
        
    }

    // Update is called once per frame
    void Update()
    {
        tutorialCanvas.enabled = tutorialActive;
        if (tutorialActive) {
            initialTutorial();
            switchTutorial();
            endTutorial();
        }
    }

    void hideTutorialScenes() {
        foreach (GameObject i in tutorialStates) {
            i.SetActive(false);
        }
    }

    void initialTutorial() {
        if (tutorialStateIndex == 0) {
            tutorialStates[0].SetActive(true);
            tutorialStateIndex++;
        }
    }

    void switchTutorial() {
        if (Input.GetMouseButtonDown(0)) {
            tutorialStates[tutorialStateIndex-1].SetActive(false); // hide prior tutorial state

            if (tutorialStateIndex < tutorialStates.Length) {
                 tutorialStates[tutorialStateIndex].SetActive(true);
            }
            tutorialStateIndex++;
        }
    }

    void endTutorial() {
        if (tutorialStateIndex == tutorialStates.Length + 1) {
            tutorialActive = false;
        }
    }
}
