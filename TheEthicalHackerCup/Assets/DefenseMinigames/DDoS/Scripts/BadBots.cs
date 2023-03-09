using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadBots : MonoBehaviour
{
    public GameObject bot;
    public GameObject offline;

    private Rigidbody2D botRigidBody;
    private Collider2D botCollider;

    private void Awake()
    {
        botRigidBody = GetComponent<Rigidbody2D>();
        botCollider = GetComponent<Collider2D>();
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        FindObjectOfType<Manager>().IncreaseScore();

        bot.SetActive(false);
        offline.SetActive(true);
        botCollider.enabled = false;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        offline.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody2D[] slices = offline.GetComponentsInChildren<Rigidbody2D>();

        foreach(Rigidbody2D slice in slices)
        {
            slice.velocity = botRigidBody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }
}
