using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField]
    private GameObject[] levels;
    // Start is called before the first frame update
    void Start()
    {
        InsiderSingleton.GetInstance().InitializeGameState();
        InsiderSingleton.GetInstance().PickLevel();
        string levelName = InsiderSingleton.GetInstance().GetCurrentLevelName();
        GameObject level = null;
        foreach (GameObject g in levels) {
            if (g.name.Equals(levelName)) {
                level = g;
            }
        }
        Instantiate(level, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
