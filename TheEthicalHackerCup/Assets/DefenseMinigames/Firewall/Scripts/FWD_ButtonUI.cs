using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FWD_ButtonUI : MonoBehaviour
{

    public GameObject PregamePanel;
    public GameObject PanelHUD;

    public TextMeshProUGUI CanvasText;

    public void HandleContinueButtonClick()
    {
        PregamePanel.SetActive(false); // Hiding Canvas UI layers
        PanelHUD.SetActive(true); //Showing the HUD
        FWD_Manager.GetInstance().StartGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        CanvasText.text = "Welcome to Firewall Defense!\n";
        CanvasText.text += "Your current upgrade level is "+ FWD_Manager.GetInstance().DifficultyLevel.DifficultyLevel +"\n";
    }

}
