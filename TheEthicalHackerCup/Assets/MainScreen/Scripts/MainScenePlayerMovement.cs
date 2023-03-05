using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static KeyLocations;

public class MainScenePlayerMovement : MonoBehaviour
{
    private static readonly int VELOCITY_MULTIPLIER = 4;
    private Rigidbody rb;
    private NavMeshAgent agent;
    public enum State
    {
        freeMovement,
        walkingBackToTeamSide,
    }
    private State state;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        state = State.freeMovement;
    }

    private void FixedUpdate()
    {
        switch(state) {
            case(State.freeMovement):
                rb.velocity = new Vector3(
                Input.GetAxis("Horizontal") * VELOCITY_MULTIPLIER,
                rb.velocity.y,
                Input.GetAxis("Vertical") * VELOCITY_MULTIPLIER);
                break;

            case(State.walkingBackToTeamSide):
                if (Vector3.Distance(KeyLocations.playerStartingArea, transform.position) < 0.5f ) {
                    agent.isStopped = true;
                    agent.ResetPath();
                    agent.enabled = false;
                    rb.isKinematic = false;
                    state = State.freeMovement;
                }
                break;
        }
    }

    private void playerDetected() {
        state = State.walkingBackToTeamSide;
        agent.enabled = true;
        agent.SetDestination(KeyLocations.playerStartingArea);
        rb.isKinematic = true;
    }
}
