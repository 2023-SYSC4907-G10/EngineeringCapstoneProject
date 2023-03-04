using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWD_PacketSpawner : MonoBehaviour
{
    public GameObject packetPrefab;
    private bool _isPregameState;

    // Start is called before the first frame update
    void Start()
    {
        _isPregameState = FWD_Manager.GetInstance().GetIsPregameState();
        FWD_Manager.OnPregameStateChange += SetIsPregameState;
    }


    // Update is called once per frame
    void Update()
    {
        if (!_isPregameState && Time.frameCount % 300 == 0)
        {
            Instantiate(packetPrefab, this.transform);
        }

    }
    void SetIsPregameState(bool isPregameState)
    {
        _isPregameState = isPregameState;
    }

    void OnDestroy()
    {
        FWD_Manager.OnPregameStateChange -= SetIsPregameState;
    }
}
