using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour
{
    public GameObject gameWinScreen;

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Finish") == null)
        {
            gameWinScreen.SetActive(true);
        }
    }

    public void Continue()
    {
        GameManager.GetInstance().AfterActionReportText = "DDoS Attack Completed Dude";
    }
}
