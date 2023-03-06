using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;

using UnityEngine;


public class NewBehaviourScript : MonoBehaviour
{
    private IList<string> heuristics;
    private IList<IncomingFile> incomingFiles;
    private int lives;
    private int fileIndex;
    void Start()
    {
        //read and process files
        //show results on the screen
        incomingFiles = new List<IncomingFile>();
        var bruh = new IncomingFile();
        bruh.Name = "";

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
}
