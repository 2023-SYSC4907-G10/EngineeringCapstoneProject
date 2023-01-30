using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private Canvas tutorialCanvas;

    [SerializeField] private GameObject tutorialSpeech;
    [SerializeField] private GameObject tutorialPointer;
    [SerializeField] private TextAsset pointerConfig;
    private string[] pointerLines;
    private int pointerIndex;
    private string pointer;
    private GameObject tutorialPointerCopy;

    // Start is called before the first frame update
    void Start()
    {
        tutorialCanvas = this.gameObject.GetComponent<Canvas>();
        tutorialSpeech = Instantiate(tutorialSpeech, transform);
        tutorialSpeech.transform.localPosition = new Vector3(-10, -80, 0);

        pointerLines = pointerConfig.text.Split('\n');
        pointerIndex = 0;
        
        pointer = pointerLines[pointerIndex];
        if (pointer.Trim() != "") {
            tutorialPointerCopy = Instantiate(tutorialPointer, transform);
            tutorialPointerCopy.transform.localPosition = getPointerPosition(pointer);
            tutorialPointerCopy.transform.localRotation = getPointerRotation(pointer);
        }
        pointerIndex += 1;
    }

    // Update is called once per frame
    void Update()
    {
        tutorialCanvas.enabled = !TutorialTextTyper.isTutorialOver();
        if (Input.GetMouseButtonDown(0) && pointerIndex != pointerLines.Length) {
            Destroy(tutorialPointerCopy);
            pointer = pointerLines[pointerIndex];
            if (pointer.Trim() != "") {
                tutorialPointerCopy = Instantiate(tutorialPointer, transform);
                tutorialPointerCopy.transform.localPosition = getPointerPosition(pointer);
                tutorialPointerCopy.transform.localRotation = getPointerRotation(pointer);
            }
            pointerIndex += 1;
        }
    }

    Vector3 getPointerPosition(string pointer) {
        string pointerConfig = pointer.Split(' ')[0];
        string[] position = pointerConfig.Split(',');
        float x = float.Parse(position[0]);
        float y = float.Parse(position[1]);
        float z = float.Parse(position[2]);
        return new Vector3(x,y,z);
    }

    Quaternion getPointerRotation(string pointer) {
        string pointerConfig = pointer.Split(' ')[1];
        string[] position = pointerConfig.Split(',');
        float x = float.Parse(position[0]);
        float y = float.Parse(position[1]);
        float z = float.Parse(position[2]);
        return Quaternion.Euler(x,y,z);
    }
}
