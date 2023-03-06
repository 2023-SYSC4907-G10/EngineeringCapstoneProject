using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLocations
{
    public static Vector3 enemyAreaTopLeft = new Vector3(-0.845f, -0.041f, 4.474f);
    public static Vector3 enemyAreaTopRight = new Vector3(12.247f, -0.041f, 4.474f);
    public static Vector3 enemyAreaBottomRight = new Vector3(12.247f, -0.041f, -4.872f);
    public static Vector3 enemyAreaBottomLeft = new Vector3(-0.845f, -0.041f, -4.872f);
    public static Vector3 playerAreaTopLeft = new Vector3(-11.36f, -0.041f, 2.93f);
    public static Vector3 playerAreaTopRight = new Vector3(-1.41f, -0.041f, 2.93f);
    public static Vector3 playerAreaBottomRight = new Vector3(-1.41f, -0.041f, -4.15f);
    public static Vector3 playerAreaBottomLeft = new Vector3(-11.31f, -0.041f, -3.81f);
    public static Vector3 playerStartingArea = new Vector3(-4.64f, 0.78f, 0f);

    public static Vector3 getRandomLocationOnEnemyArea() {
        return new Vector3(
            Random.Range(enemyAreaTopLeft.x, enemyAreaTopRight.x),
            enemyAreaBottomLeft.y,
            Random.Range(enemyAreaBottomLeft.z, enemyAreaTopLeft.z)
        );
    }

    public static Vector3 getRandomLocationOnPlayerArea() {
        return new Vector3(
            Random.Range(playerAreaTopLeft.x, playerAreaTopRight.x),
            playerAreaBottomLeft.y,
            Random.Range(playerAreaBottomLeft.z, playerAreaTopLeft.z)
        );
    }

}
