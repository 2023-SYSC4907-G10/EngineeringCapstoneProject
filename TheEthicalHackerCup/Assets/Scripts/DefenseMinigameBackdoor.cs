using UnityEngine;
using UnityEngine.SceneManagement;
public class DefenseMinigameBackdoor : MonoBehaviour
{
    private readonly bool ENABLED_DEFENSE_MINIGAME_BACKDOOR = true;

    private void StartTutorial(SecurityConcepts sc)
    {
        if (!GameManager.GetInstance().GetTutorialSeen(sc.ToString()))
        {
            TutorialInit.DefenseMinigame(sc);
            GameManager.GetInstance().SetTutorialSeen(sc.ToString());
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            SceneManager.LoadScene(sc + "_Defense");
        }
    }

    void Update()
    {
        if (ENABLED_DEFENSE_MINIGAME_BACKDOOR && Input.GetKeyDown(KeyCode.Backspace))
        {
            if (Input.GetKey(KeyCode.Alpha1)) // Numerical 1
            {
                StartTutorial(SecurityConcepts.DDoS);
            }
            else if (Input.GetKey(KeyCode.Alpha2)) // Numerical 2
            {
                StartTutorial(SecurityConcepts.InsiderAttack);
            }
            else if (Input.GetKey(KeyCode.Alpha3)) // Numerical 3
            {
                StartTutorial(SecurityConcepts.Firewall);
            }
            else if (Input.GetKey(KeyCode.Alpha4)) // Numerical 4
            {
                StartTutorial(SecurityConcepts.Phishing);
            }
            else if (Input.GetKey(KeyCode.Alpha5)) // Numerical 5
            {
               StartTutorial(SecurityConcepts.Ransomware);
            }
        }

    }
}
