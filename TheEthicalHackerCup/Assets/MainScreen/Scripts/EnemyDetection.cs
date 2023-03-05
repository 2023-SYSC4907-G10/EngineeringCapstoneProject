using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{

    [SerializeField] private Renderer rend;
    [SerializeField] private Material notDetected;
    [SerializeField] private Material detected;

    private void OnTriggerEnter(Collider coll) {
        if (coll.gameObject.layer == LayerMask.NameToLayer("PlayerLayer")) {
            rend.material = detected;
            coll.SendMessage("playerDetected");
        }
    }

    private void OnTriggerExit(Collider coll) {
        if (coll.gameObject.layer == LayerMask.NameToLayer("PlayerLayer")) {
            rend.material = notDetected;
        }
    }

}
