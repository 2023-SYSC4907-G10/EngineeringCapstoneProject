using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpooning : MonoBehaviour
{
    public GameObject shooter; 

    public Sprite notSelected;
    public Sprite selected;

    public GameObject spearFishing;
    public GameObject phishing;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = notSelected; 

    }

    void unSelect() {
        gameObject.GetComponent<SpriteRenderer>().sprite = notSelected;
    }

    void OnMouseDown() {
        shooter.SendMessage("selectHarpooning");
        gameObject.GetComponent<SpriteRenderer>().sprite = selected;
        spearFishing.SendMessage("unSelect");
        phishing.SendMessage("unSelect");
    }
}