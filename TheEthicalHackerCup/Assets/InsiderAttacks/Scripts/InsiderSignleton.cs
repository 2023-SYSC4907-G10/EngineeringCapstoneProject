using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InsiderSingleton
{
    private int totalDocuments;
    private int collectedDocuments;
    private int totalServers;
    private int collectedServers;
    private int totalComputers;
    private int collectedComputers;
    private double suspicionLevel;
    
    private readonly String[] VISIBLE_LEVEL_NAMES = new String[] { "GrassLevel_1", "RockLevel_1" };
    private String currentLevelName;
    // Static singleton
    private static InsiderSingleton _instance;

    public static InsiderSingleton GetInstance()
    {
        if (_instance == null)
        {
            _instance = new InsiderSingleton();
            _instance.InitializeGameState();
        }
        return _instance;
    }

    private InsiderSingleton() { } // Private constructor so new instances can't be made

    public void InitializeGameState()
    {
        this.totalComputers = 0;
        this.totalDocuments = 0;
        this.totalServers = 0;
        this.collectedComputers = 0;
        this.collectedDocuments = 0;
        this.collectedServers = 0;
        this.suspicionLevel = 0;
        this.currentLevelName = "";
    }

    // Getters
    public int GetCollectedDocuments()
    {
        return this.collectedDocuments;
    }

    public int GetCollectedComputers()
    {
        return this.collectedComputers;
    }

    public int GetCollectedServers()
    {
        return this.collectedServers;
    }

    public int GetTotaldDocuments()
    {
        return this.totalDocuments;
    }

    public int GetTotalComputers()
    {
        return this.totalComputers;
    }

    public int GetTotalServers()
    {
        return this.totalServers;
    }

    public double GetSuspicionLevel()
    {
        return Math.Truncate(this.suspicionLevel * 100) / 100;;
    }

    public String GetCurrentLevelName()
    {
        return this.currentLevelName;
    }

    // Primitive Setters
    private void SetCollectedDocuments(int collectedDocument)
    {
        this.collectedDocuments = collectedDocument;
    }

    private void SetCollectedComputers(int collectedComputer)
    {
       this.collectedComputers = collectedComputer;
    }

    private void SetCollectedServers(int collectedServer)
    {
        this.collectedServers = collectedServer;
    }

    public void SetTotalDocuments(int totalDocs)
    {
        this.totalDocuments = totalDocs;
    }

    public void SetTotalComputers(int totalComputers)
    {
       this.totalComputers = totalComputers;
    }

    public void SetTotalServers(int totalServers)
    {
        this.totalServers = totalServers;
    }

    private void SetSuspicionLevel(double suspicionLevel)
    {
        this.suspicionLevel = suspicionLevel;
    }

    // Changers
    public void ChangeCollectedDocuments(int change) { this.SetCollectedDocuments(this.collectedDocuments + change); }
    public void ChangeCollectedComputers(int change) { this.SetCollectedComputers(this.collectedComputers + change); }
    public void ChangeCollectedServers(int change) { this.SetCollectedServers(this.collectedServers + change); }
    public void ChangeSuspicionLevel(double change) { this.SetSuspicionLevel(this.suspicionLevel + change); }

    public void PickLevel() {
        int levelIndex = UnityEngine.Random.Range(0, VISIBLE_LEVEL_NAMES.Length);
        this.currentLevelName = VISIBLE_LEVEL_NAMES[levelIndex];
    }
}

