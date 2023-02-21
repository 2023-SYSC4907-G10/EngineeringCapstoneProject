using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWD_PacketSpawner : MonoBehaviour
{
    public GameObject packetPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 180 == 0)
        {
            Instantiate(packetPrefab, this.transform);
        }

    }
}
