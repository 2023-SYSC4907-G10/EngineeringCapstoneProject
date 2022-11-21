using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : ScriptableObject
{
    // Constants
    public const int MaxReputation = 100;
    public const int MaxOpponentKnowledge = 100;
    public const int MaxHeat = 100;

    // Static singleton
    public static GameManager Instance;



    // Main game fields (Not subscribable)
    private string _nextLearningMinigameFilename;

    // Main game fields (subscribable)
    private int _reputation;
    private int _opponentKnowledge;
    private Dictionary<SecurityConcepts, int> _defenseUpgradeLevels;
    private Dictionary<SecurityConcepts, int> _attackMinigamesAttempted;
    private Dictionary<SecurityConcepts, int> _attackSpecificHeat;
    private List<SecurityConcepts> _incomingAttackLog;// REVISIT THIS WHEN ADDRESSING INCOMMING ATTACK FREQUENCY



    // Update events to subscribe to
    public static event Action<int> OnReputationChange;
    public static event Action<int> OnOpponentKnowledgeChange;
    public static event Action<SecurityConcepts, int> OnDefenseUpgradeLevelsChange;
    public static event Action<SecurityConcepts, int> OnAttackMinigameAttemptChange;
    public static event Action<SecurityConcepts, int> OnAttackSpecificHeatChange;
    public static event Action<SecurityConcepts> OnIncomingAttackLogChange;
    /*
        HOW TO SUBSCRIBE TO EVENTS
        
        // Consider this class the observable, to make a different area an observer, do this in an initializer method
        GameManager.OnReputationChange += LocallyDefinedFunction_ParamMatchingAction
        // In this case, LocallyDefinedFunction_ParamMatchingAction(int reputation) would be a defined function

        // Make sure to make an OnDestroy() method to unsubscribe from the event like this 
        GameManager.OnReputationChange -= LocallyDefinedFunction_ParamMatchingAction

        NOTE THAT OBSERVERS ARE ADDED TO THE BASE CLASS AND NOT TO THE INSTANCE
    */


    // Called before application starts as the script loads
    void Awake()
    {
        // Destroy instance if it somehow already exists and is not this
        if (Instance != null && Instance != this) { Destroy(this); }
        else { Instance = this; }

        InitializeGameState();
    }

    public void InitializeGameState()
    {
        _nextLearningMinigameFilename = "";
        _reputation = 0;
        _opponentKnowledge = 0;
        _defenseUpgradeLevels = new Dictionary<SecurityConcepts, int>();
        _attackMinigamesAttempted = new Dictionary<SecurityConcepts, int>();
        _attackSpecificHeat = new Dictionary<SecurityConcepts, int>();
        _incomingAttackLog = new List<SecurityConcepts>();// REVISIT THIS WHEN ADDRESSING INCOMMING ATTACK FREQUENCY

        // Iterate thru security concepts to instantiate zeros for defense upgrade and attack heat
        foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
        {
            _defenseUpgradeLevels.Add(concept, 0);
            _attackMinigamesAttempted.Add(concept, 0);
            _attackSpecificHeat.Add(concept, 0);
        }
    }



    /*
        HOW TO CALL THESE METHODS
        GameManager.Instance.SetReputation(20);
        MAKE SURE TO USE THE INSTANCE PROPERTY AS THAT IS THE SINGLE INSTANCE
    */
    // Getters
    public string GetNextLearningMinigameFilename() { return _nextLearningMinigameFilename; }
    public int GetReputation() { return _reputation; }
    public int GetOpponentKnowledge() { return _opponentKnowledge; }
    public Dictionary<SecurityConcepts, int> GetDefenseUpgradeLevels() { return _defenseUpgradeLevels; }
    public Dictionary<SecurityConcepts, int> GetAttackMinigamesAttempted() { return _attackMinigamesAttempted; }
    public Dictionary<SecurityConcepts, int> GetAttackSpecificHeat() { return _attackSpecificHeat; }
    public List<SecurityConcepts> GetIncommingAttackLog() { return _incomingAttackLog; }// REVISIT THIS WHEN ADDRESSING INCOMMING ATTACK FREQUENCY


    // Primitive Setters
    public void SetNextLearningMinigameFilename(string filename) { this._nextLearningMinigameFilename = filename; }
    public void SetReputation(int reputation)
    {
        if (reputation >= 0 && reputation < MaxReputation)
        {
            this._reputation = reputation;
            OnReputationChange?.Invoke(reputation);
        }
    }
    public void SetOpponentKnowledge(int opponentKnowledge)
    {
        if (opponentKnowledge >= 0 && opponentKnowledge < MaxOpponentKnowledge)
        {
            this._opponentKnowledge = opponentKnowledge;
            OnOpponentKnowledgeChange?.Invoke(opponentKnowledge);
        }
    }


    // Non primitive updates
    public void UpgradeDefenseUpgradeLevel(SecurityConcepts concept)
    {
        this._defenseUpgradeLevels[concept] += 1;
        OnDefenseUpgradeLevelsChange?.Invoke(concept, this._defenseUpgradeLevels[concept]);
    }
    public void AttemptAttackMinigame(SecurityConcepts concept)
    {
        this._attackMinigamesAttempted[concept] += 1;
        OnAttackMinigameAttemptChange?.Invoke(concept, this._attackMinigamesAttempted[concept]);
    }
    public void ChangeAttackSpecificHeat(SecurityConcepts concept, int changeAmount)
    {
        if (this._attackSpecificHeat[concept] + changeAmount >= 0 && this._attackSpecificHeat[concept] + changeAmount < MaxHeat)
        {
            this._attackSpecificHeat[concept] += changeAmount;
            OnAttackSpecificHeatChange?.Invoke(concept, this._attackSpecificHeat[concept]);
        }
    }
    public void UpdateIncommingAttackLog(SecurityConcepts concept)
    {
        // REVISIT THIS WHEN ADDRESSING INCOMMING ATTACK FREQUENCY
        this._incomingAttackLog.Add(concept);
        OnIncomingAttackLogChange?.Invoke(concept);
    }


    // Changers
    public void ChangeReputation(int change) { this.SetReputation(this._reputation + change); }
    public void ChangeOpponentKnowledge(int change) { this.SetOpponentKnowledge(this._opponentKnowledge + change); }
}

