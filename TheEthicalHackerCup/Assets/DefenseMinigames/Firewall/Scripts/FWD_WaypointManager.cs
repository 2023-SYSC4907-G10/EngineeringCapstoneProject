using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWD_WaypointManager : MonoBehaviour
{
    public FWD_Waypoint rootWaypoint;
    public List<FWD_Waypoint> secondWaypoints; //Targets 1,2 and junction to 3,4,5
    public List<FWD_Waypoint> thirdWaypoints; //Targets 3,4,5




    // Start is called before the first frame update
    void Start()
    {
        FWD_Manager.GetInstance().WaypointManager = this;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Queue<FWD_Waypoint> GetWaypointPath()
    {
        // Every packet will get a 3 waypoint path. If they arrive at T1/T2, then they will be deleted and won't even nav to the 3rd waypoint
        Queue<FWD_Waypoint> waypoints = new Queue<FWD_Waypoint>();

        waypoints.Enqueue(rootWaypoint);
        waypoints.Enqueue(secondWaypoints[Random.Range(0, secondWaypoints.Count)]);
        waypoints.Enqueue(thirdWaypoints[Random.Range(0, thirdWaypoints.Count)]);

        return waypoints;
    }
}
