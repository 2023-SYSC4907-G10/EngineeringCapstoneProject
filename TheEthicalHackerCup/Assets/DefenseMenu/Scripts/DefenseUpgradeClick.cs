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
        var currentUpgradeLevel = GameManager.GetInstance().GetDefenseUpgradeLevel(sc);
        for (var i = 0; i < currentUpgradeLevel; i++)
        {
          progressBar.GetComponent<ProgressBar>().updateProgressBar();
        }

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
