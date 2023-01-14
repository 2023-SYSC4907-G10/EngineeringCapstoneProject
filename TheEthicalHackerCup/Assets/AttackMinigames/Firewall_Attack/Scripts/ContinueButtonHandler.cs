using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            SceneManager.LoadScene("MainScene"); // TODO: Replace this with the after action report once ready
        }
    }
}
