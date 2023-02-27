using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FWD_BadPacketsReceivedText : MonoBehaviour
{
    public TextMeshProUGUI BadPacketsReceivedText;
    // Start is called before the first frame update
    void Start()
    {
        FWD_Manager.OnBadPacketsReceivedChange += UpdateBadPacketsReceivedText;
    }

    void UpdateBadPacketsReceivedText(int receivedCount)
    {
        BadPacketsReceivedText.text = "Bad Packets Received: " + receivedCount;
    }

    void OnDestroy()
    {
        FWD_Manager.OnBadPacketsReceivedChange -= UpdateBadPacketsReceivedText;
    }
}
