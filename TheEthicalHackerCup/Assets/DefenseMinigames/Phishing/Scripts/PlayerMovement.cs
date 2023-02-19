using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;

    private enum AnimationState
    {
        idle, running, jumping, falling
    }

    private float jumpForce = 10f;
    private float moveSpeed = 7f;
    private float dirX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        AnimationState state;

        // Check if Moving
        if (dirX > 0f)
        {
            sprite.flipX = false;
            state = AnimationState.running;
        }
        else if (dirX < 0f)
        {
            sprite.flipX = true;
            state = AnimationState.running;
        }
        else
        {
            state = AnimationState.idle;
        }

        // Check if Jumping
        if (rb.velocity.y > 0.1f)
        {
            state = AnimationState.jumping;
        }

        if (rb.velocity.y < -0.1f)
        {
            state = AnimationState.falling;
        }

        animator.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            animator.SetTrigger("death");
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
