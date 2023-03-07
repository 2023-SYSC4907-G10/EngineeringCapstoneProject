using UnityEngine;
using UnityEngine.AI;

public class FriendlyAnimation : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    public NavMeshAgent Agent; // From parent

    private enum AnimationState
    {
        Idle,
        Walking,
    }


    private AnimationState state;
    // Start is called before the first frame update
    void Start()
    {
        state = AnimationState.Idle;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Agent.desiredVelocity.x > 0f)
        {
            // Moving right
            state = AnimationState.Walking;
            _spriteRenderer.flipX = false;
        }
        else if (Agent.desiredVelocity.x < 0f)
        {
            // Moving left. Mirror sprite            
            state = AnimationState.Walking;
            _spriteRenderer.flipX = true;
        }
        else if (Agent.desiredVelocity.x == 0f && Agent.desiredVelocity.z == 0f)
        {
            // Idle
            state = AnimationState.Idle;
        }
        _animator.SetInteger("state", (int)state);

    }
}
