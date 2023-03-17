using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverScreen;

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            gameOverScreen.SetActive(true);
        }
    }

    public void Continue()
    {
        GameManager.GetInstance().ChangeRespect(-10);
        GameManager.GetInstance().SwitchToAfterActionReportScene("DDoS Attack Failed!\n -10 Respect");
    }
}
