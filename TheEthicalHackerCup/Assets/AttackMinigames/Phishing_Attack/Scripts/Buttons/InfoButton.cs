using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoButton : MonoBehaviour
{
    public GameObject dialog;

    void OnMouseDown() {
        dialog.GetComponent<SpriteRenderer>().enabled = true;
        dialog.GetComponent<BoxCollider2D>().enabled = true;
    }
}
