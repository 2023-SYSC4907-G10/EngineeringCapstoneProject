using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspectImage : MonoBehaviour
{
    Sprite culpritImg;
    // Start is called before the first frame update
    void Start()
    {
        SuspectEnum? se = InsiderDefenseSingleton.GetInstance().getAccusedCulprit();
        switch (se) {
            case SuspectEnum.Stanley:
                culpritImg = Resources.Load<Sprite>("InsiderDefense/EnemySprites/black");
                break;
            case SuspectEnum.Ruth:
                culpritImg = Resources.Load<Sprite>("InsiderDefense/EnemySprites/black2");
                break;
            case SuspectEnum.Patricia:
                culpritImg = Resources.Load<Sprite>("InsiderDefense/EnemySprites/mexican");
                break;
            case SuspectEnum.Jessica:
                culpritImg = Resources.Load<Sprite>("InsiderDefense/EnemySprites/white");
                break;
        }

        gameObject.GetComponent<Image>().sprite = culpritImg;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
