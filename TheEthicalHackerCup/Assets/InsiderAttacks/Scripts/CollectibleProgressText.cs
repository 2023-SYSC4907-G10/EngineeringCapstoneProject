using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectibleProgressText : MonoBehaviour {
    private TextMeshProUGUI collectibleText;
    private int totalCollectibles;
    private int collectedCollectibles;
    [SerializeField]
    private Collectible collectibleEnum;

    private int TOTAL_SUSPICION_LEVEL = 100;
    
    private void Start() {
        collectibleText = this.gameObject.GetComponent<TextMeshProUGUI>();
        updateText();
    }

    private void Update() {
        updateText();
    }

    private void updateText() {
        switch (collectibleEnum) {
            case Collectible.DOCUMENT:
                totalCollectibles = InsiderSingleton.GetInstance().GetTotaldDocuments();
                collectedCollectibles = InsiderSingleton.GetInstance().GetCollectedDocuments();
                break;
            case Collectible.COMPUTER:
                totalCollectibles = InsiderSingleton.GetInstance().GetTotalComputers();
                collectedCollectibles = InsiderSingleton.GetInstance().GetCollectedComputers();
                break;
            case Collectible.SERVER:
                totalCollectibles = InsiderSingleton.GetInstance().GetTotalServers();
                collectedCollectibles = InsiderSingleton.GetInstance().GetCollectedServers();
                break;
            case Collectible.SUSPICION:
                totalCollectibles = TOTAL_SUSPICION_LEVEL;
                collectedCollectibles = (int) InsiderSingleton.GetInstance().GetSuspicionLevel();
                break;
        }
        collectibleText.text = collectedCollectibles + "/" + totalCollectibles;
    }
}