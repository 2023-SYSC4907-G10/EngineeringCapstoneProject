using System;
// using System;

public class SecurityConceptProgress
{
    public const int MaxHeat = 100;

    // Private fields
    private int _maxDefenseUpgradeLevel;
    private int _currentDefenseUpgradeLevel;
    private int _attackMinigamesAttempted;
    private int _heat;

    // Constructor
    public SecurityConceptProgress(int maxDefenseUpgradeLevel = 5)
    {
        this._maxDefenseUpgradeLevel = maxDefenseUpgradeLevel;
        this._currentDefenseUpgradeLevel = 0;
        this._attackMinigamesAttempted = 0;
        this._heat = 0;
    }

    // Getters
    public int GetMaxDefenseUpgradeLevel() { return this._maxDefenseUpgradeLevel; }
    public int GetCurrentDefenseUpgradeLevel() { return this._currentDefenseUpgradeLevel; }
    public int GetAttackMinigamesAttempted() { return this._attackMinigamesAttempted; }
    public int GetHeat() { return this._heat; }

    public int GetAttackMinigameAttemptsRequiredToUpgrade() { return Math.Min(this._maxDefenseUpgradeLevel, this._currentDefenseUpgradeLevel + 1); }

    // Boolean Indicators
    public bool IsFullyUpgraded() { return this._currentDefenseUpgradeLevel >= this._maxDefenseUpgradeLevel; }

    // Boosters
    public bool UpgradeDefense()
    {
        if (this._currentDefenseUpgradeLevel >= this._maxDefenseUpgradeLevel || this._attackMinigamesAttempted < this.GetAttackMinigameAttemptsRequiredToUpgrade())
        {
            return false;
        }
        else
        {
            this._currentDefenseUpgradeLevel++;
            return true;
        }
    }

    public void AttemptAttackMinigame()
    {
        // Play can attempt this more than upgrades are possible
        this._attackMinigamesAttempted++;
    }

    // Changers
    public bool ChangeHeat(int changeAmount)
    {
        if (this._heat + changeAmount >= 0 && this._heat + changeAmount < MaxHeat)
        {
            this._heat = this._heat + changeAmount;
            return true;
        }
        return false;
    }
}