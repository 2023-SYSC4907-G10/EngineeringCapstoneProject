using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    private Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(EndGameClicked);
    }

    void EndGameClicked()
    {
        GameManager.GetInstance().ChangeRespect(10); //TODO: Make this performance dependant and add \n +/-10 Respect
        GameManager.GetInstance().SwitchToAfterActionReportScene("Insider Defense Game Over");
    }
}
