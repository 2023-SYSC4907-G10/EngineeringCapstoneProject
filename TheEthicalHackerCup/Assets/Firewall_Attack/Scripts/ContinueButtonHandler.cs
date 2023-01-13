using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            Debug.Log("GO TO AFTER ACTION REPORT SCENE");
        }
    }
}
