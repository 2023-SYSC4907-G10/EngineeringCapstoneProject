using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICanvas : MonoBehaviour
{
    public static readonly string WINNER_MESSAGE =
@"Winner!
Your packet made it past the firewall!
Time: ";
    public readonly static string MELTED_FAILURE_MESSAGE =
@"MISSION FAILED!
Your packet was blocked by the firewall!
Time: ";
    public readonly static string PERCENT_MELTED_PREFIX = "\n Remaining health: ";

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
                float healthPercent = 100 * FirewallAttackGameManager.GetInstance().CurrentHealth / FirewallAttackGameManager.GetInstance().StartingHealth;
                Tmp.text += PERCENT_MELTED_PREFIX + healthPercent + "%";
            }
            CanvasUI.SetActive(true);
            FirewallAttackGameManager.OnCurrentGameStateChange -= handleGameStateChange; // Unsubscribe to event
        }
    }
}
