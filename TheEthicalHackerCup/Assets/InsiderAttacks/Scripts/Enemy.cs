using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField]
	Transform[] waypoints;

	[SerializeField]
	float moveSpeed = 2f;

	int waypointIndex = 0;

	void Start () {
		transform.position = waypoints[waypointIndex].transform.position;
	}

	void Update () {
		Move();
	}

	void Move()
	{
        bool lastWaypoint = waypointIndex == waypoints.Length;
        // move us back to starting waypoint
		if (lastWaypoint) {
            waypointIndex = 0;
        }

        bool atNextWaypoint = Mathf.Abs(transform.position.x - waypoints[waypointIndex].transform.position.x) < 0.1;
        
        if (!atNextWaypoint) {
            transform.position = Vector3.MoveTowards(transform.position,
												waypoints[waypointIndex].transform.position,
												moveSpeed * Time.deltaTime);
        } else {
            waypointIndex += 1;
        }
	}

}
