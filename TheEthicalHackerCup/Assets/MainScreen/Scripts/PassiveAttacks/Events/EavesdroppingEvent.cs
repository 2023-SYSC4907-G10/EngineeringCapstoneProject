using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
Event that is passed to enemies to begin eavesdropping event
*/
public class EavesdroppingEvent
{
    private Vector3 location;
    private float lengthOfEvent;
    // The main eavesdropper will be the enemy who handles interaction with player
    private bool isMainEavesdropper;

    public EavesdroppingEvent(Vector3 location, float lengthOfEvent, bool isMainEavesdropper) {
        this.location = location;
        this.lengthOfEvent = lengthOfEvent;
        this.isMainEavesdropper = isMainEavesdropper;
    }

    public Vector3 getLocation () {
        return this.location;
    }

    public float getLengthOfEvent() {
        return this.lengthOfEvent;
    }

    public bool getIsMainEavesdropper() {
        return this.isMainEavesdropper;
    }


}
