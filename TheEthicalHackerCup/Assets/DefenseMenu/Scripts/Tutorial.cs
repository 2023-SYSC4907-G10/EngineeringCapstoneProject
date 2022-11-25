using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Boolean tutorialActive;
    [SerializeField] private List<GameObject> tutorialStates;
    private Canvas tutorialCanvas;

    private int tutorialStateIndex;

    // Start is called before the first frame update
    void Start()
    {
        tutorialCanvas = this.gameObject.GetComponent<Canvas>();
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
        }
    }

    void switchTutorial() {
        if (Input.GetMouseButtonDown(0)) {
            tutorialStateIndex++;
            tutorialStates[tutorialStateIndex-1].SetActive(false); // hide prior tutorial state

            if (tutorialStateIndex < tutorialStates.Count) {
                tutorialStates[tutorialStateIndex].SetActive(true);
            }
        }
    }

    void endTutorial() {
        if (tutorialStateIndex == tutorialStates.Count) {
            tutorialActive = false;
        }
    }
}
