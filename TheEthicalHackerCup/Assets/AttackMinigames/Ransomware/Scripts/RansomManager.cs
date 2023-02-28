using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RansomManager : MonoBehaviour
{

    public event EventHandler<GameOverEvent> GameOver;
    public event EventHandler<FileLockedEvent> FileLocked;

    // Start is called before the first frame update
    void Start()
    {
        GameOver += delegate (object sender, GameOverEvent evt) 
        {
            //Debug.Log("Game Over");

            GameManager.GetInstance().SwitchToAfterActionReportScene("You " + (evt.Win ? "encrypted all their files" : "got caught and removed from the system"));
            // idk put a call to the game manager here or something idc
        };
        FileLocked += delegate (object sender, FileLockedEvent evt)
        {
            //Debug.Log("File Locked");
        };
    }

    private int fileCount;
    public int FileCount
    {
        get { return fileCount; }
        set
        {

            if (value == 0)
            {
                fileCount = value;
                end(true);
            }
            else if (fileCount > value)
            {
                fileCount = value;
                FileLocked.Invoke(this, new FileLockedEvent(fileCount));
            }
            else 
            {
                fileCount = value;
            }
            
        }
    }

    public void lose()
    {
        end(false);
    }
    private void end(bool win)
    {
        GameOver.Invoke(this, new GameOverEvent(win));
    }


    public class GameOverEvent
    {
        public bool Win { get; set; }
        public GameOverEvent(bool win)
        {
            Win = win;
        }
    }

    public class FileLockedEvent
    {
        public int FilesLeft { get; set; }
        public FileLockedEvent(int left)
        {
            this.FilesLeft = left;
        }
    }


}
