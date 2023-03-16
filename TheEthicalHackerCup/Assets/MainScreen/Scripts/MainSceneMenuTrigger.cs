using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneMenuTrigger : MonoBehaviour
{
    private bool _playerNearby;
    public GameObject SpinningSprite;
    public GameObject SpeechBubble;
    public bool IsAttackMenu;

    // Start is called before the first frame update
    void Start()
    {
        _playerNearby = false;
        SpeechBubble.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (_playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            string menu = IsAttackMenu ? "AttackMenu" : "DefenseMenu";
            if (!GameManager.GetInstance().GetTutorialSeen(menu) && IsAttackMenu)
            {
                TutorialInit.AttackMenu();
                GameManager.GetInstance().SetTutorialSeen(menu);
                SceneManager.LoadScene("Tutorial");
            }
            else if (!GameManager.GetInstance().GetTutorialSeen(menu) && !IsAttackMenu)
            {
                TutorialInit.DefenseMenu();
                GameManager.GetInstance().SetTutorialSeen(menu);
                SceneManager.LoadScene("Tutorial");
            }
            else
            {
                SceneManager.LoadScene(menu);
            }
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
