using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoStation : MonoBehaviour
{
    private bool _playerNearby;
    public GameObject SpinningSprite;
    public GameObject SpeechBubble;
    void Start()
    {
        SpeechBubble.SetActive(false);
        SpinningSprite.SetActive(true);
        if (!GameManager.GetInstance().GetTutorialSeen("main"))
        { 
            TutorialInit.MainScene();
            GameManager.GetInstance().SetTutorialSeen("main");
            SceneManager.LoadScene("Tutorial");
        }
    }
    void Update()
    {
        if (_playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            TutorialInit.MainScene();
            SceneManager.LoadScene("Tutorial");
        }
        SpinningSprite.transform.Rotate(new Vector3(0, 0.5f, 0));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerLayer"))
        {
            _playerNearby = true;
            SpeechBubble.SetActive(true);
            SpinningSprite.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerLayer"))
        {
            _playerNearby = false;
            SpeechBubble.SetActive(false);
            SpinningSprite.SetActive(true);
        }
    }
}
