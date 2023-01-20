using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketLayerRemoverToken : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D theCollision)
    {
        if (theCollision.gameObject.layer == LayerMask.NameToLayer("PlayerLayer"))
        {
            Destroy(gameObject); // Self destruction of trigger
            var playerController = (PlayerController_FirewallAttack)theCollision.gameObject.GetComponent<MonoBehaviour>();
            playerController.packetSpriteSequence.SwitchToNextPacketLayer();
        }
    }
}
