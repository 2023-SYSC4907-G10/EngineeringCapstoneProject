using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFinishLine : MonoBehaviour
{
    public GameObject finishLine;
    public float timeUntilSpawn;
    private bool hasFinishLineSpawnedYet;

    void Start()
    {
        timeUntilSpawn = 10f;
        hasFinishLineSpawnedYet = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;
        if (timeUntilSpawn <= 0 && hasFinishLineSpawnedYet == false)
        {
            Spawn();
            hasFinishLineSpawnedYet = true;
        }
    }

    void Spawn()
    {
        Instantiate(finishLine, transform.position + new Vector3(1, 1, 0), transform.rotation);
    }

}
