using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerCollision : MonoBehaviour
{
    [SerializeField] private ProgressBar pb;
    IEnumerator progress;
    private bool hasFinished;
    // Start is called before the first frame update
    void Start()
    {
        progress = pb.delayedProgressBar(1);
        hasFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pb.isComplete() && !hasFinished) {
            InsiderSingleton.GetInstance().ChangeCollectedComputers(1);
            hasFinished = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.layer == LayerMask.NameToLayer("PlayerLayer"))
        {
            StartCoroutine(progress);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collidedObject.layer == LayerMask.NameToLayer("PlayerLayer"))
        {
            StopCoroutine(progress);
        }
    }
}
