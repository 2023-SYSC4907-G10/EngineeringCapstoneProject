using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class movement : MonoBehaviour
{
    
    public float speed;
    // Update is called once per frame
    void Update()
    {
        var body = GetComponent<Rigidbody>();
        var transform = GetComponent<Transform>();
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        var direction = new Vector3(x, 0, z);
        body.velocity = direction * speed;
    }
}
