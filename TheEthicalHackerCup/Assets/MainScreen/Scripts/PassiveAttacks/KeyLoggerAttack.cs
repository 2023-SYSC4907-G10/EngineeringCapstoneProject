using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLoggerAttack : PassiveAttack
{
    public Renderer laptop;
    public Material idle;
    public Material available;
    public Material active;

    public override void changeToAttackingDisplay()
    {
        laptop.material = active;
    }

    public override void changeToAvailableDisplay()
    {
        ExclamationMarkIndicator.SetActive(true);
        laptop.material = available;
    }

    public override void changeToIdleDisplay()
    {
        ExclamationMarkIndicator.SetActive(false);
        laptop.material = idle;
    }

    public override void onSuccess()
    {
        GameManager.GetInstance().ChangeOpponentKnowledge(SUCCESS_OPP_KNOWLEDGE_INCREASE);
        Debug.Log("Succesful KeyLogging Attack");
        Debug.Log("Opp knowledge: " + GameManager.GetInstance().GetOpponentKnowledge());
    }

}
