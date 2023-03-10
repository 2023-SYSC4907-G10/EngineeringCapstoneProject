using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HUD_IncomingAttack : MonoBehaviour
{
    public TextMeshProUGUI SecurityConceptText;
    public GameObject IncommingAttackPanel;
    private bool _isWarningActive;

    private float _timeUntilNextRespectPointLoss;
    private static readonly float RESPECT_POINT_LOSS_PERIOD = 2;
    private float _timeUntilNextDefenseMinigame;
    private static readonly float DEFENSE_MINIGAME_MINIMUM_DELAY = 20;


    // Start is called before the first frame update
    void Start()
    {
        _timeUntilNextRespectPointLoss = RESPECT_POINT_LOSS_PERIOD;
        _timeUntilNextDefenseMinigame = DEFENSE_MINIGAME_MINIMUM_DELAY;
        HideWarningPanel();
        ShowWarningPanel(GameManager.GetInstance().GetNextDefenseMinigame());
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputsForWarning();
    }

    void HandleInputsForWarning()
    {
        if (_isWarningActive)
        {
            _timeUntilNextRespectPointLoss -= Time.deltaTime;
            if (_timeUntilNextRespectPointLoss <= 0)
            {
                GameManager.GetInstance().ChangeReputation(-1);
                _timeUntilNextRespectPointLoss = RESPECT_POINT_LOSS_PERIOD;
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                // Ignore warning
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
