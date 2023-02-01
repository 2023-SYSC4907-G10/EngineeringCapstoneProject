using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RansomUi : MonoBehaviour
{
    private RansomManager manager;
    private TextMeshProUGUI label;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<RansomManager>();
        label = GetComponent<TextMeshProUGUI>();
        label.text = "Lock the files and avoid the antivirus";
        manager.FileLocked += delegate (object sender, RansomManager.FileLockedEvent ev)
        {
            label.text = $"{ev.FilesLeft} file left";
        };

        manager.GameOver += delegate (object sender, RansomManager.GameOverEvent ev)
        {
            var state = ev.Win ? "won" : "lost";
            label.text = $"You {state}!!!";
        };
    }

}
