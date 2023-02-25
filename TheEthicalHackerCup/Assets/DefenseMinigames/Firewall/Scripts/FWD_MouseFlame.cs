using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWD_MouseFlame : MonoBehaviour
{

    public List<SpriteRenderer> flames;
    private int _ticksRemaining;

    // Start is called before the first frame update
    void Start()
    {
        _ticksRemaining = 9;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 30 == 0)
        {
            foreach (SpriteRenderer sr in flames)
            {
                sr.flipX = !sr.flipX;
            }
            transform.Rotate(0, 0, 10);
            transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
            _ticksRemaining--;

            if (_ticksRemaining <= 0)
            {
                // Self deletion of the flame
                Destroy(gameObject);
            }
        }

    }
}
