using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolsButton : MonoBehaviour
{
    [SerializeField] ToolEnum te;
    [SerializeField] int dl;
    private Button btn;
    private TextMeshProUGUI text;
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(ToolClicked);
        text = btn.GetComponentInChildren<TextMeshProUGUI>(); 
    }

    private void Update() {
        int currDefenseLevel = GameManager.GetInstance().GetDefenseUpgradeLevel(SecurityConcepts.InsiderAttack);
        if (currDefenseLevel < dl) {
            btn.interactable = false;
            text.text = "Requires defense level " + dl;
        }
    }

    void ToolClicked()
    {
        InsiderDefenseSingleton.GetInstance().setSelectedTool(te);
    }
}
