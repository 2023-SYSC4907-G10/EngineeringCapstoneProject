using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD_Manager : MonoBehaviour
{
    public Slider OppKnowledgeSlider;
    public TextMeshProUGUI OppKnowledgePercentText;
    public Slider RespectSlider;
    public TextMeshProUGUI RespectPercentText;
    

    void Start()
    {
        GameManager currentInstance = GameManager.GetInstance();
        UpdateRespect(currentInstance.GetRespect());
        UpdateOppKnowledge(currentInstance.GetOpponentKnowledge());

        GameManager.OnRespectChange += UpdateRespect;
        GameManager.OnOpponentKnowledgeChange += UpdateOppKnowledge;
    }

    void OnDestroy()
    {
        GameManager.OnRespectChange -= UpdateRespect;
        GameManager.OnOpponentKnowledgeChange -= UpdateOppKnowledge;
    }

    void UpdateRespect(int respect)
    {
        RespectSlider.value = respect;
        RespectPercentText.text = respect + "%";
    }

    void UpdateOppKnowledge(int opponentKnowledge)
    {
        OppKnowledgeSlider.value = opponentKnowledge;
        OppKnowledgePercentText.text = opponentKnowledge + "%";
    }
}
