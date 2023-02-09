using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private float timeToCollect = 3f;
    [SerializeField]
    private Sprite collectedSprite;

    private float collectedPercent = 0f;
    private bool isBeingCollected = false;
    private bool isCollected = false;
    private RansomManager manager;
    private SpriteRenderer spriteRenderer;
    

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<RansomManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        manager.FileCount++;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        var other = collision.gameObject;
        isBeingCollected = other.gameObject.CompareTag("Player");
        if (isBeingCollected && !isCollected)
        {
            collectedPercent += Time.deltaTime / timeToCollect;
            if (collectedPercent >= 1)
            {
                lockFile();
            }
        }
    }

    private void lockFile()
    {
        collectedPercent = 1;
        isCollected = true;
        spriteRenderer.sprite = collectedSprite;
        manager.FileCount--;
        
    }
}
