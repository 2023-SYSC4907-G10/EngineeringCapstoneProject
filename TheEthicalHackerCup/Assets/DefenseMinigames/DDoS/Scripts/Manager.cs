using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    public TMP_Text scoreText;

    public TMP_Text countDownText;

    private int score;

    private float currentTime;

    private Blade blade;
    private Spawner spawner;

    public GameObject gameOverScreen;

    public GameObject gameWinScreen;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
    }

    private void Start()
    {
        NewGame();
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        if (currentTime >= 0) {
            countDownText.text = currentTime.ToString("0");
        } else {
            GameWin();
        }
    }

    private void NewGame()
    {
        score = 0;
        scoreText.text = score.ToString();

        currentTime = 35f;
        countDownText.text = currentTime.ToString();

    }
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        blade.enabled = false;
        spawner.enabled = false;
        gameOverScreen.SetActive(true);
    }

    public void GameWin()
    {
        blade.enabled = false;
        spawner.enabled = false;
        gameWinScreen.SetActive(true);
    }

    public void Continue()
    {
        GameManager.GetInstance().ChangeRespect(-10);
        GameManager.GetInstance().SwitchToAfterActionReportScene("DDoS Defense Failed.");
    }


    public void GoodContinue()
    {
        GameManager.GetInstance().ChangeRespect(10);
        GameManager.GetInstance().SwitchToAfterActionReportScene("DDoS Defense Successful.");
    }
}
