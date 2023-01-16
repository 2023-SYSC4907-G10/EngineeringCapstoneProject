using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AfterActionReportButton : MonoBehaviour
{
    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(ProceedToMainScene);
    }

    public void ProceedToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
