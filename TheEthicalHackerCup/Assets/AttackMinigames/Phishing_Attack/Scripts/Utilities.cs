using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{
    public static bool positionInBounds(Vector3 position) {
        if (position.x > -51f && position.x < 87f && position.y > -28) {
            return true;
        }
        return false;
    }
}
