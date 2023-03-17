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
    private int _assertCalls;

    // Assumes all secConcepts have 4 upgrades, except those in GameManager.With3Upgrades that have 3 upgrades
    private static readonly int TotalPossibleUpgradesCount = Enum.GetNames(typeof(SecurityConcepts)).Length * 3;

    private void BeforeEach()
    {
        //  Resetting the singleton instance to default values
        GameManager.GetInstance().InitializeGameState();
        // Never do this under normal circumstances. 
        // Simply referencing the GameManager.GetInstance() as shown below should suffice

        this._assertCalls = 0;
    }



    /// Callback functions attached as observers to integer update events
    private void AssertIntegerEquals(int incommingInteger)
    {
        Assert.AreEqual(this._currentExpectedInteger, incommingInteger);
        this._assertCalls++;
    }
    private void AssertSecurityConceptAndIntegerEquals(SecurityConcepts incommingSecurityConcept, int incommingInteger)
    {
        Assert.AreEqual(this._currentExpectedInteger, incommingInteger);
        Assert.AreEqual(this._currentExpectedSecurityConcept, incommingSecurityConcept);
        this._assertCalls++;
    }

    [Test]
    public void NextDefenseMinigameTest()
    {
        BeforeEach();
        GameManager.GetInstance().AttemptAttackMinigame(SecurityConcepts.DDoS);
        GameManager.GetInstance().AttemptAttackMinigame(SecurityConcepts.InsiderAttack);
        GameManager.GetInstance().AttemptAttackMinigame(SecurityConcepts.Firewall);
        GameManager.GetInstance().AttemptAttackMinigame(SecurityConcepts.Phishing);

        GameManager.GetInstance().UpgradeDefenseUpgradeLevel(SecurityConcepts.DDoS);
        GameManager.GetInstance().UpgradeDefenseUpgradeLevel(SecurityConcepts.InsiderAttack);
        GameManager.GetInstance().UpgradeDefenseUpgradeLevel(SecurityConcepts.Firewall);
        GameManager.GetInstance().UpgradeDefenseUpgradeLevel(SecurityConcepts.Phishing);

        GameManager.GetInstance().ChangeAttackSpecificHeat(SecurityConcepts.DDoS, 80);
        GameManager.GetInstance().ChangeAttackSpecificHeat(SecurityConcepts.InsiderAttack, 20);

        // Least upgraded
        Assert.AreEqual(SecurityConcepts.Ransomware, GameManager.GetInstance().GetNextDefenseMinigame());

        // Then prioritize highest heat
        Assert.AreEqual(SecurityConcepts.DDoS, GameManager.GetInstance().GetNextDefenseMinigame());
        Assert.AreEqual(SecurityConcepts.InsiderAttack, GameManager.GetInstance().GetNextDefenseMinigame());

        // Random selection now to complete the cylce
        Assert.Contains(
            GameManager.GetInstance().GetNextDefenseMinigame(),
            new[] { SecurityConcepts.Firewall, SecurityConcepts.Phishing },
            "Error in determining next defense minigame"
        );
        Assert.Contains(
            GameManager.GetInstance().GetNextDefenseMinigame(),
            new[] { SecurityConcepts.Firewall, SecurityConcepts.Phishing },
            "Error in determining next defense minigame"
        );

        //After the cycle resets, should still return the least upgraded
        Assert.AreEqual(SecurityConcepts.Ransomware, GameManager.GetInstance().GetNextDefenseMinigame());
        // Then prioritize highest heat again
        Assert.AreEqual(SecurityConcepts.DDoS, GameManager.GetInstance().GetNextDefenseMinigame());
        Assert.AreEqual(SecurityConcepts.InsiderAttack, GameManager.GetInstance().GetNextDefenseMinigame());
        // Random selection now to complete the cylce again
        Assert.Contains(
            GameManager.GetInstance().GetNextDefenseMinigame(),
            new[] { SecurityConcepts.Firewall, SecurityConcepts.Phishing },
            "Error in determining next defense minigame"
        );
        Assert.Contains(
            GameManager.GetInstance().GetNextDefenseMinigame(),
            new[] { SecurityConcepts.Firewall, SecurityConcepts.Phishing },
            "Error in determining next defense minigame"
        );
    }

    [Test]
    public void NextLearningMinigameFilenameTest()
    {
        BeforeEach();
        Assert.AreEqual("Firewall1", GameManager.GetInstance().GetNextLearningMinigameFilename());
        GameManager.GetInstance().AttemptAttackMinigame(SecurityConcepts.Firewall);
        GameManager.GetInstance().AttemptAttackMinigame(SecurityConcepts.Firewall);
        GameManager.GetInstance().AttemptAttackMinigame(SecurityConcepts.Firewall);
        GameManager.GetInstance().AttemptAttackMinigame(SecurityConcepts.Firewall);
        GameManager.GetInstance().AttemptAttackMinigame(SecurityConcepts.Firewall);
        Assert.AreEqual(5, GameManager.GetInstance().GetAttackMinigamesAttempted(SecurityConcepts.Firewall));
        GameManager.GetInstance().UpgradeDefenseUpgradeLevel(SecurityConcepts.Firewall);
        Assert.AreEqual("Firewall2", GameManager.GetInstance().GetNextLearningMinigameFilename());
    }



    [Test]
    public void RespectTest()
    {
        BeforeEach();

        GameManager.OnRespectChange += AssertIntegerEquals;
        Assert.AreEqual(25, GameManager.GetInstance().GetRespect());
        _currentExpectedInteger = 0;
        GameManager.GetInstance().ChangeRespect(-25);
        // Increase by 20 from 0
        _currentExpectedInteger = 20;
        GameManager.GetInstance().ChangeRespect(20);

        // Decrease by 10 from 20
        _currentExpectedInteger = 10;
        GameManager.GetInstance().ChangeRespect(-10);

        // Attempt to decrease into the negatives. Stay at 0
        _currentExpectedInteger = 0;
        GameManager.GetInstance().ChangeRespect(-100);
        Assert.AreEqual(0, GameManager.GetInstance().GetRespect());

        // try to increase past limit. Stay at limit
        _currentExpectedInteger = GameManager.MAX_RESPECT;
        GameManager.GetInstance().ChangeRespect(GameManager.MAX_RESPECT + 100);
        Assert.AreEqual(GameManager.MAX_RESPECT, GameManager.GetInstance().GetRespect());

        // Set Directly
        _currentExpectedInteger = 88;
        GameManager.GetInstance().SetRespect(88);
        Assert.AreEqual(88, GameManager.GetInstance().GetRespect());

        GameManager.OnRespectChange -= AssertIntegerEquals;
        Assert.AreEqual(6, this._assertCalls);
    }

    [Test]
    public void OpponentKnowledgeTest()
    {
        BeforeEach();

        GameManager.OnOpponentKnowledgeChange += AssertIntegerEquals;
        Assert.AreEqual(0, GameManager.GetInstance().GetOpponentKnowledge());

        // Increase by 20 from 0
        _currentExpectedInteger = 20;
        GameManager.GetInstance().ChangeOpponentKnowledge(20);

        // Decrease by 10 from 20
        _currentExpectedInteger = 10;
        GameManager.GetInstance().ChangeOpponentKnowledge(-10);

        // Attempting to decrease into the negatives. Stay at zero
        _currentExpectedInteger = 0;
        GameManager.GetInstance().ChangeOpponentKnowledge(-100);
        Assert.AreEqual(0, GameManager.GetInstance().GetOpponentKnowledge());

        // Attempt to increase past limit. Stay at max
        _currentExpectedInteger = GameManager.MAX_OPP_KNOWLEDGE;
        GameManager.GetInstance().ChangeOpponentKnowledge(GameManager.MAX_OPP_KNOWLEDGE + 100);
        Assert.AreEqual(GameManager.MAX_OPP_KNOWLEDGE, GameManager.GetInstance().GetOpponentKnowledge());

        // Set Directly
        _currentExpectedInteger = 88;
        GameManager.GetInstance().SetOpponentKnowledge(88);
        Assert.AreEqual(88, GameManager.GetInstance().GetOpponentKnowledge());

        GameManager.OnOpponentKnowledgeChange -= AssertIntegerEquals;
        Assert.AreEqual(5, this._assertCalls);
    }

    [Test]
    public void DefenseUpgradeLevelTest_DepthFirst()
    {
        BeforeEach();
        GameManager.OnDefenseUpgradeLevelsChange += AssertSecurityConceptAndIntegerEquals;

        // Iterate thru security concepts. Attempt to upgrade 5 times each, but they will stop at the max
        foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
        {
            _currentExpectedSecurityConcept = concept;
            for (int i = 1; i <= 5; i++)
            {
                _currentExpectedInteger = i;
                GameManager.GetInstance().AttemptAttackMinigame(concept); // Attack attempt required to upgrade
                GameManager.GetInstance().UpgradeDefenseUpgradeLevel(concept);
            }
        }

        Assert.AreEqual(TotalPossibleUpgradesCount, this._assertCalls);

        // Verify everyone is max upgraded
        foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
        {
            Assert.AreEqual(true, GameManager.GetInstance().GetSecurityConceptProgressDictionary()[concept].IsFullyUpgraded());
        }
        Assert.True(GameManager.GetInstance().IsEverythingFullyUpgraded());

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
                GameManager.GetInstance().AttemptAttackMinigame(concept); // Attack attempt required to upgrade
                GameManager.GetInstance().UpgradeDefenseUpgradeLevel(concept);
            }
        }

        Assert.AreEqual(TotalPossibleUpgradesCount, this._assertCalls);

        // Verify everyone is max upgraded
        foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
        {
            Assert.AreEqual(true, GameManager.GetInstance().GetSecurityConceptProgressDictionary()[concept].IsFullyUpgraded());
        }
        Assert.True(GameManager.GetInstance().IsEverythingFullyUpgraded());

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
                GameManager.GetInstance().AttemptAttackMinigame(concept);
            }
        }

        Assert.AreEqual(25, this._assertCalls);

        // Verify everyone is at 5 attempts
        foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
        {
            Assert.AreEqual(5, GameManager.GetInstance().GetAttackMinigamesAttempted(concept));
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
                GameManager.GetInstance().AttemptAttackMinigame(concept);
            }
        }

        Assert.AreEqual(25, this._assertCalls);

        // Verify everyone is at 5 attempts
        foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
        {
            Assert.AreEqual(5, GameManager.GetInstance().GetAttackMinigamesAttempted(concept));
        }

        GameManager.OnAttackMinigameAttemptChange -= AssertSecurityConceptAndIntegerEquals;

    }

    [Test]
    public void AttackMinigamesRequiredTest()
    {
        BeforeEach();
        foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
        {
            // Everyone needs one attempt to upgrade initially
            Assert.AreEqual(1, GameManager.GetInstance().GetAttackMinigamesAttemptsRequiredToUpgrade(concept));

            // Attack minigame attempt everyone once
            GameManager.GetInstance().AttemptAttackMinigame(concept);

            // Before upgrading, everyone still needs one attempt to upgrade, even if they have an attempt
            Assert.AreEqual(1, GameManager.GetInstance().GetAttackMinigamesAttemptsRequiredToUpgrade(concept));

            // Then upgrade once successfully
            Assert.AreEqual(true, GameManager.GetInstance().UpgradeDefenseUpgradeLevel(concept));

            // Everyone now needs 2 attempts to get the next upgrade
            Assert.AreEqual(2, GameManager.GetInstance().GetAttackMinigamesAttemptsRequiredToUpgrade(concept));

            // Can't upgrade without another attack game attempt
            Assert.AreEqual(false, GameManager.GetInstance().UpgradeDefenseUpgradeLevel(concept));
            Assert.AreEqual(1, GameManager.GetInstance().GetDefenseUpgradeLevel(concept));

            //Fully upgrade
            do { GameManager.GetInstance().AttemptAttackMinigame(concept); }
            while (GameManager.GetInstance().UpgradeDefenseUpgradeLevel(concept));

            // Ensure max defense upgrade level matches requirements to upgrade once fully upgraded
            Assert.AreEqual(GameManager.GetInstance().GetMaxDefenseUpgradeLevel(concept), GameManager.GetInstance().GetAttackMinigamesAttemptsRequiredToUpgrade(concept));
        }
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
                GameManager.GetInstance().ChangeAttackSpecificHeat(concept, change);
            }
        }
        void AssertAllHeatEquals(int expectedHeat)
        {
            //Verify everyone is at expected heat
            foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
            {
                Assert.AreEqual(expectedHeat, GameManager.GetInstance().GetAttackSpecificHeat(concept));
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
        tryToChangeAllHeats(GameManager.MAX_HEAT);
        AssertAllHeatEquals(99);

        GameManager.OnAttackSpecificHeatChange -= AssertSecurityConceptAndIntegerEquals;

        Assert.AreEqual(5, this._assertCalls);
    }
}