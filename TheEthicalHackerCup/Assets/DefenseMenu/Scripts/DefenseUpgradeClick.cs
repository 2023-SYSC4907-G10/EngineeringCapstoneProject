using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefenseUpgradeClick : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    private Button upgradeButton;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private AttackAttemptText attackAttemptText;

    [SerializeField] private Sprite enabledIcon;
    [SerializeField] private Sprite disabledIcon;
    [SerializeField] SecurityConcepts sc;
    void Start()
    {
        upgradeButton = this.gameObject.GetComponent<Button>();
        upgradeButton.onClick.AddListener(Upgrade);
    }

    void Update()
    {
        upgradeButton.interactable = attackAttemptText.isAttemptComplete();
        updateImage(upgradeButton.interactable);
    }

    void Upgrade()
    {
        if (upgradeButton.interactable)
        {
            progressBar.GetComponent<ProgressBar>().updateProgressBar(); //This might not need to be dynamically boosted. Instead, set it when the scene loads

            // Launch learning minigame 
            GameManager.GetInstance().StartLearningMinigameUpgradeQuiz(sc);
        }
    }

    void updateImage(bool buttonEnabled)
    {
        Image buttonImage = upgradeButton.GetComponent<Image>();
        Image[] images = upgradeButton.GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            if (image != buttonImage && buttonEnabled)
            {
                image.sprite = enabledIcon;
            }
            else
            {
                image.sprite = disabledIcon;
            }
        }
    }
}
