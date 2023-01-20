using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController_FirewallAttack : MonoBehaviour
{
    // Constants
    private readonly static float MAX_VELOCITY = 8;

    // Inspector based fields
    public PacketSpriteSequence packetSpriteSequence;
    public TextMeshPro playerTextMeshPro;

    // Fields
    private Rigidbody2D rb;
    private float _horizontalInput;
    private bool _isEnabled;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        disable();

        updateHealthUI();
        FirewallAttackGameManager.OnCurrentGameStateChange += handleGameStateChange;
        FirewallAttackGameManager.OnFlameTrapCollision += handleObstacleCollision;
    }

    private void updateHealthUI()
    {
        playerTextMeshPro.SetText("Health: {0}/{1}", FirewallAttackGameManager.GetInstance().CurrentHealth, FirewallAttackGameManager.GetInstance().StartingHealth);
    }

    void FixedUpdate()
    {
        if (_isEnabled)
        {
            handleInputMovement();
        }
    }

    private void handleInputMovement()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = Vector2.ClampMagnitude(new Vector2(rb.velocity.x + _horizontalInput, rb.velocity.y), MAX_VELOCITY);
    }

    private void handleObstacleCollision()
    {
        FirewallAttackGameManager.GetInstance().CurrentHealth--;
        updateHealthUI();
    }

    private void handleGameStateChange(FirewallAttackStates state)
    {
        if (state == FirewallAttackStates.Playing)
        {
            enable();
        }
        else
        {
            disable();
            if (state == FirewallAttackStates.Win || state == FirewallAttackStates.Lose)
            {

                FirewallAttackGameManager.OnCurrentGameStateChange -= handleGameStateChange;
                FirewallAttackGameManager.OnFlameTrapCollision -= handleObstacleCollision;
            }
        }
    }

    private void enable()
    {
        _isEnabled = true;
        rb.gravityScale = 1;
    }

    private void disable()
    {
        _isEnabled = false;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0, 0);
    }
}
