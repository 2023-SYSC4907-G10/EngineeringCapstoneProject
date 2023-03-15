using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackOut : MonoBehaviour
{
    [SerializeField] private Button closeMenuButton;
    // Start is called before the first frame update
    void Start()
    {
        closeMenuButton.onClick.AddListener(closeMenu);
    }

    void closeMenu()
    {
        SceneManager.LoadScene("MainScene");
    }
}
