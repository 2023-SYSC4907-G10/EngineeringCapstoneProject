using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWD_PacketSpawner : MonoBehaviour
{
    public GameObject packetPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(packetPrefab, this.transform);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
