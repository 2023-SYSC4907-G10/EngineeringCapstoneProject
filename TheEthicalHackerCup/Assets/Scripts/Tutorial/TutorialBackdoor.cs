using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TutorialBackdoorEnum
{
    AttackMenu,
    DefenseMenu,
    DDoS,
    InsiderAttack,
    Firewall,
    Phishing,
    Ransomware,
}

public class TutorialBackdoor : MonoBehaviour
{
    [SerializeField] private TutorialBackdoorEnum scene;
    [SerializeField] private bool isDefenseGame;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (scene == TutorialBackdoorEnum.AttackMenu)
            {
                TutorialInit.AttackMenu();
            }
            else if (scene == TutorialBackdoorEnum.DefenseMenu)
            {
                TutorialInit.DefenseMenu();
            }
            else
            {
                SecurityConcepts scNew = SecurityConcepts.Firewall;
                foreach (SecurityConcepts sc in Enum.GetValues(typeof(SecurityConcepts)))
                {
                    if (scene.ToString() == sc.ToString())
                    {
                        scNew = sc;
                        break;
                    }
                }
                if (isDefenseGame)
                {
                    TutorialInit.DefenseMinigame(scNew);
                }
                else
                {
                    TutorialInit.AttackMinigame(scNew);
                }
            }


            GameManager.GetInstance().ResetTutorialSeen(scene.ToString());
            SceneManager.LoadScene("Tutorial");
            GameManager.GetInstance().SetTutorialSeen(scene.ToString());
        }
    }
}