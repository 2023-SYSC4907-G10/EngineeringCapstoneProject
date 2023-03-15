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

    public const int MAX_RESPECT = 100;
    public const int MAX_OPP_KNOWLEDGE = 100;
    public const int MAX_HEAT = 100;

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
    private string _playerEmail;

    public string AfterActionReportText { get { return _afterActionReportText; } }

    // Main game fields (subscribable)
    private int _respect;
    private int _opponentKnowledge;
    private Dictionary<SecurityConcepts, SecurityConceptProgress> _securityConceptProgressDictionary;
    private List<SecurityConcepts> _currentDefenseMinigameOptions;

    // Update events to subscribe to
    public static event Action<int> OnRespectChange;
    public static event Action<int> OnOpponentKnowledgeChange;
    public static event Action<SecurityConcepts, int> OnDefenseUpgradeLevelsChange;
    public static event Action<SecurityConcepts, int> OnAttackMinigameAttemptChange;
    public static event Action<SecurityConcepts, int> OnAttackSpecificHeatChange;

    public void InitializeGameState()
    {
        _afterActionReportText = "Sample after action report text";
        _nextLearningMinigameSecurityConcept = SecurityConcepts.Firewall; //Default but will not be used before getting rewritten
        _respect = 25;
        _opponentKnowledge = 0;
        _securityConceptProgressDictionary = new Dictionary<SecurityConcepts, SecurityConceptProgress>();
        _nextLearningMinigameTutorialFileName = "";
        _postLearningMinigameReturnScene = "MainScene";
        _playerEmail = "";
        _currentDefenseMinigameOptions = new List<SecurityConcepts>();

        // Iterate thru security concepts to instantiate zeros for defense upgrade and attack heat
        foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
        {
            _currentDefenseMinigameOptions.Add(concept);
            int currentMaxUpgrade = Array.Exists(With3Upgrades, sc => sc == concept) ? 3 : 4;
            _securityConceptProgressDictionary.Add(concept, new SecurityConceptProgress(currentMaxUpgrade));
        }
    }


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
    public int GetRespect() { return _respect; }
    public int GetOpponentKnowledge() { return _opponentKnowledge; }
    public Dictionary<SecurityConcepts, SecurityConceptProgress> GetSecurityConceptProgressDictionary() { return _securityConceptProgressDictionary; }
    public int GetDefenseUpgradeLevel(SecurityConcepts concept) { return _securityConceptProgressDictionary[concept].GetCurrentDefenseUpgradeLevel(); }
    public int GetMaxDefenseUpgradeLevel(SecurityConcepts concept) { return _securityConceptProgressDictionary[concept].GetMaxDefenseUpgradeLevel(); }
    public int GetAttackMinigamesAttempted(SecurityConcepts concept) { return _securityConceptProgressDictionary[concept].GetAttackMinigamesAttempted(); }
    public int GetAttackMinigamesAttemptsRequiredToUpgrade(SecurityConcepts concept) { return _securityConceptProgressDictionary[concept].GetAttackMinigameAttemptsRequiredToUpgrade(); }
    public int GetAttackSpecificHeat(SecurityConcepts concept) { return _securityConceptProgressDictionary[concept].GetHeat(); }
    public string GetPlayerEmail() { return this._playerEmail; }

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

    public SecurityConcepts GetNextDefenseMinigame()
    {
        // Try to get least upgraded
        int currentLowestUpgradeLevel = _securityConceptProgressDictionary[_currentDefenseMinigameOptions[0]].GetCurrentDefenseUpgradeLevel();

        foreach (SecurityConcepts concept in _currentDefenseMinigameOptions)
        {
            currentLowestUpgradeLevel = Math.Min(currentLowestUpgradeLevel, _securityConceptProgressDictionary[concept].GetCurrentDefenseUpgradeLevel());
        }

        List<SecurityConcepts> lowestUpgradedConcepts = new List<SecurityConcepts>();
        foreach (SecurityConcepts concept in _currentDefenseMinigameOptions)
        {
            if (_securityConceptProgressDictionary[concept].GetCurrentDefenseUpgradeLevel() == currentLowestUpgradeLevel)
            {
                lowestUpgradedConcepts.Add(concept);
            }
        }
        if (lowestUpgradedConcepts.Count == 1) { return UpdateCurrentDefenseMinigameOptions(lowestUpgradedConcepts[0]); }


        // When upgrade levels tied, choose highest heat of the tied ones
        int currentHighestHeatLevel = _securityConceptProgressDictionary[lowestUpgradedConcepts[0]].GetHeat();
        foreach (SecurityConcepts concept in lowestUpgradedConcepts)
        {
            currentHighestHeatLevel = Math.Max(currentHighestHeatLevel, _securityConceptProgressDictionary[concept].GetHeat());
        }
        List<SecurityConcepts> highestHeatConcepts = new List<SecurityConcepts>();
        foreach (SecurityConcepts concept in _currentDefenseMinigameOptions)
        {
            if (_securityConceptProgressDictionary[concept].GetHeat() == currentHighestHeatLevel)
            {
                highestHeatConcepts.Add(concept);
            }
        }
        if (highestHeatConcepts.Count == 1) { return UpdateCurrentDefenseMinigameOptions(highestHeatConcepts[0]); }


        // If all heat values are tied, randomly select from the heat tied ones
        return UpdateCurrentDefenseMinigameOptions(highestHeatConcepts[UnityEngine.Random.Range(0, highestHeatConcepts.Count)]);
    }

    private SecurityConcepts UpdateCurrentDefenseMinigameOptions(SecurityConcepts concecptAboutToBeReturned)
    {
        _currentDefenseMinigameOptions.RemoveAll(concept => concept == concecptAboutToBeReturned);

        if (_currentDefenseMinigameOptions.Count <= 0)
        {
            //After the last sec concept is done for this round, start the next round by re-filling the list
            foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
            {
                _currentDefenseMinigameOptions.Add(concept);
            }
        }
        // Return input param so that this method can be the return value
        return concecptAboutToBeReturned;
    }


    // Primitive Setters

    public void SetRespect(int respect)
    {
        this._respect = respect;
        if (respect <= 0)
        {
            this._respect = 0;
        }
        else if (respect >= MAX_RESPECT)
        {
            this._respect = MAX_RESPECT;
        }
        OnRespectChange?.Invoke(this._respect);
    }
    public void SetOpponentKnowledge(int opponentKnowledge)
    {
        this._opponentKnowledge = opponentKnowledge;
        if (opponentKnowledge <= 0)
        {
            this._opponentKnowledge = 0;
        }
        else if (opponentKnowledge >= MAX_OPP_KNOWLEDGE)
        {
            this._opponentKnowledge = MAX_OPP_KNOWLEDGE;
        }
        OnOpponentKnowledgeChange?.Invoke(this._opponentKnowledge);
    }

    public void SwitchToAfterActionReportScene(string afterActionReportText)
    {
        _afterActionReportText = afterActionReportText;
        SceneManager.LoadScene("AfterActionReport");
    }

    public void SetPlayerEmail(string email)
    {
        this._playerEmail = email;
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
    public void ChangeRespect(int change) { this.SetRespect(this._respect + change); }
    public void ChangeOpponentKnowledge(int change) { this.SetOpponentKnowledge(this._opponentKnowledge + change); }
}

