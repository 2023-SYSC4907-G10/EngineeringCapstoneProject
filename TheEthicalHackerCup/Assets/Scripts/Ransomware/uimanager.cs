using Codice.CM.Client.Differences.Graphic;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using static RansomSingleton;

public class uimanager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        var single = RansomSingleton.GetInstance();
        setLeft(single.FileCount);
        single.FileLocked += delegate (object sender, FileLockedEvent ev) { setLeft(ev.FilesLeft); };
        single.GameOver += delegate (object sender, GameOverEvent ev) { setEnd(ev.Win); };
    }

    private void setEnd(bool win) 
    {
        var label = GetComponent<TextMeshProUGUI>();
        label.text = string.Format("You {0}!", win ? "won" : "lost");
    }

    private void setLeft(int left) 
    {
        var label = GetComponent<TextMeshProUGUI>();
        label.text = string.Format("{0} files left", left);

    }
}
