using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeLevelText : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    [SerializeField] SecurityConcepts sc;

    void Start()
    {
        textComponent = this.gameObject.GetComponent<TextMeshProUGUI>();
        textComponent.text = GameManager.GetInstance().GetDefenseUpgradeLevel(sc) + "/3";
    }
}
