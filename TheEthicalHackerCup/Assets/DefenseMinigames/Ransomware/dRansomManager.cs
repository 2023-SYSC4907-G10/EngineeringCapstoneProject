using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.WebSockets;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class dRansomManager : MonoBehaviour
{
    private string heuristics;
    private IList<MetaData> metaData;
    private int lives;
    private int dataIndex;
    private float timer;
    private bool timerOn;
    private MetaData current { get { return metaData[dataIndex]; } }

    [SerializeField]
    private TextMeshProUGUI fileText;

    [SerializeField]
    private TextMeshProUGUI livesText;

    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private TextMeshProUGUI sandboxStatusText;


    [SerializeField]
    private TextMeshProUGUI heuristicText;
    [SerializeField]
    private GameObject denyButton;
    [SerializeField]
    private GameObject acceptButton;
    [SerializeField]
    private GameObject sandboxButton;
    

    void Start()
    {

        //read and process files
        var fileContent = Resources.Load<TextAsset>(FileHelperSingleton.Singleton.currentFileName).ToString();
        XDocument doc = XDocument.Parse(fileContent);
        metaData = MetaData.ListFromXml(doc.Root.Element("MetaDataCollection"));
        heuristics = doc.Root.Element("Heuristic").Value;



        denyButton.GetComponent<Button>().onClick.AddListener(() => handleDenyClicked());
        acceptButton.GetComponent<Button>().onClick.AddListener(() => handleAcceptClicked());
        sandboxButton.GetComponent<Button>().onClick.AddListener(() => handleSandboxClicked());

        lives = 4;
        dataIndex = 0;
        timer = 0;
        timerOn = false;

        updateHeuristics(heuristics);
        updateAll();

    }

    private void updateHeuristics(string heuristics)
    {
        heuristicText.text = heuristics;
    }

    private void updateMetaData() 
    {
        fileText.text = current.ToString();
    }

    private void updateLives() {
        this.livesText.text = "Lives: " + lives;
    }

    private void updateSandboxStatus() 
    {
        if(!this.timerOn) 
        {
            this.sandboxStatusText.text = "";
            return;
        }
        sandboxStatusText.text = this.current.isVirus && this.current.TimeTillInfection <= timer?"Virus Detected":"Program Sandboxed";
    }

    private void updateTimer() {
        this.timerText.text = timerOn ? "Time program is in sandbox: "+timer : "";
    }

    private void Update()
    {
        if (this.timerOn) 
        {
            timer += Time.deltaTime;
            updateTimer();
            updateSandboxStatus();
        }
    }

    private void handleAcceptClicked() 
    {
        if (current.isVirus)
        {
            lives-=2;
            updateLives();
            checkForDeath();
        }
        nextData();
    }
    private void handleDenyClicked() 
    {
        if (!current.isVirus)
        {
            lives--;
            updateLives();
            checkForDeath();
        }
        nextData();
    }
    private void handleSandboxClicked() 
    {
        if (timerOn) 
        {
            updateTimer();
        }
        timerOn = !timerOn;
        updateSandboxStatus();
    }

    private void updateAll() 
    {
        
        updateMetaData();
        updateLives();
        updateTimer();
        updateSandboxStatus();
    }

    private void checkForDeath() 
    {
        if (lives <= 0) 
        {
            GameManager.GetInstance().ChangeRespect(-10);
            GameManager.GetInstance().SwitchToAfterActionReportScene("All your lives were lost. You either let ransomers destroy the all the production data (including backups), thought too many things were viruses or a combination of both.\n -10 Respect");
            return;
        }
    }

    private void nextData() 
    {
        if (dataIndex+1 == metaData.Count) 
        {
            GameManager.GetInstance().ChangeRespect(10);
            GameManager.GetInstance().SwitchToAfterActionReportScene("You successfully scanned the incoming software and kept the production data safe. Good job!\n +10 Respect");
            FileHelperSingleton.Singleton.nextFile();
            return;
        }
        dataIndex++;
        timer = 0;
        timerOn = false;
        updateAll();
    }

}

class FileHelperSingleton
{
    private string[] fileNames = { "RansomDefense/level1", "RansomDefense/level2", "RansomDefense/level3" };
    private int index = 0;
    public string currentFileName { get { return fileNames[index]; } }

    public void nextFile() 
    {
        if (fileNames.Length == index + 1) 
        {
            return;
        }
        index++;
    }

    private static FileHelperSingleton instance;
    public static FileHelperSingleton Singleton { get { return instance == null ? (instance = new FileHelperSingleton()) : instance; } }
}
