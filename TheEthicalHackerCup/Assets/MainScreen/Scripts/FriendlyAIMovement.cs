using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyAIMovement : MonoBehaviour
{
    
    private NavMeshAgent agent;
    private Vector3 walkingDestination;
    private float startStandingTime;

    public enum State
    {
        PreparingToWalk,
        Walking,
        Standing
    }
    private State state;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startStandingTime = Time.time;
        state = State.Standing;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.PreparingToWalk:
                walkingDestination = KeyLocations.getRandomLocationOnPlayerArea();
                agent.SetDestination(walkingDestination);
                state = State.Walking;
                break;

            case State.Walking:
                if (
                    // If we reach our walking destination, we will begin standing
                    Mathf.Abs(agent.transform.position.x - walkingDestination.x) < 2 &
                    Mathf.Abs(agent.transform.position.z - walkingDestination.z) < 2
                )
                {
                    startStandingTime = Time.time;
                    state = State.Standing;
                }
                break;

            case State.Standing:
                if (Time.time - startStandingTime > 3)
                {
                    state = State.PreparingToWalk;
                }
                break;
        }
    }
}
