using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationMarkIndicator : MonoBehaviour
{
    private static readonly float  WIGGLE_PERIOD = 0.2f;
    private float _currentTimeUntilWiggle;
    private int _turnDirectionMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        _currentTimeUntilWiggle = WIGGLE_PERIOD;
        _turnDirectionMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTimeUntilWiggle -= Time.deltaTime;

        if (_currentTimeUntilWiggle <= 0)
        {
            // Wiggles the exclamation point back and forth
            _currentTimeUntilWiggle = WIGGLE_PERIOD;
            _turnDirectionMultiplier = -_turnDirectionMultiplier;
            transform.Rotate(new Vector3(0, 0, 30 * _turnDirectionMultiplier));
        }
    }
}
