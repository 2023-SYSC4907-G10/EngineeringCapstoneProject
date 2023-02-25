using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailHandler : MonoBehaviour
{
    [SerializeField] private GameObject email;
    [SerializeField] private GameObject emailAnnotated;
    [SerializeField] private GameObject winOrLoseDialog;

    [SerializeField] private GameObject phishingEmails;

    private bool correctIsPhishingAnswer = true;

    private SpriteRenderer rend;
    private SpriteRenderer rendAnnotated;
    // Start is called before the first frame update
    void Start()
    {
        rend = email.GetComponent<SpriteRenderer>();
        rendAnnotated = emailAnnotated.GetComponent<SpriteRenderer>();
    }

    void foundFile() {
        rend.enabled = true;
    }

    void clearEmail() {
        rend.enabled = false;
        rendAnnotated.enabled = false;
    }

    void showAnnotatedEmail() {
        rendAnnotated.enabled = true;
    }

    void showWinOrLoseScreen(bool isPhishing) {
        if (isPhishing == correctIsPhishingAnswer) {
            // Correct

            if (correctIsPhishingAnswer) {
                // You were right it was phishing
                winOrLoseDialog.SendMessage("showCorrectPhishing");
                phishingEmails.SendMessage("requiresAnnotation");
            } else {
                // You were right it was not phishing
                winOrLoseDialog.SendMessage("showCorrectNotPhishing");
            }
            
        } else {
            // Wrong
            if (correctIsPhishingAnswer) {
                // Sorry that was in fact a phishing email
                winOrLoseDialog.SendMessage("showIncorrectPhishing");
                phishingEmails.SendMessage("requiresAnnotation");
            } else {
                // Sorry that was not a phishing email
                winOrLoseDialog.SendMessage("showIncorrectNotPhishing");
            }
        }
    }

    
}
