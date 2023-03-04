using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FWD_Manager
{
    // <Singleton Operations>
    // Static singleton
    private static FWD_Manager _instance;
    private FWD_Manager() { } // Private constructor so new instances can't be made

    public static FWD_Manager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new FWD_Manager();
            _instance.InitializeGameState();
        }
        return _instance;
    }
    // </Singleton Operations>


    public static event Action<bool> OnPregameStateChange;
    public static event Action<int> OnBadPacketsReceivedChange;
    public static event Action<int> OnBurnedGoodPacketsChange;

    public FWD_WaypointManager WaypointManager { get; set; }
    public FWD_DifficultyLevel DifficultyLevel { get; private set; }
    private bool _isPregameState;
    private int _goodPacketsBurned;
    private int _badPacketsReceived;

    public void InitializeGameState()
    {
        DifficultyLevel = new FWD_DifficultyLevel(GameManager.GetInstance().GetDefenseUpgradeLevel(SecurityConcepts.Firewall));
        WaypointManager = null;
        _goodPacketsBurned = 0;
        _badPacketsReceived = 0;
        _isPregameState = true;
    }

    public void BurnGoodPacket()
    {
        _goodPacketsBurned++;
        OnBurnedGoodPacketsChange?.Invoke(_goodPacketsBurned);
    }
    public void ReceivedBadPacket()
    {
        _badPacketsReceived++;
        OnBadPacketsReceivedChange?.Invoke(_badPacketsReceived);
    }

    public void StartGame()
    {
        this._isPregameState = false;
        OnPregameStateChange?.Invoke(false);
    }
    public void EndGame()
    {
        this._isPregameState = true;
        OnPregameStateChange?.Invoke(true);
    }

    public bool GetIsPregameState() { return _isPregameState; }
}