using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HUD_IncomingAttack : MonoBehaviour
{
    public TextMeshProUGUI SecurityConceptText;
    public GameObject IncommingAttackPanel;
    private bool _isWarningActive;
    // Start is called before the first frame update
    void Start()
    {
        HideWarningPanel();
        ShowWarningPanel(SecurityConcepts.DDoS); // JUST FOR TESTING
    }

    // Update is called once per frame
    void Update()
    {
        if (_isWarningActive)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                // Ignore warning
                Debug.Log("HandleIgnoreButtonClick");
                HideWarningPanel();
                GameManager.GetInstance().ChangeReputation(-20);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                // Defend
                // TODO: Switch to the defense scene of the incoming attack
                // TODO: Update the incoming attack log
                Debug.Log("HandleDefendButtonClick");
            }
        }
    }

    void ShowWarningPanel(SecurityConcepts securityConcept)
    {
        _isWarningActive = true;
        IncommingAttackPanel.SetActive(true);
        SecurityConceptText.text = securityConcept.ToString();
    }
    void HideWarningPanel()
    {
        _isWarningActive = false;
        IncommingAttackPanel.SetActive(false);
    }
}
