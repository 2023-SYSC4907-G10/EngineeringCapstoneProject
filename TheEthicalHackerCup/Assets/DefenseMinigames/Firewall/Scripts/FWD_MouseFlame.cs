using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWD_MouseFlame : MonoBehaviour
{

    public List<SpriteRenderer> flames;
    private int _ticksRemaining;
    private float _timeUntilNextTick;
    private static readonly float TICK_PERIOD = 0.125f;

    void Start()
    {
        _timeUntilNextTick = TICK_PERIOD;
        _ticksRemaining = 9;
    }

    void Update()
    {
        _timeUntilNextTick -= Time.deltaTime;
        if (_timeUntilNextTick <= 0)
        {
            _timeUntilNextTick = TICK_PERIOD;
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
