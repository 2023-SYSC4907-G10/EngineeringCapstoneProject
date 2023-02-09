using UnityEngine;

public class ContinueButtonHandler : MonoBehaviour
{
    public GameObject CanvasUI;

    public void HandleContinueButtonClick()
    {
        //SAMPLE CALL BELOW USED TO TEST TUTORIAL GOING TO LEARNINGSCENE AND THEN RETURNING TO ORIGINAL SCENE
        // GameManager.GetInstance().StartLearningMinigameTutorial("Firewall4"); 
        if (FirewallAttackGameManager.GetInstance().CurrentGameState == FirewallAttackStates.Intro)
        {
            CanvasUI.SetActive(false); // Hiding Canvas UI layers
            FirewallAttackGameManager.GetInstance().CurrentGameState = FirewallAttackStates.Playing;
        }
        else
        {
            FirewallAttackGameManager.GetInstance().InitializeGameState();
            GameManager.GetInstance().SwitchToAfterActionReportScene("Firewall attack completed dude");
        }
    }
}
