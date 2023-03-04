public class FWD_DifficultyLevel
{
    public FWD_DifficultyLevel(int level)
    {
        if (level <= 0)
        {
            // EASY - No upgrades
            DifficultyLevel = 0;
            SecondsUntilEnd = 30;
            MaliciousPacketPercentage = 12;
        }
        else if (level == 1)
        {
            // LO MID - 1 upgrade
            DifficultyLevel = 1;
            SecondsUntilEnd = 30;
            MaliciousPacketPercentage = 22;
        }
        else if (level == 2)
        {
            // HI MID - 2 upgrades
            DifficultyLevel = 2;
            SecondsUntilEnd = 45;
            MaliciousPacketPercentage = 22;
        }
        else
        {
            // HARD - 3 upgrades
            DifficultyLevel = 3;
            SecondsUntilEnd = 60;
            MaliciousPacketPercentage = 32;
        }
    }


    public int DifficultyLevel { get; private set; }
    public float SecondsUntilEnd { get; private set; }
    public int MaliciousPacketPercentage { get; private set; }

}