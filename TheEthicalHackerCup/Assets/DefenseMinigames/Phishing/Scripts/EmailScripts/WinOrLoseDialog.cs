using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOrLoseDialog : MonoBehaviour
{

    [SerializeField] private GameObject correctPhishing;
    [SerializeField] private GameObject correctNotPhishing;
    [SerializeField] private GameObject incorrectPhishing;
    [SerializeField] private GameObject incorrectNotPhishing;

    private SpriteRenderer correctPhishingRend;
    private SpriteRenderer correctNotPhishingRend;
    private SpriteRenderer incorrectPhishingRend;
    private SpriteRenderer incorrectNotPhishingRend;

    // Start is called before the first frame update
    void Start()
    {
        correctPhishingRend = correctPhishing.GetComponent<SpriteRenderer>();
        correctNotPhishingRend = correctNotPhishing.GetComponent<SpriteRenderer>();
        incorrectPhishingRend = incorrectPhishing.GetComponent<SpriteRenderer>();
        incorrectNotPhishingRend = incorrectNotPhishing.GetComponent<SpriteRenderer>();
    }

    void hideDialogs() {
        correctPhishingRend.enabled = false;
        correctNotPhishingRend.enabled = false;
        incorrectPhishingRend.enabled = false;
        incorrectNotPhishingRend.enabled = false;
    }

    void showCorrectPhishing() {
        correctPhishingRend.enabled = true;
    }

    void showCorrectNotPhishing() {
        correctNotPhishingRend.enabled = true;
    }

    void showIncorrectPhishing() {
        incorrectPhishingRend.enabled = true;
    }

    void showIncorrectNotPhishing() {
        incorrectNotPhishingRend.enabled = true;
    }
}
