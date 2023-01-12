using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpooning : MonoBehaviour
{
    public GameObject shooter; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown() {
        Debug.Log("Harpooning Selected");
        shooter.SendMessage("selectHarpooning");
    }
}