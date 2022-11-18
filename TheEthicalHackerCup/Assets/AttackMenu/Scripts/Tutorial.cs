using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[Serializable]
public class TutorialState {
  public GameObject tutorialState;
  public string tutorialText;
}

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Boolean tutorialActive;
    [SerializeField] private Canvas tutorialCanvas;
    [SerializeField] private List<TutorialState> tutorialStates;

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
        foreach (TutorialState i in tutorialStates) {
            i.tutorialState.SetActive(false);
        }
    }

    void autoText() {
        GameObject tutorialSpeechObj = tutorialStates[tutorialStateIndex].tutorialState.transform.Find("TutorialSpeech").gameObject;
        TextMeshProUGUI tutorialTextObj = tutorialSpeechObj.transform.Find("TutorialTextBackground/TutorialText").GetComponent<TMPro.TextMeshProUGUI>();
        AutoText.TypeText(tutorialTextObj, tutorialStates[tutorialStateIndex].tutorialText, 2.5f);
    }

    void initialTutorial() {
        if (tutorialStateIndex == 0 && !AutoText.autoTextTyping) {
            tutorialStates[0].tutorialState.SetActive(true);
           
            autoText();
        }
    }

    IEnumerator switchTutorial() {
        if (Input.GetMouseButtonDown(0) && !isShowingExample) {
            tutorialStateIndex++;
            tutorialStates[tutorialStateIndex-1].tutorialState.SetActive(false); // hide prior tutorial state

            if (tutorialStateIndex < tutorialStates.Count) {
                tutorialStates[tutorialStateIndex].tutorialState.SetActive(true);
                if (tutorialStates[tutorialStateIndex].tutorialState.name == "TutorialExample") {
                    isShowingExample = true;
                    yield return new WaitForSeconds(2);
                    isShowingExample = false;
                } else {
                    autoText();
                }
            }
        }
    }

    void endTutorial() {
        if (tutorialStateIndex == tutorialStates.Count) {
            tutorialActive = false;
        }
    }
}
