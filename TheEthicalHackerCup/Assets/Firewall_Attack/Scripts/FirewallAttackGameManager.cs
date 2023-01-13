using System;
using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class FirewallAttackGameManager
{
    private FirewallAttackStates _currentGameState;
    public FirewallAttackStates CurrentGameState
    {
        get { return _currentGameState; }
        set
        {
            _currentGameState = value;
            OnCurrentGameStateChange.Invoke(value);
        }
    }
    public static event Action<FirewallAttackStates> OnCurrentGameStateChange;

    // Static singleton
    private static FirewallAttackGameManager _instance;

    public static FirewallAttackGameManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new FirewallAttackGameManager();
            _instance.InitializeGameState();
        }
        return _instance;
    }

    public void InitializeGameState()
    {
        CurrentGameState = FirewallAttackStates.Intro;
        Debug.Log("Initializing firewall attack game manager");
    }
}
