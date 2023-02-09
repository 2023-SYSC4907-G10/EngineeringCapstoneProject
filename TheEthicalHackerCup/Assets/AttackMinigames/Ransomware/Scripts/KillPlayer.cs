using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private RansomManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<RansomManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var other = collision.gameObject;
        if (other.CompareTag("Player")) 
        {
            manager.lose();
        }
    }
}
