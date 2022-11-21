using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PassiveAttackControl : MonoBehaviour
{
    // Start is called before the first frame update

    public NavMeshAgent agent1;
    public NavMeshAgent agent2;

    public GameObject laptop;

    public GameObject dumpster;

    private float timeSinceLastAttack;

    void Start()
    {
        timeSinceLastAttack = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // Every 30 seconds a random passive attack will become available for a random amount of time
        if (Time.time - timeSinceLastAttack > 30) {
            float lengthOfEvent = Random.Range(10f, 20f);

            int randomEvent = Random.Range(1,3);

            switch(randomEvent){
                default:
                case 1:
                    invokeEavesdroppingEvent(new Vector3(0.88f,0.74f,-4.09f), lengthOfEvent);
                    break;

                case 2:
                    invokeDumpsterDivingEvent(lengthOfEvent);
                    break;

                case 3:
                    invokeKeyLoggerEvent(lengthOfEvent);
                    break;
            }

            timeSinceLastAttack = Time.time;
        }
    }

    void invokeEavesdroppingEvent(Vector3 location, float lengthOfEvent) {
        EavesdroppingEvent agent1EavesdroppingEvent = new EavesdroppingEvent(location, lengthOfEvent, true);
        agent1.SendMessage("eavesdroppingEvent", agent1EavesdroppingEvent);

        EavesdroppingEvent agent2EavesdroppingEvent = new EavesdroppingEvent(location, lengthOfEvent, false);
        agent2.SendMessage("eavesdroppingEvent", agent2EavesdroppingEvent);
    }

    void invokeDumpsterDivingEvent(float lengthOfEvent) {
        PassiveAttackEvent dumpsterDivingEvent = new PassiveAttackEvent(lengthOfEvent);
        dumpster.SendMessage("passiveAttackEvent", dumpsterDivingEvent);
    }

    void invokeKeyLoggerEvent(float lengthOfEvent) {
        PassiveAttackEvent keyLoggerEvent = new PassiveAttackEvent(lengthOfEvent);
        laptop.SendMessage("passiveAttackEvent", keyLoggerEvent);
    }
}
