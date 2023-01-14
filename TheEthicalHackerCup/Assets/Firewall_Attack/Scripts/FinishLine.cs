using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D theCollision)
    {
        if (theCollision.gameObject.layer == LayerMask.NameToLayer("PlayerLayer"))
        {
            Debug.Log("Reached finish line");
            FirewallAttackGameManager.GetInstance().CurrentGameState = FirewallAttackStates.Win;
        }
    }
}
