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
    private bool isShowingExample;
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
            StartCoroutine(switchTutorial());
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

    IEnumerator switchTutorial() {
        if (Input.GetMouseButtonDown(0) && !isShowingExample) {
            tutorialStateIndex++;
            tutorialStates[tutorialStateIndex-1].SetActive(false); // hide prior tutorial state

            if (tutorialStateIndex < tutorialStates.Length) {
                 tutorialStates[tutorialStateIndex].SetActive(true);
                if (tutorialStates[tutorialStateIndex].name == "TutorialExample") {
                    isShowingExample = true;
                    yield return new WaitForSeconds(5);
                    isShowingExample = false;
                }
            }
        }
    }

    void endTutorial() {
        if (tutorialStateIndex == tutorialStates.Length) {
            tutorialActive = false;
        }
    }
}
