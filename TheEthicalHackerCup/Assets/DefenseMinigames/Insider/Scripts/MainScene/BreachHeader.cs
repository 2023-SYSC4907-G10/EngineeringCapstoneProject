using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class BreachHeader : MonoBehaviour
{
    private string[] documentBreaches;
    private string[] computerBreaches;
    private string[] serverBreaches;

    // Start is called before the first frame update
    void Start()
    {
        initComputerBreaches();
        initDocumentBreaches();
        initServerBreaches();
    }

    private void Update()
    {
        DateTime dt = System.DateTime.Now;
        TextMeshProUGUI tmp = gameObject.GetComponent<TextMeshProUGUI>();
        BreachType? type = InsiderDefenseSingleton.GetInstance().getBreachType();

        int breachIndex = 0;
        if (type == null)
        {
            breachIndex = UnityEngine.Random.Range(0, 3);
        }

        switch (type)
        {
            case BreachType.Computer:
                tmp.text = computerBreaches[breachIndex];
                break;
            case BreachType.Document:
                tmp.text = documentBreaches[breachIndex];
                break;
            case BreachType.Server:
                tmp.text = serverBreaches[breachIndex];
                break;
        }
        tmp.text += " Breached @ " + dt.ToString("HH:mm");
    }

    void initDocumentBreaches()
    {
        documentBreaches = new string[3];
        documentBreaches[0] = "DB Validation Document";
        documentBreaches[1] = "API Vulnerabilities Document";
        documentBreaches[2] = "Kubernetes Password Document";
    }
    void initComputerBreaches()
    {
        computerBreaches = new string[3];
        computerBreaches[0] = "Development Computer";
        computerBreaches[1] = "Testing Computer";
        computerBreaches[2] = "Production Computer";
    }
    void initServerBreaches()
    {
        serverBreaches = new string[3];
        serverBreaches[0] = "Development Server";
        serverBreaches[1] = "Production Server";
        serverBreaches[2] = "Marketing Server";
    }
}
