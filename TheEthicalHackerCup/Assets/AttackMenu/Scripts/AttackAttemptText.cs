using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackAttemptText : MonoBehaviour
{
    public TextMeshProUGUI txt;
    private string attackAttemptText = "Attacks Attempted";
    [SerializeField] private int attemptsTotal;
    [SerializeField] private int attemptCompleted;

    private Color red;
    private Color green;

    // Start is called before the first frame update
    void Start()
    {
        setupColors();
        string attempts = attackAttemptText + "\n" + attemptCompleted + "/" + attemptsTotal;
        txt.text = attempts;
    }

    // Update is called once per frame
    void Update()
    {
        txt.color = isAttemptComplete() ? green : red;
    }

    private void setupColors() {
        // red 181, 0, 21
        red = new Color(200f/255f, 0, 21f/255f);

        // green 0, 189, 63
        green = new Color(0, 189f/255f, 63f/255f);
    }

    public bool isAttemptComplete() {
        // checks if completed attack attempts equal to total attack attempts
        return attemptCompleted == attemptsTotal;
    }
}
