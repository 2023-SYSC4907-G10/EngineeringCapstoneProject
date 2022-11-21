using UnityEngine;
using UnityEngine.UI;

public class DefenseUpgradeClick : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    private Button upgradeButton;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private AttackAttemptText attemptAttemptText;

    [SerializeField] private Sprite enabledIcon;
    [SerializeField] private Sprite disabledIcon;

    void Start()
    {
        upgradeButton = this.gameObject.GetComponent<Button>();
        upgradeButton.onClick.AddListener(Upgrade);
    }

    void Update()
    {
        upgradeButton.interactable = attemptAttemptText.isAttemptComplete();
        updateImage(upgradeButton.interactable);
    }

    void Upgrade()
    {
        if (upgradeButton.interactable)
        {
            // GameManager.GetInstance().SetNextLearningMinigameSecurityConcept(...);Sec concept associated with this button
            // GameManager.GetInstance().SetNextLearningMinigameFilename(...); Question set filename associated with the curernt concept and upgrade level

            // Launch learning minigame 

            progressBar.GetComponent<ProgressBar>().updateProgressBar();
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
