using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpsterDivingAttack : PassiveAttack
{

    public Renderer garbage;
    public Material idle;
    public Material available;
    public Material active;

    public override void changeToAttackingDisplay()
    {
        garbage.material = active;
    }

    public override void changeToAvailableDisplay()
    {
        ExclamationMarkIndicator.SetActive(true);
        garbage.material = available;
    }

    public override void changeToIdleDisplay()
    {
        ExclamationMarkIndicator.SetActive(false);
        garbage.material = idle;
    }

    public override void onSuccess()
    {
        GameManager.GetInstance().ChangeOpponentKnowledge(SUCCESS_OPP_KNOWLEDGE_INCREASE);
        Debug.Log("Succesful Dumpster Diving Attack");
        Debug.Log("Opp knowledge: " + GameManager.GetInstance().GetOpponentKnowledge());
    }
}
