using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FWD_GoodPacketsBurnedText : MonoBehaviour
{
    public TextMeshProUGUI GoodPacketsBurnedText;

    // Start is called before the first frame update
    void Start()
    {
        FWD_Manager.OnBurnedGoodPacketsChange += UpdateBurnedPacketsText;

    }
    void UpdateBurnedPacketsText(int burnedCount)
    {
        GoodPacketsBurnedText.text = "Good Packets Burned: " + burnedCount;
    }

    void OnDestroy()
    {
        FWD_Manager.OnBurnedGoodPacketsChange -= UpdateBurnedPacketsText;
    }
}
