using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FWD_Manager
{
    // <Singleton Operations>
    // Static singleton
    private static FWD_Manager _instance;
    private FWD_Manager() { } // Private constructor so new instances can't be made

    public static FWD_Manager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new FWD_Manager();
            _instance.InitializeGameState();
        }
        return _instance;
    }
    // </Singleton Operations>

    // private FWD_WaypointManager _waypointManager;
    public FWD_WaypointManager WaypointManager {get;set;}
    public void InitializeGameState()
    {
        WaypointManager = null;
     }
}