using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLocations
{
    public static Vector3 enemyAreaTopLeft = new Vector3(-0.845f, -0.041f, 4.474f);
    public static Vector3 enemyAreaTopRight = new Vector3(12.247f, -0.041f, 4.474f);
    public static Vector3 enemyAreaBottomRight = new Vector3(12.247f, -0.041f, -4.872f);
    public static Vector3 enemyAreaBottomLeft = new Vector3(-0.845f, -0.041f, -4.872f);

    public static Vector3 getRandomLocationOnEnemyArea() {
        return new Vector3(
            Random.Range(enemyAreaTopLeft.x, enemyAreaTopRight.x),
            enemyAreaBottomLeft.y,
            Random.Range(enemyAreaBottomLeft.z, enemyAreaTopLeft.z)
        );
    }

}
