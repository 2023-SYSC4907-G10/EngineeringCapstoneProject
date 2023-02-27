using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InsiderDefenseSingleton
{
    private BreachType? breachType;
    private SuspectEnum? accusedCulprit;
    private SuspectEnum? culprit;
    private ToolEnum? selectedTool;

    // Static singleton
    private static InsiderDefenseSingleton _instance;

    public static InsiderDefenseSingleton GetInstance()
    {
        if (_instance == null)
        {
            _instance = new InsiderDefenseSingleton();
            _instance.InitializeGameState();
        }
        return _instance;
    }

    private InsiderDefenseSingleton() { } // Private constructor so new instances can't be made

    public void InitializeGameState()
    {
        this.breachType = null;
        this.accusedCulprit = null;
        this.selectedTool = null;
        this.culprit = null;
    }

    public void setBreachType(BreachType? type) {
        this.breachType = type;
    }

    public void setAccusedCulprit(SuspectEnum? se) {
        this.accusedCulprit = se;
    }

    public void setSelectedTool(ToolEnum? te) {
        this.selectedTool = te;
    }

    public void setCuplrit(SuspectEnum? se) {
        this.culprit = se;
    }

    public ToolEnum? getSelectedTool() {
        return this.selectedTool;
    }

    public BreachType? getBreachType() {
        return this.breachType;
    }

    public SuspectEnum? getAccusedCulprit() {
        return this.accusedCulprit;
    }

    public SuspectEnum? getCulprit() {
        return this.culprit;
    }

    public bool isCulpritCorrect() {
        return this.accusedCulprit == this.culprit;
    }
}

