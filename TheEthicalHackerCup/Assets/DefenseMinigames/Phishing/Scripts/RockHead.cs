using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHead : MonoBehaviour
{

    [SerializeField] private GameObject fallPoint;
    [SerializeField] private float speedDown = 2f;
    [SerializeField] private float speedUp = 2f;

    private float timeSinceLastAttack;
    private float timeBetweenAttack;
    private Vector2 initialPosition;
    private Animator animator;

    private AnimationState state;

    private BoxCollider2D coll;

    private enum AnimationState
    {
        idle, blink, idleFalling, hit, idleRising
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        state = AnimationState.idle;
        timeSinceLastAttack = Time.time;
        timeBetweenAttack = Random.Range(3f, 5f);
        initialPosition = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        switch(state) {
            case AnimationState.idle:
                animator.SetInteger("state", 0);
                if (Time.time - timeSinceLastAttack > timeBetweenAttack) {
                    state = AnimationState.blink;
                }
                break;
            case AnimationState.blink:
                animator.SetInteger("state",1);
                break;
            case AnimationState.idleFalling:
                animator.SetInteger("state", 0);
                transform.position = Vector2.MoveTowards(transform.position, fallPoint.transform.position, Time.deltaTime * speedDown);
                
                if (Vector2.Distance(fallPoint.transform.position, transform.position) < 0.1f) {
                    state = AnimationState.hit;
                }
                break;
            case AnimationState.hit:
                animator.SetInteger("state", 2);
                break;
            case AnimationState.idleRising:
                animator.SetInteger("state", 0);
                transform.position = Vector2.MoveTowards(transform.position, initialPosition, Time.deltaTime * speedUp);
                
                if (Vector2.Distance(initialPosition, transform.position) < 0.1f) {
                    state = AnimationState.idle;
                    timeSinceLastAttack = Time.time;
                }
                break;
            default:
                break;
        }
    }

    private void startDownwardMovement() {
        state = AnimationState.idleFalling;
    }

    private void startUpwardMovement() {
        state = AnimationState.idleRising;
    }

    
}
