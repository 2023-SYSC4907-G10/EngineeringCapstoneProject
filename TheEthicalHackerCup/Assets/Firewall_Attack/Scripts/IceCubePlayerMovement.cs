using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCubePlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private readonly static float SHRINK_FACTOR_PER_FRAME = -0.0005f;
    private readonly static float MAX_VELOCITY = 8;
    private float _horizontalInput;
    private bool _isEnabled;
    private int _shrinkSlowdownFactor;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        disable();
        _shrinkSlowdownFactor = 4;
        FirewallAttackGameManager.OnCurrentGameStateChange += handleGameStateChange;
        FirewallAttackGameManager.OnFlameTrapCollision += shrinkPlayer;
    }

    void FixedUpdate()
    {
        if (_isEnabled)
        {
            handleInputMovement();
            if (Time.frameCount % _shrinkSlowdownFactor == 0) shrinkPlayer(SHRINK_FACTOR_PER_FRAME); // Shrink every x frames
        }
    }

    private void handleInputMovement()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = Vector2.ClampMagnitude(new Vector2(rb.velocity.x + _horizontalInput, rb.velocity.y), MAX_VELOCITY);
    }

    private void shrinkPlayer(float shrinkFactor)
    {
        Vector3 scaleChange = new Vector3(shrinkFactor, shrinkFactor, shrinkFactor);
        transform.localScale += scaleChange;
        if (transform.localScale.sqrMagnitude < 0.27) // Super strange bug. No idea why this doesn't work when not hard coded
        {
            FirewallAttackGameManager.GetInstance().PercentMelted = 1 - transform.localScale.sqrMagnitude / 0.75f; // FIGURE OUT THE ORDER OF THIS
            FirewallAttackGameManager.GetInstance().CurrentGameState = FirewallAttackStates.Lose;
        }
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
                FirewallAttackGameManager.OnFlameTrapCollision -= shrinkPlayer;
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
