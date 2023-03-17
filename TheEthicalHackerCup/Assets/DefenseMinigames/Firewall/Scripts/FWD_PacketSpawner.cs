using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWD_PacketSpawner : MonoBehaviour
{
    public GameObject packetPrefab;
    private bool _isPregameState;

    private float _timeUntilNextSpawn; 
    private readonly float SPAWN_PERIOD = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _timeUntilNextSpawn = SPAWN_PERIOD;
        _isPregameState = FWD_Manager.GetInstance().GetIsPregameState();
        FWD_Manager.OnPregameStateChange += SetIsPregameState;
    }


    // Update is called once per frame
    void Update()
    {
        _timeUntilNextSpawn -= Time.deltaTime;
        if (!_isPregameState && _timeUntilNextSpawn <= 0)
        {
            _timeUntilNextSpawn = SPAWN_PERIOD;
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
