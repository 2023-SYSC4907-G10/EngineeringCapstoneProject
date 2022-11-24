using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHeatBar : MonoBehaviour
{
    public Button gameButton;
    public GameObject heatBar;

    // Start is called before the first frame update
    void Start()
    {
        gameButton.onClick.AddListener(Increment);
    }

    void Increment(){
    Debug.Log("Button is Clicked");
    heatBar.GetComponent<HeatBar>().IncrementProgress();
  }
}
