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
            GameManager.GetInstance().ChangeRespect(evt.Win ? 10 : -10);
            GameManager.GetInstance().SwitchToAfterActionReportScene(
                "You " +
                (evt.Win ?
                    "Successfully encrypted all their files!\n +10 Respect" :
                    "FAILED! You got caught and removed from the system\n -10 Respect"
                ));
            // idk put a call to the game manager here or something idc
        };
        FileLocked += delegate (object sender, FileLockedEvent evt)
        {
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
