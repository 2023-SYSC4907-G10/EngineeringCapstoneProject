using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHeatBar : MonoBehaviour
{
    public Button gameButton;
    public Slider slider;
    [SerializeField] SecurityConcepts sc;

    void Start()
    {
        slider.value = GameManager.GetInstance().GetAttackSpecificHeat(sc);
        if (GameManager.GetInstance().GetOpponentKnowledge() >= 10)
        {
            gameButton.onClick.AddListener(HandleClick_StartAttackMinigame);
        }
    }

    void HandleClick_StartAttackMinigame()
    {
        GameManager.GetInstance().ChangeAttackSpecificHeat(sc, 10); // Heat up the selected security concept
        foreach (SecurityConcepts concept in Enum.GetValues(typeof(SecurityConcepts)))
        {
            if (concept != sc) // Reduce heat of all other security concepts
            {
                GameManager.GetInstance().ChangeAttackSpecificHeat(concept, -5);
            }
        }
        GameManager.GetInstance().AttemptAttackMinigame(sc);
        GameManager.GetInstance().ChangeOpponentKnowledge(-10);
        SceneManager.LoadScene(sc + "_Attack");

    }
}
