using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killPlayer : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        var hit = other.gameObject.CompareTag("Player");

        if (hit)
            RansomSingleton.GetInstance().lose();
    }
}
