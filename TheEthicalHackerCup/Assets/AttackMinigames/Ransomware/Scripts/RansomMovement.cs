using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class RansomMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D body;

    public void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        var side2side = Input.GetAxis("Horizontal");
        var upDown = Input.GetAxis("Vertical");
        var direction = new Vector2(side2side, upDown);
        body.velocity = direction * speed;
    }
}
