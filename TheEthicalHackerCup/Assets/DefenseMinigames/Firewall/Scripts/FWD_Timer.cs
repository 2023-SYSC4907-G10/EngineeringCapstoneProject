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
            FWD_Manager.GetInstance().EndGame();
            GameManager.GetInstance().ChangeRespect(10); // TODO Update this to be performance based
            GameManager.GetInstance().SwitchToAfterActionReportScene("Firewall defense minigame finished");
        }
    }
}
