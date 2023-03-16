using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveAttack : MonoBehaviour
{
    public enum State
    {
        Idle,
        Available,
        Active
    }

    private State state;

    public LayerMask playerMask;

    public GameObject ExclamationMarkIndicator;

    private float timeSinceAvailable;
    private float timeSinceStartingAttack;
    private float lengthOfEvent;


    public abstract void changeToIdleDisplay();
    public abstract void changeToAvailableDisplay();
    public abstract void changeToAttackingDisplay();

    public abstract void onSuccess();

    void passiveAttackEvent(PassiveAttackEvent args)
    {
        lengthOfEvent = args.getLengthOfEvent();
        changeToAvailableDisplay();
        timeSinceAvailable = Time.time;
        state = State.Available;
    }


    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        ExclamationMarkIndicator.SetActive(false);
        changeToIdleDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.Idle:
                break;

            case State.Available:
                if (Time.time - timeSinceAvailable > lengthOfEvent)
                {
                    // Event has gone too long, return to idle state
                    changeToIdleDisplay();
                    state = State.Idle;
                }
                else if (Physics.OverlapSphere(transform.position, 1, playerMask).Length > 0)
                {
                    // Player has begun capturing event
                    changeToAttackingDisplay();
                    timeSinceStartingAttack = Time.time;
                    state = State.Active;
                }
                break;

            case State.Active:
                if (Time.time - timeSinceAvailable > lengthOfEvent)
                {
                    // Event has gone too long, return to idle state
                    changeToIdleDisplay();
                    state = State.Idle;
                }
                else if (Physics.OverlapSphere(transform.position, 1, playerMask).Length > 0)
                {
                    if (Time.time - timeSinceStartingAttack > 5)
                    {
                        // Passive Attack has been captured
                        changeToIdleDisplay();
                        onSuccess();
                        state = State.Idle;
                    }
                }
                else
                {
                    // Player has stopped capturing
                    changeToAvailableDisplay();
                    state = State.Available;
                }
                break;
        }
    }
}
