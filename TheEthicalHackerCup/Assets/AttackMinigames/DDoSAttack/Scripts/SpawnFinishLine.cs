using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFinishLine : MonoBehaviour
{
    public GameObject finishLine;
    public int frameUntilSpawn;
    private int startFrame;

    void Start()
    {
        startFrame = Time.frameCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.fixedTime == startFrame + frameUntilSpawn)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Instantiate(finishLine, transform.position + new Vector3(1, 1 , 0), transform.rotation);
    }

}
