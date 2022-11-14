using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    // Constants
    public const int MaxReputation = 100;
    public const int MaxOpponentKnowledge = 100;

    // Static singleton
    public static GameManager Instance { get; private set; }


    // Main game fields
    private int _reputation;
    private int _opponentKnowledge;
    private Dictionary<SecurityConcepts, int> _defenseUpgradeLevels;
    private Dictionary<SecurityConcepts, int> _attackSpecificHeat;
    private List<object> _passiveAttackLog;//TODO: Update with passive attack type Enum from HG
    private List<SecurityConcepts> _incommingAttackLog;



    // Update events to subscribe to
    public static event Action<int> OnReputationChange;
    public static event Action<int> OnOpponentKnowledgeChange;
    public static event Action<SecurityConcepts, int> OnDefenseUpgradeLevelsChange;
    public static event Action<SecurityConcepts, int> OnAttackSpecificHeatChange;
    public static event Action<object> OnPassiveAttackLogChange; //TODO: Update with passive attack type Enum from HG
    public static event Action<SecurityConcepts> OnIncommingAttackLogChange;
    /*
        HOW TO SUBSCRIBE TO EVENTS
        
        // Consider this class the observable, to make a different area an observer, do this in an initializer method
        GameManager.OnReputationChange += LocallyDefinedFunction_ParamMatchingAction
        // In this case, LocallyDefinedFunction_ParamMatchingAction(int reputation) would be a defined function

        // Make sure to make an OnDestroy() method to unsubscribe from the event like this 
        GameManager.OnReputationChange -= LocallyDefinedFunction_ParamMatchingAction
    */


    // Called before application starts as the script loads
    void Awake()
    {
        // Destroy instance if it somehow already exists and is not this
        if (Instance != null && Instance != this) { Destroy(this); }
        else { Instance = this; }

        Instance._reputation = 0;
        Instance._opponentKnowledge = 0;
        Instance._defenseUpgradeLevels = new Dictionary<SecurityConcepts, int>();
        Instance._attackSpecificHeat = new Dictionary<SecurityConcepts, int>();
        Instance._passiveAttackLog = new List<>(); //TODO: Update with passive attack type Enum from HG
        Instance._incommingAttackLog = new List<SecurityConcepts>();

        // Iterate thru security concepts to instantiate zeros for defense upgrade and attack heat
        foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
        {
            _defenseUpgradeLevels.Add(concept, 0);
            _attackSpecificHeat.Add(concept, 0);
        }

    }

    /*
        HOW TO CALL THESE METHODS
        GameManager.Instance.SetReputation(20);
        MAKE SURE TO USE THE INSTANCE PROPERTY AS THAT IS THE SINGLE INSTANCE
    */


    // Getters
    public int GetReputation() { return _reputation; }
    public int GetOpponentKnowledge() { return _opponentKnowledge; }
    public Dictionary<SecurityConcepts, int> GetDefenseUpgradeLevels() { return _defenseUpgradeLevels; }
    public Dictionary<SecurityConcepts, int> GetAttackSpecificHeat() { return _attackSpecificHeat; }
    public List<object> GetPassiveAttackLog()
    {
        //TODO: Update with passive attack type Enum from HG
        return _passiveAttackLog;
    }
    public List<SecurityConcepts> GetIncommingAttackLog() { return _incommingAttackLog; }


    // Primitive Setters
    public void SetReputation(int reputation)
    {
        if (reputation > 0 && reputation < MaxReputation)
        {
            this._reputation = reputation;
            OnReputationChange?.Invoke(reputation);
        }
    }
    public void SetOpponentKnowledge(int opponentKnowledge)
    {
        if (opponentKnowledge > 0 && opponentKnowledge < MaxOpponentKnowledge)
        {
            this._opponentKnowledge = opponentKnowledge;
            OnReputationChange?.Invoke(opponentKnowledge);
        }
    }


    // Non primitive updates
    public void UpdateDefenseUpgradeLevel(SecurityConcepts concept, int changeAmount)
    {
        _defenseUpgradeLevels[concept] += changeAmount;
        OnDefenseUpgradeLevelsChange?.Invoke(concept, changeAmount);
    }
    public void UpdateAttackSpecificHeat(SecurityConcepts concept, int changeAmount)
    {
        _attackSpecificHeat[concept] += changeAmount;
        OnAttackSpecificHeatChange?.Invoke(concept, changeAmount);
    }
    public void UpdatePassiveAttackLog(object passiveattack)
    {
        _passiveAttackLog.Add(passiveattack);
        OnPassiveAttackLogChange?.Invoke(passiveattack);
    }
    public void UpdateIncommingAttackLog(SecurityConcepts concept)
    {
        _incommingAttackLog.Add(concept);
        OnIncommingAttackLogChange?.Invoke(concept);
    }
}

