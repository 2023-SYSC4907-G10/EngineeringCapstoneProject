using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspicionTracker : MonoBehaviour
{
    [SerializeField]
    private Sprite[] suspicionLevels;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        double susLevel = InsiderSingleton.GetInstance().GetSuspicionLevel();
        if (susLevel > 80) {
            sr.sprite = suspicionLevels[2];
        } else if (susLevel > 40) {
            sr.sprite = suspicionLevels[1];
        } else {
            sr.sprite = suspicionLevels[0];
        }
    }
}
