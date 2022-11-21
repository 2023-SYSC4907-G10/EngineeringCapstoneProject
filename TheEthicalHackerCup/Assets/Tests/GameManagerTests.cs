using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


[TestFixture]
public class GameManagerTests
{

    private int _currentExpectedInteger;
    private SecurityConcepts _currentExpectedSecurityConcept;


    private void BeforeEach()
    {
        //  Resetting the singleton instance to default values
        GameManager.Instance = (GameManager)ScriptableObject.CreateInstance("GameManager");
        // Never do this under normal circumstances. Simply referencing the GameManager.Instance as shown below should suffice
    }



    /// Callback functions attached as observers to integer update events
    private void AssertIntegerEquals(int incommingInteger)
    {
        Assert.True(this._currentExpectedInteger == incommingInteger);
    }
    private void AssertSecurityConceptAndIntegerEquals(SecurityConcepts incommingSecurityConcept, int incommingInteger)
    {
        Assert.True(this._currentExpectedInteger == incommingInteger);
        Assert.True(this._currentExpectedSecurityConcept == incommingSecurityConcept);
    }



    [Test]
    public void NextLearningMinigameFilenameTest()
    {
        BeforeEach();
        Assert.True("" == GameManager.Instance.GetNextLearningMinigameFilename());
        var filename = "Nice.txt";
        GameManager.Instance.SetNextLearningMinigameFilename(filename);
        Assert.True(filename == GameManager.Instance.GetNextLearningMinigameFilename());
    }


    [Test]
    public void ReputationTest()
    {
        BeforeEach();

        GameManager.OnReputationChange += AssertIntegerEquals;
        Assert.True(0 == GameManager.Instance.GetReputation());
        // Increase by 20 from 0
        _currentExpectedInteger = 20;
        GameManager.Instance.ChangeReputation(20);

        // Decrease by 10 from 20
        _currentExpectedInteger = 10;
        GameManager.Instance.ChangeReputation(-10);

        // Fail to decrease into the negatives. Stay at 10
        GameManager.Instance.ChangeReputation(-100);
        Assert.True(10 == GameManager.Instance.GetReputation());

        // Fail to increase past limit. Stay at 10
        GameManager.Instance.ChangeReputation(GameManager.MaxReputation + 100);
        Assert.True(10 == GameManager.Instance.GetReputation());

        // Set Directly
        _currentExpectedInteger = 88;
        GameManager.Instance.SetReputation(88);
        Assert.True(88 == GameManager.Instance.GetReputation());

        GameManager.OnReputationChange -= AssertIntegerEquals;
    }

    [Test]
    public void OpponentKnowledgeTest()
    {
        BeforeEach();

        GameManager.OnOpponentKnowledgeChange += AssertIntegerEquals;
        Assert.True(0 == GameManager.Instance.GetOpponentKnowledge());

        // Increase by 20 from 0
        _currentExpectedInteger = 20;
        GameManager.Instance.ChangeOpponentKnowledge(20);

        // Decrease by 10 from 20
        _currentExpectedInteger = 10;
        GameManager.Instance.ChangeOpponentKnowledge(-10);

        // Fail to decrease into the negatives. Stay at 10
        GameManager.Instance.ChangeOpponentKnowledge(-100);
        Assert.True(10 == GameManager.Instance.GetOpponentKnowledge());

        // Fail to increase past limit. Stay at 10
        GameManager.Instance.ChangeOpponentKnowledge(GameManager.MaxOpponentKnowledge + 100);
        Assert.True(10 == GameManager.Instance.GetOpponentKnowledge());

        // Set Directly
        _currentExpectedInteger = 88;
        GameManager.Instance.SetOpponentKnowledge(88);
        Assert.True(88 == GameManager.Instance.GetOpponentKnowledge());

        GameManager.OnOpponentKnowledgeChange -= AssertIntegerEquals;
    }

    [Test]
    public void DefenseUpgradeLevelTest_DepthFirst()
    {
        BeforeEach();
        GameManager.OnDefenseUpgradeLevelsChange += AssertSecurityConceptAndIntegerEquals;

        // Iterate thru security concepts. Boosting them 0 to 5 in series
        foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
        {
            _currentExpectedSecurityConcept = concept;
            for (int i = 1; i <= 5; i++)
            {
                _currentExpectedInteger = i;
                GameManager.Instance.UpgradeDefenseUpgradeLevel(concept);
            }
        }

        // Verify everyone is at 5
        var conceptDictionary = GameManager.Instance.GetDefenseUpgradeLevels();
        foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
        {
            Assert.True(5 == conceptDictionary[concept]);
        }

        GameManager.OnDefenseUpgradeLevelsChange -= AssertSecurityConceptAndIntegerEquals;
    }

