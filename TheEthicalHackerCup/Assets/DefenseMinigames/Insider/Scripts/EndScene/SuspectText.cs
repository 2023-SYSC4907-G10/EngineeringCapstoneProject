using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SuspectText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        bool suspectCorrect = InsiderDefenseSingleton.GetInstance().isCulpritCorrect();

        if (suspectCorrect)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = "suspect caught";
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(50, 255, 50, 255);
        } else {
            gameObject.GetComponent<TextMeshProUGUI>().text = "suspect not caught";
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 50, 50, 255);
        }
    }
}
