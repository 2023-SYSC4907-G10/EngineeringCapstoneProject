using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.FilePathAttribute;

public class patrol : MonoBehaviour
{


    public GameObject[] patrolPoints;
    public int currentWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        if (patrolPoints.Length != 0)
            moveToPoint();
    }

    private void moveToPoint()
    {
        var agent = GetComponent<NavMeshAgent>();
        var destObj = patrolPoints[currentWaypoint];
        var destTrans = destObj.transform;
        var destPos = destTrans.position;
        agent.SetDestination(destPos);
    }

    private void updateWaypoint() {
        currentWaypoint++;
        currentWaypoint %= patrolPoints.Length;
    }

    public void OnTriggerEnter(Collider other)
    {
        var otherCollider = patrolPoints[currentWaypoint].GetComponent<Collider>();
        if (other == otherCollider) {
            updateWaypoint();
            moveToPoint();
        }
    }

}
