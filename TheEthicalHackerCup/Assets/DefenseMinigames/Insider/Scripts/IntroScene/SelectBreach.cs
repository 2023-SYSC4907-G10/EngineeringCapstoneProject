using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectBreach : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
		btn.onClick.AddListener(InvestigateClicked);
    }

    void InvestigateClicked() {
        BreachType? breachType = (BreachType?)Random.Range(0, 3);
        InsiderDefenseSingleton.GetInstance().setBreachType(breachType);

        SuspectEnum? culprit = (SuspectEnum?)Random.Range(0, 4);
        InsiderDefenseSingleton.GetInstance().setCuplrit(culprit);
        InsiderDefenseSingleton.GetInstance().setCanvasEnum(CanvasEnum.Main);
    }
}
