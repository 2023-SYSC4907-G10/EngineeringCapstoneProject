using System;
using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class FirewallAttackGameManager
{
    public int StartingHealth { get; private set; }
    private int _currentHealth;
    public int CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = value;
            if (_currentHealth <= 0)
            {
                CurrentGameState = FirewallAttackStates.Lose;
            }
        }
    }
    private float _playStateStartTime;
    private float _playStateEndTime;
    private FirewallAttackStates _currentGameState;
    public FirewallAttackStates CurrentGameState
    {
        get { return _currentGameState; }
        set
        {
            _currentGameState = value;
            if (value == FirewallAttackStates.Playing) _playStateStartTime = Time.time;
            if (value == FirewallAttackStates.Win || value == FirewallAttackStates.Lose) _playStateEndTime = Time.time;
            OnCurrentGameStateChange?.Invoke(value);
        }
    }
    public static event Action<FirewallAttackStates> OnCurrentGameStateChange;
    public static event Action OnFlameTrapCollision;

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

        StartingHealth = 3; // TODO: Make this a difficulty control and get this dynamically
        _currentHealth = StartingHealth;
    }

    public float GetPlayTime()
    {
        if (CurrentGameState == FirewallAttackStates.Win || CurrentGameState == FirewallAttackStates.Lose)
        {
            return _playStateEndTime - _playStateStartTime;
        }
        return -1f;
    }

    public void FlameTrapCollision()
    {
        OnFlameTrapCollision?.Invoke();
    }
}
