using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCubePlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float shrinkFactorPerFrame = -0.0005f;
    public float minimumShrinkSize = 0.01f;
    private readonly static float MAX_VELOCITY = 7;
    private float horizontalInput;
    private bool _isEnabled;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        disable();
        FirewallAttackGameManager.OnCurrentGameStateChange += handleGameStateChange;
    }

    void FixedUpdate()
    {
        if (_isEnabled)
        {
            handleInputMovement();
            handleShrinking();
        }
    }

    private void handleInputMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = Vector2.ClampMagnitude(new Vector2(rb.velocity.x + horizontalInput, rb.velocity.y), MAX_VELOCITY);
    }

    private void handleShrinking()
    {
        Vector3 scaleChange = new Vector3(shrinkFactorPerFrame, shrinkFactorPerFrame, shrinkFactorPerFrame);
        transform.localScale += scaleChange;

        if (transform.localScale.sqrMagnitude < minimumShrinkSize)
        {
            Debug.Log("Done Shrinking");
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
            if (state == FirewallAttackStates.End)
            {
                FirewallAttackGameManager.OnCurrentGameStateChange -= handleGameStateChange;
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
    }
}
