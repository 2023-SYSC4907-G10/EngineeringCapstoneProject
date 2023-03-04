using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider2D spawnArea;
    public GameObject[] botPrefab;

    public GameObject goodBotPrefab;

    [Range(0f, 1f)]
    public float goodBotChance = 0.10f;
    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 22f;

    public float maxLifetime = 5f;
    private void Awake()
    {
        spawnArea = GetComponent<Collider2D>();
    }

   
    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }
    

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);

        while(enabled)
        {
            GameObject bot = botPrefab[0];

            if(Random.value < goodBotChance){
                bot = goodBotPrefab;
            }

            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = 0f;

            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            GameObject badBot = Instantiate(bot, position, rotation);
            Destroy(badBot, maxLifetime);

            float force = Random.Range(minForce, maxForce);
            badBot.GetComponent<Rigidbody2D>().AddForce(badBot.transform.up * force, ForceMode2D.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
    
}
