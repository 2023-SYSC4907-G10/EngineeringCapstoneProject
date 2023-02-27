using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWD_DefenseManager : MonoBehaviour
{
    public GameObject DeviceFirewalls;
    public GameObject NetworkFirewalls;
    public GameObject BetterDeviceFirewalls;

    // Start is called before the first frame update
    void Start()
    {
        FWD_Manager.GetInstance().InitializeGameState();
        int difficultyLevel = FWD_Manager.GetInstance().DifficultyLevel.DifficultyLevel;

        if (difficultyLevel <= 0)
        {
            DeviceFirewalls.SetActive(false);
            NetworkFirewalls.SetActive(false);
            BetterDeviceFirewalls.SetActive(false);
        }
        else if (difficultyLevel == 1)
        {
            DeviceFirewalls.SetActive(true);
            NetworkFirewalls.SetActive(false);
            BetterDeviceFirewalls.SetActive(false);
        }
        else if (difficultyLevel == 2)
        {
            DeviceFirewalls.SetActive(true);
            NetworkFirewalls.SetActive(true);
            BetterDeviceFirewalls.SetActive(false);
        }
        else
        {
            DeviceFirewalls.SetActive(false);
            NetworkFirewalls.SetActive(true);
            BetterDeviceFirewalls.SetActive(true);
        }

    }
}
