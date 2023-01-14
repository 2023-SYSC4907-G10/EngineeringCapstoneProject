using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phishing : MonoBehaviour
{
    public GameObject shooter;

    public Sprite notSelected;
    public Sprite selected;

    public GameObject spearFishing;
    public GameObject harpooning;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = notSelected; 
    }

    void unSelect() {
        gameObject.GetComponent<SpriteRenderer>().sprite = notSelected;
    }

    void OnMouseDown() {
        Debug.Log("Phishing Selected");
        shooter.SendMessage("selectPhishing");
        gameObject.GetComponent<SpriteRenderer>().sprite = selected;
        spearFishing.SendMessage("unSelect");
        harpooning.SendMessage("unSelect");
    }
}