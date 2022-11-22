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
        garbage.material = available;
    }

    public override void changeToIdleDisplay()
    {
        garbage.material = idle;
    }

    public override void onSuccess()
    {
        Debug.Log("Succesful Dumpster Diving Attack");
    }
}
