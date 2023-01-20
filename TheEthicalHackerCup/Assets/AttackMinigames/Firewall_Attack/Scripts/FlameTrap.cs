using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrap : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D theCollision)
    {
        if (theCollision.gameObject.layer == LayerMask.NameToLayer("PlayerLayer"))
        {
            FirewallAttackGameManager.GetInstance().FlameTrapCollision();
        }
    }
}
