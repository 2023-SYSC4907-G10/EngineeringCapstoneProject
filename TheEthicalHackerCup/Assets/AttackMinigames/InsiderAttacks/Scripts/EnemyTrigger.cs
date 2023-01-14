using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;
    private double suspicionRate;
    // Start is called before the first frame update
    void Start()
    {
        suspicionRate = 0.05;
    }

    // Update is called once per frame
    void Update()
    {   
        int xOffset = enemy.isEnemyFlipped() ? -1 : 0;
        this.GetComponent<BoxCollider2D>().offset = new Vector3(xOffset, this.GetComponent<BoxCollider2D>().offset.y);
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerLayer")) {
            InsiderSingleton.GetInstance().ChangeSuspicionLevel(this.suspicionRate);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
    }
}
