using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy the bullet after a certain time
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the bullet when it collides with any object (except the player)
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
