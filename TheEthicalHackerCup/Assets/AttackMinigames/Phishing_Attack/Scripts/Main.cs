using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject fishPrefab;
    public GameObject spearFishPrefab;
    public GameObject whalePrefab;

    private int count;

    // Start is called before the first frame update
    void Start()
    {
        // We can check game state here to know how many fish to spawn
        count = 3;
        GameObject fish = Instantiate(fishPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        fish.GetComponent<FishMovement>().control = gameObject;

        fish = Instantiate(spearFishPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        fish.GetComponent<FishMovement>().control = gameObject;

        fish = Instantiate(whalePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        fish.GetComponent<FishMovement>().control = gameObject;

    }

    void fishDied()
    {
        count--;
        if (count == 0)
        {
            proceedToAfterActionReport("Phishing WIN :)");
        }
    }

    void outOfTime()
    {
        Debug.Log("game loss");
        proceedToAfterActionReport("Phishing loss :(");
    }

    private void proceedToAfterActionReport(string afterActionReportMessage)
    {
        GameManager.GetInstance().AfterActionReportText = afterActionReportMessage;
    }

}