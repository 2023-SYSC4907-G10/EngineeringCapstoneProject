using System;
using UnityEngine;
using TMPro;

public class FWD_Timer : MonoBehaviour
{

    // Attached to initially disabled CanvasUI/PanelHUD
    private float _remainingTime;
    public TextMeshProUGUI HUD_TimeText;

    // Start is called before the first frame update
    void Start()
    {
        _remainingTime = FWD_Manager.GetInstance().DifficultyLevel.SecondsUntilEnd;
        // 30f; // 30 Second playtime
    }

    // Update is called once per frame
    void Update()
    {
        if (_remainingTime >= 0)
        {
            _remainingTime -= Time.deltaTime;
            // Update the UI
            HUD_TimeText.text = "Time Remaining: " + Math.Floor(_remainingTime) + "s";
        }
        else
        {
            // End the game

            int goodPacketsBurned = FWD_Manager.GetInstance().GetGoodPacketsBurned();
            int badPacketsReceived = FWD_Manager.GetInstance().GetBadPacketsReceived();
            string endStatement = "Good packets burned: " +goodPacketsBurned + "\n";
            endStatement +=  "Bad packets received: "+badPacketsReceived + "\n";
            if (goodPacketsBurned == 0 && badPacketsReceived == 0)
            {
                endStatement += "Flawless victory! +20 Respect!";
                GameManager.GetInstance().ChangeRespect(20);
            }
            else if (goodPacketsBurned <= 10 && badPacketsReceived <= 10)
            {
                GameManager.GetInstance().ChangeRespect(10);
                endStatement += "Good work! +10 Respect";
            }
            else
            {
                GameManager.GetInstance().ChangeRespect(-10);
                endStatement += "We need a better firewall system. -10 Respect";
            }

            FWD_Manager.GetInstance().EndGame();
            GameManager.GetInstance().SwitchToAfterActionReportScene("Firewall defense minigame finished\n\n" + endStatement);
        }
    }
}
