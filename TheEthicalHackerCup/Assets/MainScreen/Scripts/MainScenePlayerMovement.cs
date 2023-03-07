using UnityEngine;
using UnityEngine.AI;
using static KeyLocations;

public class MainScenePlayerMovement : MonoBehaviour
{
    // Constants
    private static readonly int VELOCITY_MULTIPLIER = 4;

    // Public fields
    public Animator _animator;
    public SpriteRenderer _spriteRenderer;
    public GameObject SorrySpeechBubble;

    // States
    private enum AnimationState { Idle, Walking, }
    public enum MovementState { freeMovement, walkingBackToTeamSide, }
    private MovementState movementState;
    private AnimationState animationState;

    // Private fields
    private Rigidbody rb;
    private NavMeshAgent agent;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        movementState = MovementState.freeMovement;
        SorrySpeechBubble.SetActive(false);
    }

    private void FixedUpdate()
    {
        switch (movementState)
        {
            case (MovementState.freeMovement):
                rb.velocity = new Vector3(
                    Input.GetAxis("Horizontal") * VELOCITY_MULTIPLIER,
                    rb.velocity.y,
                    Input.GetAxis("Vertical") * VELOCITY_MULTIPLIER);
                updateAnimationState();
                SorrySpeechBubble.SetActive(false);
                break;

            case (MovementState.walkingBackToTeamSide):
                if (Vector3.Distance(KeyLocations.playerStartingArea, transform.position) < 0.5f)
                {
                    agent.isStopped = true;
                    agent.ResetPath();
                    agent.enabled = false;
                    rb.isKinematic = false;
                    movementState = MovementState.freeMovement;
                }
                // Return home animation
                _animator.SetInteger("state", 1);
                _spriteRenderer.flipX = true;
                SorrySpeechBubble.SetActive(true);
                break;
        }
    }

    private void playerDetected()
    {
        movementState = MovementState.walkingBackToTeamSide;
        agent.enabled = true;
        agent.SetDestination(KeyLocations.playerStartingArea);
        rb.isKinematic = true;
    }

    private void updateAnimationState()
    {
        if (rb.velocity.x != 0f || rb.velocity.z != 0f)
        {
            animationState = AnimationState.Walking;
            // Mirror sprite if moving left
            if (rb.velocity.x != 0f) { _spriteRenderer.flipX = rb.velocity.x < 0f; }
        }
        else if (rb.velocity.x == 0f && rb.velocity.z == 0f)
        {
            // Idle
            animationState = AnimationState.Idle;
        }
        _animator.SetInteger("state", (int)animationState);
    }
}
