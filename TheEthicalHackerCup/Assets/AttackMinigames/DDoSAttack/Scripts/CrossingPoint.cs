using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossingPoint : MonoBehaviour
{
    private GameObject player;
    public GameObject gameWinScreen;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Finish");
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Finish"){
            Destroy(player.gameObject);
        }
    }

}
