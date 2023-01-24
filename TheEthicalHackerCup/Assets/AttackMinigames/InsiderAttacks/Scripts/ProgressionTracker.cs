using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionTracker : MonoBehaviour
{
    [SerializeField]
    private TextAsset data;
    private string[] linesInFile;

    [SerializeField]
    private GameObject level;
    private bool visibility;
    // Start is called before the first frame update
    void Start()
    {
        this.linesInFile = data.text.Split('\n');
        foreach (string line in linesInFile)
        {
            string[] split = line.Split('=');
            string collectibleType = split[0];
            int collectibleValue = int.Parse(split[1]);
            
            switch(collectibleType) {
                case "document":
                    InsiderSingleton.GetInstance().SetTotalDocuments(collectibleValue);
                    break;
                case "computer":
                    InsiderSingleton.GetInstance().SetTotalComputers(collectibleValue);
                    break;
                case "server":
                    InsiderSingleton.GetInstance().SetTotalServers(collectibleValue);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
