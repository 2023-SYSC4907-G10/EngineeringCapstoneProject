using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaivePatrol : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private List<GameObject> waypointList;
    private int currentWaypointIndex=0;
    private GameObject currentWaypoint { get { return waypointList[currentWaypointIndex]; } }
    private Vector2 velocity;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        updateVelocity();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() 
    {
        this.rb.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var other = collision.gameObject;
        if (other.Equals(currentWaypoint))
        {
            updateWayPoint();
            if(waypointList.Count > 1) 
            {
                updateVelocity();
            }
            else 
            {
                velocity = Vector2.zero;
            }
            
        }
    }


    private void updateVelocity()
    {
        var dest = currentWaypoint.transform.position;
        var source = transform.position;

        var direction = (dest - source).normalized;
        velocity = direction * speed;
    }

    private void updateWayPoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypointList.Count;
    }
}
