using UnityEngine;
using UnityEngine.SceneManagement;
public class DefenseMinigameBackdoor : MonoBehaviour
{
    private readonly bool ENABLED_DEFENSE_MINIGAME_BACKDOOR = true;
    
    void Update()
    {
        if (ENABLED_DEFENSE_MINIGAME_BACKDOOR && Input.GetKeyDown(KeyCode.Backspace))
        {
            if (Input.GetKey(KeyCode.Alpha1)) // Numerical 1
            {
                SceneManager.LoadScene("DDoS_Defense");
            }
            else if (Input.GetKey(KeyCode.Alpha2)) // Numerical 2
            {
                SceneManager.LoadScene("InsiderAttack_Defense");
            }
            else if (Input.GetKey(KeyCode.Alpha3)) // Numerical 3
            {
                SceneManager.LoadScene("Firewall_Defense");
            }
            else if (Input.GetKey(KeyCode.Alpha4)) // Numerical 4
            {
                SceneManager.LoadScene("Phishing_Defense");
            }
            else if (Input.GetKey(KeyCode.Alpha5)) // Numerical 5
            {
                SceneManager.LoadScene("Ransomware_Defense");
            }
        }

    }
}
