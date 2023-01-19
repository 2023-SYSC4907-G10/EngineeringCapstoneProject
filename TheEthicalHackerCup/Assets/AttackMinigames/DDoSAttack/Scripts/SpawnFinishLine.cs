using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFinishLine : MonoBehaviour
{
    public GameObject finishLine;
    public float spawnTime;

    // Update is called once per frame
    void Update()
    {
        if(Time.fixedTime == spawnTime){
            Spawn();
        }
    }

    void Spawn(){
        Instantiate(finishLine, transform.position + new Vector3(1, 1 , 0), transform.rotation);
    }

}
