using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utilities;

public class Shooter : MonoBehaviour
{
    // Start is called before the first frame update

    public enum State
    {
        AimingLeft,
        AimingRight,
        Shooting,
        Hit
    }

    public enum ShootingMode
    {
        SpearFish,
        Phishing,
        Harpooning
    }

    private State state;
    private ShootingMode shootingMode;
    private Vector3 initialPosition;
    void Start()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, 270f);
        initialPosition = transform.position;
        state = State.AimingLeft;
        shootingMode = ShootingMode.SpearFish;
    }

    void selectSpearFishing() {
        shootingMode = ShootingMode.SpearFish;
    }

    void selectPhishing() {
        shootingMode = ShootingMode.Phishing;
    }

    void selectHarpooning() {
        shootingMode = ShootingMode.Harpooning;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) {
            state = State.Shooting;
        }


        switch (state)
        {
            default:
            case State.AimingLeft:
                if (transform.localRotation.eulerAngles.z <= 180) {
                    state = State.AimingRight;
                }
                transform.Rotate(0f, 0f, -0.25f);
                break;

            case State.AimingRight:
                if (transform.localRotation.eulerAngles.z > 355) {
                    state = State.AimingLeft;
                }
                transform.Rotate(0f, 0f, 0.25f);
                break;

            case State.Shooting:
                transform.Translate(Vector3.right * Time.deltaTime * 25f);
                // We have missed target
                if (!positionInBounds(transform.position)) {
                    transform.localRotation = Quaternion.Euler(0f, 0f, 270f);
                    transform.position = initialPosition;
                    state = State.AimingLeft;
                }
                break;
            
            case State.Hit:
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, 0.1f);
                if (transform.position.x == initialPosition.x && transform.position.y == initialPosition.y) {
                    transform.localRotation = Quaternion.Euler(0f, 0f, 270f);
                    state = State.AimingLeft;
                }
                break;
                
        
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (state == State.Shooting) {
            Debug.Log("hit detected");
            if (collider.gameObject.tag == "Fish" && shootingMode != ShootingMode.Phishing) {
                state = State.Hit;
                Debug.Log("Wrong tool");
                return;
            } else if (collider.gameObject.tag == "SpearFish" && shootingMode != ShootingMode.SpearFish) {
                state = State.Hit;
                Debug.Log("Wrong tool");
                return;
            } else if (collider.gameObject.tag == "Whale" && shootingMode != ShootingMode.Harpooning) {
                state = State.Hit;
                Debug.Log("Wrong tool");
                return;
            }

            collider.SendMessage("hitEvent", new HitEvent(initialPosition));
            state = State.Hit;
        }
        
    }
}
