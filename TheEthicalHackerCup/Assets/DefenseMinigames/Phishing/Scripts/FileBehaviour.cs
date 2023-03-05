using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject phishingEmails;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            collision.gameObject.SendMessage("goToSelectionAreaAndFeeze");
            phishingEmails.SendMessage("foundFile");
        }
    }
}
