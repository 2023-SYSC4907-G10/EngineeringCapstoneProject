using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearPhishingScript : MonoBehaviour
{
    public GameObject shooter; 

    public Sprite notSelected;
    public Sprite selected;

    public GameObject phishing;
    public GameObject harpooning;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = selected; 
    }

    void unSelect() {
        gameObject.GetComponent<SpriteRenderer>().sprite = notSelected;
    }


    void OnMouseDown() {
        shooter.SendMessage("selectSpearFishing");
        gameObject.GetComponent<SpriteRenderer>().sprite = selected;
        phishing.SendMessage("unSelect");
        harpooning.SendMessage("unSelect");
    }
}