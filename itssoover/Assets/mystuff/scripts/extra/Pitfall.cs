using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitfall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) //als een ander gameobject tegen dit gameobject aanbotst dan
    {
        
        if (other.CompareTag("peasant")) //kijkt of het andere gameobject waar een collision mee is de juiste tag heeft
        {
            EnemyScript enemyScript = other.GetComponent<EnemyScript>(); //refereert naar enemyscript wat zit op het gameobject met peasant tag
            if (enemyScript != null)
            {
                //als de collision inderdaad met de enemy is, dan wordt de "trapped" state aangeroepen uit het enemyscript
                enemyScript.SetTrappedState();  
            }
        }
    }
}
