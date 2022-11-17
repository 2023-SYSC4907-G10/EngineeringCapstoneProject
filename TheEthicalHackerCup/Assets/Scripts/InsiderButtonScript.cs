using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsiderButtonScript : MonoBehaviour
{
    public Button gameButton;
    public GameObject progressBar;
  public void Start(){
    gameButton.onClick.AddListener(Increment);
  }

   void Increment(){
    Debug.Log("Button is Clicked");
    progressBar.GetComponent<InsiderBar>().updateBar();
  }
}
