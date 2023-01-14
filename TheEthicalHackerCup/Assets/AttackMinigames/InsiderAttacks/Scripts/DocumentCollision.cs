using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collidedObject.layer == LayerMask.NameToLayer("PlayerLayer"))
        {
            InsiderSingleton.GetInstance().ChangeCollectedDocuments(1);
            Destroy(gameObject);
        }
    }
}
