using UnityEngine;

public class ContinueButtonHandler : MonoBehaviour
{
    public GameObject CanvasUI;

    public void HandleContinueButtonClick()
    {
        if (FirewallAttackGameManager.GetInstance().CurrentGameState == FirewallAttackStates.Intro)
        {
            CanvasUI.SetActive(false); // Hiding Canvas UI layers
            FirewallAttackGameManager.GetInstance().CurrentGameState = FirewallAttackStates.Playing;
        }
        else
        {
            GameManager.GetInstance().ChangeRespect(
                FirewallAttackGameManager.GetInstance().CurrentGameState == FirewallAttackStates.Win ? 10:-10
            );

            FirewallAttackGameManager.GetInstance().InitializeGameState();
            GameManager.GetInstance().SwitchToAfterActionReportScene("Firewall attack completed dude");
        }
    }
}
