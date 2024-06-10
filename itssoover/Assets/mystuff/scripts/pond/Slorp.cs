using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slorp : MonoBehaviour
{
    public float range = 1f; // Range within which the player can refill ammo
    public Transform player; // Reference to the player transform
    public Spitonchild spitonchild; // Reference to the Spitonchild script

    void Update()
    {
        // Check if player presses "R" and is within range
        if (Input.GetKeyDown(KeyCode.R) && Vector3.Distance(transform.position, player.position) <= range)
        {
            spitonchild.IncreaseAmmo(spitonchild.maxAmmo / 5); // Increase by 1/5 of max ammo
        }
    }
}
