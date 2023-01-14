using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
Event that is passed to enemies to begin eavesdropping event
*/
public class HitEvent
{
    private Vector3 location;
    private float lengthOfEvent;
    // The main eavesdropper will be the enemy who handles interaction with player
    private bool isMainEavesdropper;

    public HitEvent(Vector3 location) {
        this.location = location;
    }

    public Vector3 getLocation () {
        return this.location;
    }
}
