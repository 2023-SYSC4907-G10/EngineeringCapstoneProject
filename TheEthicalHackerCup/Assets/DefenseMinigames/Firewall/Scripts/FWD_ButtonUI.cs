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
        CanvasText.text = "TODO: Build a string based on upgrade level";
    }

}
