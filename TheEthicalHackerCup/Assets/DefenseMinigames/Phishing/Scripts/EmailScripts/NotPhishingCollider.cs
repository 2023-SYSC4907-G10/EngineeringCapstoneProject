using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotPhishingCollider : MonoBehaviour
{

    [SerializeField] private GameObject phishingEmails;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            phishingEmails.SendMessage("playerChoice", false);
        }
    }
}
