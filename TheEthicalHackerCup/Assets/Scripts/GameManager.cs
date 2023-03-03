using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    // Constants
    public static readonly SecurityConcepts[] With3Upgrades =
    { // Currently based on proposal. Those not in here have 4 upgrades
        SecurityConcepts.DDoS,
        SecurityConcepts.Firewall,
        SecurityConcepts.Ransomware,
    };

    public const int MaxReputation = 100;
    public const int MaxOpponentKnowledge = 100;
    public const int MaxHeat = 100;

    // Static singleton
    private static GameManager _instance;

    public static GameManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GameManager();
            _instance.InitializeGameState();
        }
        return _instance;
    }

    private GameManager() { } // Private constructor so new instances can't be made


    // Main game fields (Not subscribable)
    private SecurityConcepts _nextLearningMinigameSecurityConcept;
    private string _nextLearningMinigameTutorialFileName;
    private string _postLearningMinigameReturnScene;

    private string _afterActionReportText;
    public string AfterActionReportText
    {
        get { return _afterActionReportText; }
        set
        {
            Debug.LogWarning("DEPRECATED: Please replace GameManager.GetInstance().AfterActionReportText = \"...\" \nwith GameManager.GetInstance().SwitchToAfterActionReportScene(...)");
            SwitchToAfterActionReportScene(value);
        }
    }

    // Main game fields (subscribable)
    private int _reputation;
    private int _opponentKnowledge;
    private Dictionary<SecurityConcepts, SecurityConceptProgress> _securityConceptProgressDictionary;
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

    public void InitializeGameState()
    {
        _afterActionReportText = "Sample after action report text";
        _nextLearningMinigameSecurityConcept = SecurityConcepts.Firewall; //Default but will not be used before getting rewritten
        _reputation = 0;
        _opponentKnowledge = 0;
        _securityConceptProgressDictionary = new Dictionary<SecurityConcepts, SecurityConceptProgress>();
        _nextLearningMinigameTutorialFileName = "";
        _postLearningMinigameReturnScene = "MainScene";

        _incomingAttackLog = new List<SecurityConcepts>();// REVISIT THIS WHEN ADDRESSING INCOMMING ATTACK FREQUENCY

        // Iterate thru security concepts to instantiate zeros for defense upgrade and attack heat
        foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
        {
            int currentMaxUpgrade = Array.Exists(With3Upgrades, sc => sc == concept) ? 3 : 4;
            _securityConceptProgressDictionary.Add(concept, new SecurityConceptProgress(currentMaxUpgrade));
        }
    }



    /*
        HOW TO CALL THESE METHODS
        GameManager.GetInstance().SetReputation(20);
        MAKE SURE TO USE THE INSTANCE PROPERTY AS THAT IS THE SINGLE INSTANCE
    */
    // Getters
    public string GetNextLearningMinigameFilename()
    {
        // Returns the filename only without the .xml extension
        if (_nextLearningMinigameTutorialFileName == "")
        {
            return _nextLearningMinigameSecurityConcept + GetAttackMinigamesAttemptsRequiredToUpgrade(_nextLearningMinigameSecurityConcept).ToString();
        }
        else
        {
            return _nextLearningMinigameTutorialFileName;
        }
    }
    public int GetReputation() { return _reputation; }
    public int GetOpponentKnowledge() { return _opponentKnowledge; }
    public Dictionary<SecurityConcepts, SecurityConceptProgress> GetSecurityConceptProgressDictionary() { return _securityConceptProgressDictionary; }
    public int GetDefenseUpgradeLevel(SecurityConcepts concept)
    {
        return _securityConceptProgressDictionary[concept].GetCurrentDefenseUpgradeLevel();
    }
    public int GetMaxDefenseUpgradeLevel(SecurityConcepts concept)
    {
        return _securityConceptProgressDictionary[concept].GetMaxDefenseUpgradeLevel();
    }
    public int GetAttackMinigamesAttempted(SecurityConcepts concept)
    {
        return _securityConceptProgressDictionary[concept].GetAttackMinigamesAttempted();
    }
    public int GetAttackMinigamesAttemptsRequiredToUpgrade(SecurityConcepts concept)
    {
        return _securityConceptProgressDictionary[concept].GetAttackMinigameAttemptsRequiredToUpgrade();
    }
    public int GetAttackSpecificHeat(SecurityConcepts concept)
    {
        return _securityConceptProgressDictionary[concept].GetHeat();
    }
    public List<SecurityConcepts> GetIncommingAttackLog() { return _incomingAttackLog; }// REVISIT THIS WHEN ADDRESSING INCOMMING ATTACK FREQUENCY


    // Boolean indicators
    public bool IsEverythingFullyUpgraded()
    {
        foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
        {
            if (!_securityConceptProgressDictionary[concept].IsFullyUpgraded())
            {
                return false;
            }
        }
        return true;
    }


    // Primitive Setters

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

    public void SwitchToAfterActionReportScene(string afterActionReportText)
    {
        _afterActionReportText = afterActionReportText;
        SceneManager.LoadScene("AfterActionReport");
    }


    // Non primitive updates
    public bool UpgradeDefenseUpgradeLevel(SecurityConcepts concept)
    {
        if (this._securityConceptProgressDictionary[concept].UpgradeDefense())
        {
            // Upgraded successfully returns true and invokes the action event
            OnDefenseUpgradeLevelsChange?.Invoke(concept, this._securityConceptProgressDictionary[concept].GetCurrentDefenseUpgradeLevel());
            return true;
        }
        return false;
    }
    public void AttemptAttackMinigame(SecurityConcepts concept)
    {
        this._securityConceptProgressDictionary[concept].AttemptAttackMinigame();
        OnAttackMinigameAttemptChange?.Invoke(concept, this._securityConceptProgressDictionary[concept].GetAttackMinigamesAttempted());
    }
    public void ChangeAttackSpecificHeat(SecurityConcepts concept, int changeAmount)
    {
        if (this._securityConceptProgressDictionary[concept].ChangeHeat(changeAmount))
        {
            OnAttackSpecificHeatChange?.Invoke(concept, this._securityConceptProgressDictionary[concept].GetHeat());
        }
    }
    public void UpdateIncommingAttackLog(SecurityConcepts concept)
    {
        // REVISIT THIS WHEN ADDRESSING INCOMMING ATTACK FREQUENCY
        this._incomingAttackLog.Add(concept);
        OnIncomingAttackLogChange?.Invoke(concept);
    }

    public void StartLearningMinigameUpgradeQuiz(SecurityConcepts nextLearningMinigameSecurityConcept)
    {
        this._nextLearningMinigameTutorialFileName = ""; //This will be used as the condition to determine if using tutorial or sec concept files
        this._postLearningMinigameReturnScene = "MainScene";
        this._nextLearningMinigameSecurityConcept = nextLearningMinigameSecurityConcept;
        SceneManager.LoadScene("LearningScene");
    }

    public void StartLearningMinigameTutorial(string tutorialFileName)
    {
        this._nextLearningMinigameTutorialFileName = tutorialFileName;
        this._postLearningMinigameReturnScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("LearningScene");
    }

    public void EndLearningMinigame(bool quizPassed)
    {
        if (quizPassed)
        {
            UpgradeDefenseUpgradeLevel(_nextLearningMinigameSecurityConcept);
        }
        SceneManager.LoadScene(this._postLearningMinigameReturnScene);
    }


    // Changers
    public void ChangeReputation(int change) { this.SetReputation(this._reputation + change); }
    public void ChangeOpponentKnowledge(int change) { this.SetOpponentKnowledge(this._opponentKnowledge + change); }
}

