using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int VELOCITY_MULTIPLIER = 4;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(
            Input.GetAxis("Horizontal") * VELOCITY_MULTIPLIER,
            rb.velocity.y,
            Input.GetAxis("Vertical") * VELOCITY_MULTIPLIER);
    }
}
