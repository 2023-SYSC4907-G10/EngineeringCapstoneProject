using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{

    [SerializeField] private Renderer rend;
    [SerializeField] private Material notDetected;
    [SerializeField] private Material detected;
    [SerializeField] private GameObject GetOutOfHereSpeechBubble;
    private CapsuleCollider coll;

    private bool _isSpeechBubbleActive;
    private float _remainingSpeechBubbleShowTime;
    private static readonly float SPEECH_BUBBLE_SHOW_TIME = 1f;

    void Start()
    {
        GetOutOfHereSpeechBubble.SetActive(false);
        _isSpeechBubbleActive = false;
        coll = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("PlayerLayer"))
        {
            rend.material = detected;
            GetOutOfHereSpeechBubble.SetActive(true);
            _isSpeechBubbleActive = true;
            coll.SendMessage("playerDetected");
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("PlayerLayer"))
        {
            rend.material = notDetected;
        }
    }

    void Update()
    {
        if (_isSpeechBubbleActive)
        {
            if (_remainingSpeechBubbleShowTime <= 0)
            {
                // Hiding the speech bubble
                _remainingSpeechBubbleShowTime = SPEECH_BUBBLE_SHOW_TIME;
                _isSpeechBubbleActive = false;
                GetOutOfHereSpeechBubble.SetActive(false);

            }
            else { _remainingSpeechBubbleShowTime -= Time.deltaTime; }
        }
    }
    public void eavesdroppingStarted() {
        rend.enabled = false;
        coll.enabled = false;
    }

    public void eavesdroppingStopped() {
        rend.enabled = true;
        coll.enabled = true;
    }
}
