using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graven : MonoBehaviour
{
    
    public GameObject hole;    // prefab van de val moet in de inspector worden gesleept
    public float gravenCooldown = 10f; //variabele voor cooldowntijd
    public float lastKuil; 
    public bool canDig = true; 

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.G) && canDig)   //checkt constant of G wordt ingedrukt en canDig=true 
        {
            SpawnTrap(); //roept functie aan om val te laten verschijnen
            lastKuil = Time.time; //game geeft de verstreken gametijd zodra G wordt ingedrukt en koppelt het aan lastKuil
        }

        
        if (Time.time - lastKuil < gravenCooldown) //als de verstreken tijd min de tijdwaarde van lastKuil kleiner is dan 10 sec
        {

            canDig = false; // dan gebeurt er niks als je G indrukt

        }
        else
        {
            canDig = true;  //is die nieuwe waarde hoger dan de cooldown, dan verschijnt er weer een val als G wordt ingedrukt
        }
    }

    public void SpawnTrap()
    {
        //val verschijnt op plek van de speler, maar achter de speler zodat de kuil niet over spelersprite staat
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 1); 
        Instantiate(hole, spawnPosition, Quaternion.identity); //maakt nieuwe val-gameobject aan

    }
}
