using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHeatBar : MonoBehaviour
{
    public Button gameButton;
    public GameObject heatBar;
    [SerializeField] SecurityConcepts sc;

    // Start is called before the first frame update
    void Start()
    {
        gameButton.onClick.AddListener(Increment);
    }

    void Increment()
    {
        Debug.Log(sc + " Button is Clicked");
        heatBar.GetComponent<HeatBar>().IncrementProgress();

        /*
          TODO: 
          -Review heat system with group 
            - Constant vs exponential vs other
            - how much to increase per attempt
            - How to decrease it
        */
        /*
          TODO:
          -Make the heat bars load dynamically with the GameManager value for the sec concept
        */

        GameManager.GetInstance().ChangeAttackSpecificHeat(sc, 10);
        GameManager.GetInstance().AttemptAttackMinigame(sc);
        /*
          TODO: 
            Switch to the corresponding attack minigame scene
          Proposal: 
            Title atk minigame scenes like DDoS_Attack, Firewall_Attack, InsiderAttack_Attack, Phishing_Attack, Ransomware_Attack
            That way the enum can be used directly with the "_Attack" string appended afterwareds
        */

    }
}
