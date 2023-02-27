using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateLogs : MonoBehaviour
{
    [SerializeField]
    private GameObject log;
    void Update()
    {
        string logsFile = chooseFile();
        string investigationTool = chooseTool();
        string culprit = chooseCulprit();

        if (!string.IsNullOrEmpty(logsFile) && !string.IsNullOrEmpty(investigationTool) && !string.IsNullOrEmpty(culprit)) {
            destroyChildren();
            // generate logs
            var logs = Resources.Load<TextAsset>("InsiderDefense/" + investigationTool + culprit + logsFile);
            string[] logText = logs.text.Split('\n');
            foreach (string line in logText)
            {
                TextMeshProUGUI Tmp = log.GetComponent<TextMeshProUGUI>();
                Tmp.text = line;
                TextMeshProUGUI createdLog = Instantiate(Tmp, transform);
            }
            InsiderDefenseSingleton.GetInstance().setSelectedTool(null); // reset selected tool to prevent multiple runs of prior foreach loop
        }
    }

    void destroyChildren() {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    string chooseCulprit() {
        SuspectEnum? te = InsiderDefenseSingleton.GetInstance().getCulprit();
        string culprit = "";
        switch(te) {
            case SuspectEnum.Stanley:
                culprit = "Stanley/";
                break;
            case SuspectEnum.Ruth:
                culprit = "Ruth/";
                break;
            case SuspectEnum.Patricia:
                culprit = "Patricia/";
                break;
            case SuspectEnum.Jessica:
                culprit = "Jessica/";
                break;
        }
        return culprit;
    }

    string chooseTool() {
        ToolEnum? te = InsiderDefenseSingleton.GetInstance().getSelectedTool();
        string tool = "";
        switch(te) {
            case ToolEnum.BasicLogs:
                tool = "BasicLogs/";
                break;
            case ToolEnum.BetterLogs:
                tool = "BetterLogs/";
                break;
            case ToolEnum.Eyewitness:
                tool = "Eyewitness/";
                break;
            case ToolEnum.Camera:
                tool = "Camera/";
                break;
        }
        return tool;
    }

    string chooseFile() {
        BreachType? breach = InsiderDefenseSingleton.GetInstance().getBreachType();

        string file = "";
        switch(breach) {
            case BreachType.Document:
                file = "Document/documentLogs";
                break;
            case BreachType.Computer:
                file = "Computer/computerLogs";
                break;
            case BreachType.Server:
                file = "Server/serverLogs";
                break;
        }
        return file;
    }
}
