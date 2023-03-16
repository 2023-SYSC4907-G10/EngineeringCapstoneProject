using UnityEngine;

public class NoOppKnowledgeMessage : MonoBehaviour
{
    void Start()
    {
        // Only show if there is not enough opp knowledge to attempt anything
        gameObject.SetActive(GameManager.GetInstance().GetOpponentKnowledge() < 10);
    }
}
