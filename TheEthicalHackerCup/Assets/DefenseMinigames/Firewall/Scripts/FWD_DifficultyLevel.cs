public class FWD_DifficultyLevel
{
    public FWD_DifficultyLevel(int level)
    {
        if (level <= 0)
        {
            // EASY
            DifficultyLevel = 0;
            SecondsUntilEnd = 12;
            PacketsPerSecondSpawnRate = 12;
            PacketMovementSpeed = 12;
            MaliciousPacketPercentage = 12;
        }
        else if (level == 1)
        {
            // MEDIUM
            DifficultyLevel = 1;
            SecondsUntilEnd = 22;
            PacketsPerSecondSpawnRate = 22;
            PacketMovementSpeed = 22;
            MaliciousPacketPercentage = 22;
        }
        else
        {
            // HARD
            DifficultyLevel = 2;
            SecondsUntilEnd = 32;
            PacketsPerSecondSpawnRate = 32;
            PacketMovementSpeed = 32;
            MaliciousPacketPercentage = 32;
        }
    }


    public int DifficultyLevel { get; private set; }
    public int SecondsUntilEnd { get; private set; }
    public int PacketsPerSecondSpawnRate { get; private set; }
    public int PacketMovementSpeed { get; private set; }
    public int MaliciousPacketPercentage { get; private set; }

}