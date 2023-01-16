using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{

    public enum State
    {
        PreparingToMoveLeft,
        MovingLeftToWall,
        PreparingToMoveRight,
        MovingRightToWall,
        Hit,
        Death
    }


    private State state;
    private Vector2 destination;

    private Vector3 hitLocation;

    public GameObject control;

    void hitEvent(HitEvent args)
    {
        hitLocation = args.getLocation();
        state = State.Hit;
    }

    // Start is called before the first frame update
    void Start()
    {
        // x -50 81
        // y -14 55
        // 0 - start on left
        // 1 - start on right
        int startingSide = Random.Range(0,2);

        if (startingSide == 0) {
            float startingVerticalLocation = Random.Range(-14f, 55f);
            Vector2 startPosition = new Vector2(-50f, startingVerticalLocation);
            transform.position = startPosition;
            transform.Rotate(new Vector3(0,180,0));
            state = State.PreparingToMoveRight;
        }

        if (startingSide == 1) {
            float startingVerticalLocation = Random.Range(-14f, 55f);
            Vector2 startPosition = new Vector2(81f, startingVerticalLocation);
            transform.position = startPosition;
            state = State.PreparingToMoveLeft;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.PreparingToMoveLeft:
                float randomVerticalLocation = Random.Range(-14f, 55f);
                destination = new Vector2(-50f, randomVerticalLocation);
                state = State.MovingLeftToWall;
                break;

            case State.MovingLeftToWall:
                float speed = 25f;
                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, destination, step);

                if (transform.position.x == destination.x && transform.position.y == destination.y) {
                    transform.Rotate(new Vector3(0,180,0));
                    state = State.PreparingToMoveRight;
                }
                break;

            case State.PreparingToMoveRight:
                randomVerticalLocation = Random.Range(-14f, 55f);
                destination = new Vector2(81f, randomVerticalLocation);
                state = State.MovingRightToWall;
                break;
            case State.MovingRightToWall:
                speed = 25f;
                step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, destination, step);

                if (transform.position.x == destination.x && transform.position.y == destination.y) {
                    transform.Rotate(new Vector3(0,180,0));
                    state = State.PreparingToMoveLeft;
                }
                break;

            case State.Hit:
                transform.position = Vector3.MoveTowards(transform.position, hitLocation, 0.1f);
                if (transform.position.x == hitLocation.x && transform.position.y == hitLocation.y) {
                    transform.localRotation = Quaternion.Euler(0f, 0f, 270f);
                    state = State.Death;
                }
                break;

            case State.Death:
                control.SendMessage("fishDied");
                Destroy(this.gameObject);
                break;
        
        }
    }
}
