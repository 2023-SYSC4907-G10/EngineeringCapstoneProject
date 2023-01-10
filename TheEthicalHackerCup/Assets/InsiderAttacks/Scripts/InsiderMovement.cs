using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsiderMovement : MonoBehaviour
{
    private float horizontalInput;
    private float speed;
    private float jumpForce;
    private Rigidbody2D rb;
    private bool isGrounded;
    private SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        speed = 6.0f;
        jumpForce = 7.0f;
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(0, jumpForce, 0);
        }

        int xOffset = horizontalInput < 0 ? -1 : 0;
        renderer.flipX = horizontalInput < 0;
        this.GetComponent<BoxCollider2D>().offset = new Vector3(xOffset, this.GetComponent<BoxCollider2D>().offset.y);
    }

    void OnCollisionEnter2D(Collision2D theCollision){
        if(theCollision.gameObject.layer == LayerMask.NameToLayer("MinigameTilemap"))
        {
            isGrounded = true;
        }
    }
 
    void OnCollisionExit2D(Collision2D theCollision){
        if(theCollision.gameObject.layer == LayerMask.NameToLayer("MinigameTilemap"))
        {
            isGrounded = false;
        }
    }
}
