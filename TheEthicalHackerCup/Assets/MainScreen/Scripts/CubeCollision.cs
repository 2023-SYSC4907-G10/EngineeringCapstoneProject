using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeCollision : MonoBehaviour
{
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    // void FixedUpdate() {
    //     if (Physics.OverlapSphere(transform.position, 1, layerMask).Length > 0) {
    //         Debug.Log(gameObject.name);
    //     }
    // }

    void OnCollisionEnter(Collision other)
    {
        if (Physics.OverlapSphere(transform.position, 1, layerMask).Length > 0) {
            Debug.Log(gameObject.name);
            switch (gameObject.name)
            {
                case "attackMenuCube":
                    SceneManager.LoadScene("AttackMenu");
                    break;
                case "defenseMenuCube":
                    SceneManager.LoadScene("DefenseMenu");
                    break;
            }
          
        }
    }
}