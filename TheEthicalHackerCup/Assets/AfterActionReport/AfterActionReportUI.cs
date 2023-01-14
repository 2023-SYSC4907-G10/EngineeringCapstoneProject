using UnityEngine;
using TMPro;

public class AfterActionReportUI : MonoBehaviour
{
    void Start()
    {
        TextMeshProUGUI Tmp = gameObject.GetComponent<TextMeshProUGUI>();
        Tmp.text = GameManager.GetInstance().AfterActionReportText;
    }
}
