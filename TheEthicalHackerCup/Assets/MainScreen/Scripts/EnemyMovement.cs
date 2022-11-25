using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static KeyLocations;
public class EnemyMovement : MonoBehaviour
{
    public enum State
    {
        PreparingToWalk,
        Walking,
        Standing,
        WalkingToTalkingDestination,
        TalkingAvailable,
        TalkingActive,
        TalkingIdle
    }
    public NavMeshAgent agent;
    public Rigidbody player;

    public LayerMask enemyMask;
    public LayerMask playerMask;
    private State state;


    private Vector3 walkingDestination;
    private Vector3 talkingDestination;
    private float startStandingTime;
    private float startTalkingTime;
    private float timeSinceStartingAttack;

    public Renderer agentRenderer;
    public Material normal;
    public Material available;
    public Material active;

    public bool isMainEavesdropper;

    public float lengthOfEavesdroppingEvent;


    // Start is called before the first frame update
    void Start()
    {
        state = State.Standing;
    }

    // Calling this method forces the enemy to go initiate a talking event
    void eavesdroppingEvent(EavesdroppingEvent args)
    {
        talkingDestination = args.getLocation();
        agent.SetDestination(talkingDestination);
        state = State.WalkingToTalkingDestination;
        isMainEavesdropper = args.getIsMainEavesdropper();
        lengthOfEavesdroppingEvent = args.getLengthOfEvent();
    }

    void changeToAttackingDisplay()
    {
        agentRenderer.material = active;
    }

    void changeToAvailableDisplay()
    {
        agentRenderer.material = available;
    }

    void changeToNormalDisplay()
    {
        agentRenderer.material = normal;
    }

    void onSuccess()
    {
        GameManager.GetInstance().ChangeOpponentKnowledge(PassiveAttack.SUCCESS_OPP_KNOWLEDGE_INCREASE);
        Debug.Log("Succesfully captured eavesdropping attack");
        Debug.Log("Opp knowledge: " + GameManager.GetInstance().GetOpponentKnowledge());
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.PreparingToWalk:
                walkingDestination = KeyLocations.getRandomLocationOnEnemyArea();
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

            case State.WalkingToTalkingDestination:

                if (
                    // If we reach our walking destination, we will begin standing
                    Mathf.Abs(agent.transform.position.x - talkingDestination.x) < 1 &
                    Mathf.Abs(agent.transform.position.z - talkingDestination.z) < 1
                )
                {
                    startTalkingTime = Time.time;
                    changeToAvailableDisplay();
                    state = State.TalkingAvailable;
                }
                break;

            case State.TalkingAvailable:
                // This state is during an eavesdropping event
                if (Time.time - startTalkingTime > lengthOfEavesdroppingEvent)
                {
                    changeToNormalDisplay();
                    state = State.PreparingToWalk;
                }
                else if (
                    Physics.OverlapSphere(transform.position, 1, playerMask).Length > 0 &
                    isMainEavesdropper == true)
                {
                    // A player has begun capturing the eavesdropping event
                    changeToAttackingDisplay();
                    timeSinceStartingAttack = Time.time;
                    state = State.TalkingActive;
                }

                break;

            case State.TalkingActive:
                if (Physics.OverlapSphere(transform.position, 1, playerMask).Length > 0)
                {
                    if (Time.time - startTalkingTime > lengthOfEavesdroppingEvent)
                    {
                        changeToNormalDisplay();
                        state = State.PreparingToWalk;
                    }
                    else if (Time.time - timeSinceStartingAttack > 5)
                    {
                        // Enemy has eavedropped converstation
                        changeToNormalDisplay();
                        onSuccess();
                        state = State.TalkingIdle;
                    }
                }
                else
                {
                    changeToAvailableDisplay();
                    state = State.TalkingAvailable;
                }
                break;

            case State.TalkingIdle:
                if (Time.time - startTalkingTime > lengthOfEavesdroppingEvent)
                {
                    changeToNormalDisplay();
                    state = State.PreparingToWalk;
                }
                break;

        }
    }
}
