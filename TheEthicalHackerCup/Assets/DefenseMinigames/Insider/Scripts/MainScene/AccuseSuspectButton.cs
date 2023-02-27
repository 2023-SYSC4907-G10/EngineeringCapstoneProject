using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AccuseSuspectButton : MonoBehaviour
{
    [SerializeField] SuspectEnum se;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(AccuseClicked);
    }

    void AccuseClicked()
    {
        InsiderDefenseSingleton.GetInstance().setAccusedCulprit(se);
        SceneManager.LoadScene("InsiderDefenseEnd");
    }
}