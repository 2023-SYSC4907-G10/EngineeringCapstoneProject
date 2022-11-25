using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedBlueGames.Tools.TextTyper;

public class TutorialTextTyper : MonoBehaviour
{
    [SerializeField] private TextTyper tt;
    [SerializeField] private TextAsset textFile;
    private Queue<string> textQueue;
    // Start is called before the first frame update
    void Start()
    {
        textQueue = new Queue<string>();
        initQueue();
        showText();
    }

    void initQueue()
    {
        string[] linesInFile = textFile.text.Split('\n');
        foreach (string line in linesInFile)
        {
            textQueue.Enqueue(line);
        }
    }

    void showText()
    {
        if (textQueue.Count > 0)
        {
            this.tt.TypeText(textQueue.Dequeue());
        }
    }

    // Update is called once per frame
    void Update()
    {
        handleSkips();
    }

    void handleSkips()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (this.tt.IsSkippable() && this.tt.IsTyping)
            {
                this.tt.Skip();
            }
            else
            {
                showText();
            }
        }
    }
}
