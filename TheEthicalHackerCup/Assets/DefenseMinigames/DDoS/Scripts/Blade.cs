using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    
    private Collider2D bladeCollider;

    private bool isSlicing;

    public Vector3 direction { get; private set; } // public getter, private setter

    public float sliceForce = 5f;

    public float minSliceVelocity = 0.01f;

    private Camera mainCamera;

    private TrailRenderer bladeTrail;

    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider2D>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
        
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            StartSlicing();
        } else if (Input.GetMouseButtonUp(0)){
            StopSlicing();
        } else if (isSlicing){
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        Vector3 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0f;
        transform.position = position;

        isSlicing = true;
        bladeCollider.enabled = true;
        
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    private void StopSlicing()
    {
        isSlicing = false;
        bladeCollider.enabled = false;

        bladeTrail.enabled = false;
        
    }

    private void ContinueSlicing()
    {

        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); // convert screen space to world space, mouse position is 2D so set z axis to 0
        newPosition.z = 0f;

        direction = newPosition - transform.position; // get the direction the blade is moving in

        float velocity = direction.magnitude / Time.deltaTime; // need velocity cuz we wanna disable blade collider if it is not moving
        bladeCollider.enabled = velocity > minSliceVelocity; // enable blade collider if velocity is above a threshold 
        

        transform.position = newPosition;
    }
}