    [Test]
    public void DefenseUpgradeLevelTest_BreadthFirst()
    {
        BeforeEach();
        GameManager.OnDefenseUpgradeLevelsChange += AssertSecurityConceptAndIntegerEquals;

        // Iterate thru security concepts, boosting each by one until they all reach 5
        for (int i = 1; i <= 5; i++)
        {
            _currentExpectedInteger = i;
            foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
            {
                _currentExpectedSecurityConcept = concept;
                GameManager.Instance.UpgradeDefenseUpgradeLevel(concept);
            }
        }

        // Verify everyone is at 5
        var conceptDictionary = GameManager.Instance.GetDefenseUpgradeLevels();
        foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
        {
            Assert.True(5 == conceptDictionary[concept]);
        }

        GameManager.OnDefenseUpgradeLevelsChange -= AssertSecurityConceptAndIntegerEquals;
    }

    [Test]
    public void AttackMinigameAttemptTest_DepthFirst()
    {
        BeforeEach();
        GameManager.OnAttackMinigameAttemptChange += AssertSecurityConceptAndIntegerEquals;
        // Iterate thru security concepts. Boosting them 0 to 5 in series
        foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
        {
            _currentExpectedSecurityConcept = concept;
            for (int i = 1; i <= 5; i++)
            {
                _currentExpectedInteger = i;
                GameManager.Instance.AttemptAttackMinigame(concept);
            }
        }

        // Verify everyone is at 5
        var conceptDictionary = GameManager.Instance.GetAttackMinigamesAttempted();
        foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
        {
            Assert.True(5 == conceptDictionary[concept]);
        }


        GameManager.OnAttackMinigameAttemptChange -= AssertSecurityConceptAndIntegerEquals;

    }
    [Test]
    public void AttackMinigameAttemptTest_BreadthFirst()
    {
        BeforeEach();
        GameManager.OnAttackMinigameAttemptChange += AssertSecurityConceptAndIntegerEquals;
        // Iterate thru security concepts, boosting each by one until they all reach 5
        for (int i = 1; i <= 5; i++)
        {
            _currentExpectedInteger = i;
            foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
            {
                _currentExpectedSecurityConcept = concept;
                GameManager.Instance.AttemptAttackMinigame(concept);
            }
        }

        // Verify everyone is at 5
        var conceptDictionary = GameManager.Instance.GetAttackMinigamesAttempted();
        foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
        {
            Assert.True(5 == conceptDictionary[concept]);
        }


        GameManager.OnAttackMinigameAttemptChange -= AssertSecurityConceptAndIntegerEquals;

    }


    [Test]
    public void AttackSpecificHeatTest()
    {
        void tryToChangeAllHeats(int change)
        {
            foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
            {
                _currentExpectedSecurityConcept = concept;
                _currentExpectedInteger = change;
                GameManager.Instance.ChangeAttackSpecificHeat(concept, change);
            }
        }
        void AssertAllHeatEquals(int expectedHeat)
        {
            //Verify everyone is at 99
            var conceptDictionary = GameManager.Instance.GetAttackSpecificHeat();
            foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
            {
                Assert.True(expectedHeat == conceptDictionary[concept]);
            }
        }

        BeforeEach();
        GameManager.OnAttackSpecificHeatChange += AssertSecurityConceptAndIntegerEquals;

        // Change all heats from 0 to 99
        AssertAllHeatEquals(0);
        tryToChangeAllHeats(99);
        AssertAllHeatEquals(99);

        // Attempt and fail to change all heats to negative. Remain at 99
        tryToChangeAllHeats(-999);
        AssertAllHeatEquals(99);

        // Attempt and fail to change all heats beyone upper limit. Remain at 99
        tryToChangeAllHeats(GameManager.MaxHeat);
        AssertAllHeatEquals(99);

        GameManager.OnAttackSpecificHeatChange -= AssertSecurityConceptAndIntegerEquals;
    }
}