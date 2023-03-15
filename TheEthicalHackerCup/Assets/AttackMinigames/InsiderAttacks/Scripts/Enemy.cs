using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField]
	Transform[] waypoints;

	[SerializeField]
	float moveSpeed = 2f;

	int waypointIndex = 0;

	private SpriteRenderer _renderer;
	private bool enemyFlipped;
	void Start () {
		transform.position = waypoints[waypointIndex].transform.position;
		_renderer = GetComponent<SpriteRenderer>();
		enemyFlipped = false;
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
			_renderer.flipX = true;
			enemyFlipped = true;
        }

        bool atNextWaypoint = Mathf.Abs(transform.position.x - waypoints[waypointIndex].transform.position.x) < 0.1;
        
        if (!atNextWaypoint) {
            transform.position = Vector3.MoveTowards(transform.position,
												waypoints[waypointIndex].transform.position,
												moveSpeed * Time.deltaTime);
        } else {
			if (waypointIndex == 0) {
				_renderer.flipX = false;
				enemyFlipped = false;
			}
            waypointIndex += 1;
        }
	}

	public bool isEnemyFlipped() {
		return enemyFlipped;
	}

}
