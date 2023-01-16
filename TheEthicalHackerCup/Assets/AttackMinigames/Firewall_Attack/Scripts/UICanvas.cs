using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICanvas : MonoBehaviour
{
    public static readonly string WINNER_MESSAGE =
@"Winner! 
Time: ";
    public readonly static string MELTED_FAILURE_MESSAGE =
@"MISSION FAILED!
Your packet was melted!
Time: ";
    public readonly static string PERCENT_MELTED_PREFIX = "\nPercent melted: ";

    public GameObject CanvasUI;
    public TextMeshProUGUI Tmp;

    // Start is called before the first frame update
    void Start()
    {
        FirewallAttackGameManager.OnCurrentGameStateChange += handleGameStateChange; // Subscribe to event
    }

    private void handleGameStateChange(FirewallAttackStates state)
    {
        if (state == FirewallAttackStates.Win || state == FirewallAttackStates.Lose)
        {
            Tmp.text = state == FirewallAttackStates.Win ? WINNER_MESSAGE : MELTED_FAILURE_MESSAGE;
            Tmp.text += FirewallAttackGameManager.GetInstance().GetPlayTime() + "s";

            if (state == FirewallAttackStates.Win)
            {
                Tmp.text += PERCENT_MELTED_PREFIX + FirewallAttackGameManager.GetInstance().PercentMelted + "%";
            }
            CanvasUI.SetActive(true);
            FirewallAttackGameManager.OnCurrentGameStateChange -= handleGameStateChange; // Unsubscribe to event
        }
    }
}
