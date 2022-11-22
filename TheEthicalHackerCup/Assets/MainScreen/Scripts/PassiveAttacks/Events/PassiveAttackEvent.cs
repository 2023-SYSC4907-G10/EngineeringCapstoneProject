using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Event that is sent to invoke passive attack
*/
public class PassiveAttackEvent
{

    private float lengthOfEvent;
    // Start is called before the first frame update
    public PassiveAttackEvent(float lengthOfEvent) {
        this.lengthOfEvent = lengthOfEvent;
    }

    public float getLengthOfEvent() {
        return this.lengthOfEvent;
    }
}
