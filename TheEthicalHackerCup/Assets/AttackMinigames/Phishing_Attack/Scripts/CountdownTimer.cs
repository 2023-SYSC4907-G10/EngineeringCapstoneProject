using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    private float currentTime;
    private float startingTime = 20f;

    public Text countdownText;

    public GameObject control;  

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0f;
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        if (currentTime >= 0) {
            countdownText.text = currentTime.ToString("0");
        } else {
            control.SendMessage("outOfTime");
        }
    }
}
