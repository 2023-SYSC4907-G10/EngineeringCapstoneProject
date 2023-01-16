// using Codice.CM.WorkspaceServer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RansomSingleton
{
    private static RansomSingleton instance;

    public static RansomSingleton GetInstance()
    {
        if (instance == null)
        {
            instance = new RansomSingleton();
        }
        return instance;
    }

    private int fileCount;
    public int FileCount { get { return fileCount; } 
        set {
            fileCount = value;
            if (fileCount == 0)
            {
                end(true);
            }
            else {
                FileLocked.Invoke(this, new FileLockedEvent(fileCount));
            }
        } }

    public void lose() {
        end(false);
    }
    private void end(bool win) {
        GameOver.Invoke(this, new GameOverEvent(win));
        Time.timeScale = 0;
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

    public event EventHandler<GameOverEvent> GameOver;
    public event EventHandler<FileLockedEvent> FileLocked;
}
