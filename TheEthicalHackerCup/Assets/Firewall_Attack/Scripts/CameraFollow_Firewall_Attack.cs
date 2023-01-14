using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow_Firewall_Attack : MonoBehaviour
{
    public float smoothSpeed = 0.125f;
    public Transform iceCube;
    public Vector3 cameraOffset;


    void FixedUpdate()
    {
        Vector3 desiredPosition = cameraOffset + new Vector3(iceCube.position.x, iceCube.position.y, 0);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = cameraOffset + new Vector3(iceCube.position.x, iceCube.position.y, 0);
    }
}
