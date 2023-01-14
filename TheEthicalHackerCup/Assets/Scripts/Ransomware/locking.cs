using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class locking : MonoBehaviour
{

    // make it so that the computer changes textures on lock
    // make it so that the computer changes textures while being locked
    // make it so that when the computer is locked, the minigame state is changes

    private float lockedPercent = 0f;
    private bool beingLocked = false;
    public float timeToLock = 3f;
    public Material lockMaterial = null;

    private bool isLocked = false;
    // Start is called before the first frame update
    void Start()
    {
        RansomSingleton.GetInstance().FileCount++;

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerStay(Collider other)
    {
        beingLocked = other.gameObject.CompareTag("Player");
        if (beingLocked && !isLocked)
        {
            lockedPercent += Time.deltaTime / timeToLock;
            if (lockedPercent >= 1)
            {
                lockFile();
            }
        }
    }


    private void lockFile() {
        lockedPercent = 1;
        isLocked = true;
        GetComponent<Renderer>().material = lockMaterial;
        RansomSingleton.GetInstance().FileCount--;
    }

    
}
