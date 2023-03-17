using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class HUD_IncomingAttack : MonoBehaviour
{
    public TextMeshProUGUI SecurityConceptText;
    public GameObject IncommingAttackPanel;
    private bool _isWarningActive;

    private float _timeUntilNextRespectPointLoss;
    private static readonly float RESPECT_POINT_LOSS_PERIOD = 2;
    private float _timeUntilNextDefenseMinigame;
    private static readonly float DEFENSE_MINIGAME_MINIMUM_DELAY = 5;
    private SecurityConcepts _currentDefenseMinigame;


    // Start is called before the first frame update
    void Start()
    {
        _timeUntilNextRespectPointLoss = RESPECT_POINT_LOSS_PERIOD;
        _timeUntilNextDefenseMinigame = DEFENSE_MINIGAME_MINIMUM_DELAY;
        _currentDefenseMinigame = SecurityConcepts.Firewall;
        ShowWarningPanel(_currentDefenseMinigame);
        // HideWarningPanel();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputsForWarning();
        HandleDynamicWarningSpawn();
    }

    void HandleInputsForWarning()
    {
        if (_isWarningActive)
        {
            _timeUntilNextRespectPointLoss -= Time.deltaTime;
            if (_timeUntilNextRespectPointLoss <= 0)
            {
                GameManager.GetInstance().ChangeRespect(-1);
                _timeUntilNextRespectPointLoss = RESPECT_POINT_LOSS_PERIOD;
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                // Ignore warning
                HideWarningPanel();
                GameManager.GetInstance().ChangeRespect(-20);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                // Defend
                if (!GameManager.GetInstance().GetTutorialSeen(_currentDefenseMinigame.ToString() + "_Defense"))
                {
                    TutorialInit.DefenseMinigame(_currentDefenseMinigame);
                    GameManager.GetInstance().SetTutorialSeen(_currentDefenseMinigame.ToString() + "_Defense");
                    SceneManager.LoadScene("Tutorial");
                }
                else
                {
                    SceneManager.LoadScene(_currentDefenseMinigame + "_Defense");
                }
            }
        }
    }

    void HandleDynamicWarningSpawn()
    {
        _timeUntilNextDefenseMinigame -= Time.deltaTime;
        if (!_isWarningActive && _timeUntilNextDefenseMinigame <= 0 && GameManager.GetInstance().GetOpponentKnowledge() > 0)
        {
            _currentDefenseMinigame = GameManager.GetInstance().GetNextDefenseMinigame();
            ShowWarningPanel(_currentDefenseMinigame);
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

        // Random period between 5s - 25s after rejections
        _timeUntilNextDefenseMinigame = DEFENSE_MINIGAME_MINIMUM_DELAY + Random.Range(0f, 20f);
    }
}
