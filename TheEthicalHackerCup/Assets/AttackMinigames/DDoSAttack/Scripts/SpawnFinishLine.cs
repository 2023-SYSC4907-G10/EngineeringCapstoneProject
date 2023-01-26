using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFinishLine : MonoBehaviour
{
    public GameObject finishLine;
    public int timeUntilSpawn;
    private int startFrame;
    private bool hasFinishLineSpawnedYet;

    //TODO: Initialize timeUntilSpawn in Start() method
    void Start()
    {
        startFrame = Time.frameCount;
        hasFinishLineSpawnedYet = false;
        

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.fixedTime == startFrame + timeUntilSpawn && hasFinishLineSpawnedYet == false)
        {
            Spawn();
            hasFinishLineSpawnedYet = true;
        }
    }

    void Spawn()
    {
        Instantiate(finishLine, transform.position + new Vector3(1, 1 , 0), transform.rotation);
    }

}
