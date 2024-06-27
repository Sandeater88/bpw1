using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //3 states waarin de enemy kan zijn
    public enum EnemyState
    {
        Roaming,
        Attacking,
        Trapped
    }

    public string lamaTag = "Player";
    public float Speed = 2f; // Speed of enemy movement
    public float attackRadius = 2f;
    public float hoehardShakey = 0.1f; 
    public float hoelangShakey = 0.2f; 
    public float zitVast = 3f; 

    public EnemyState CurrentState { get; private set; }

    private Transform lamaTrans;
    private Vector2 roamStart; 
    private int currentWalkingCoord; 
    private Vector2[] walkingCoord; 
    private Vector3 trappedStart; 
    private Coroutine trapCoroutine; 

    private void Start()
    {
        CurrentState = EnemyState.Roaming;               //enemy begint in roaming state
        lamaTrans = GameObject.FindGameObjectWithTag(lamaTag).transform;  //zoekt de player                                 
        roamStart = transform.position;     //geeft huidige positie van enemy             
        Initializeroaming();               //start de Roaming state
        currentWalkingCoord = 0;             
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case EnemyState.Roaming:
                Roam();
                CheckForAttack();
                break;
            case EnemyState.Attacking:
                MoveTowardsPlayer();
                break;
            case EnemyState.Trapped:

                break;
        }
    }

    private void Initializeroaming()
    {
        // Define patrol points for roaming in a 4x4 square relative to the current position
        walkingCoord = new Vector2[4];

        // Directly assign each patrol point to the array
        walkingCoord[0] = new Vector2(roamStart.x + 2, roamStart.y); // Move right
        walkingCoord[1] = new Vector2(roamStart.x + 2, roamStart.y + 2); // Move up
        walkingCoord[2] = new Vector2(roamStart.x, roamStart.y + 2); // Move left
        walkingCoord[3] = new Vector2(roamStart.x, roamStart.y); // Move down
    }

    private void Roam()
    {
        // Move towards the next patrol point
        transform.position = Vector2.MoveTowards(transform.position, walkingCoord[currentWalkingCoord], Speed * Time.deltaTime);

        // Check if the enemy reached the current patrol point
        if (Vector2.Distance(transform.position, walkingCoord[currentWalkingCoord]) < 0.1f)
        {
            // Move to the next patrol point
            currentWalkingCoord = (currentWalkingCoord + 1) % walkingCoord.Length;
        }
    }

    private void CheckForAttack()
    {
        // Check if player is within attack range
        if (Vector2.Distance(transform.position, lamaTrans.position) <= attackRadius)
        {
            CurrentState = EnemyState.Attacking;
        }
    }

    private void MoveTowardsPlayer()
    {
        // Move towards player
        transform.position = Vector2.MoveTowards(transform.position, lamaTrans.position, Speed * Time.deltaTime);

        // Check if player is out of attack range
        if (Vector2.Distance(transform.position, lamaTrans.position) > attackRadius)
        {
            CurrentState = EnemyState.Roaming;
            roamStart = transform.position; // Update roaming origin to current position
            Initializeroaming(); // Re-initialize patrol points based on new origin
        }
    }

    public void SetTrappedState()           //moet public zijn, omdat de pitfall script het refereert
    {
        if (CurrentState != EnemyState.Trapped)
        {
            CurrentState = EnemyState.Trapped;
            trappedStart = transform.position;
            if (trapCoroutine != null)
            {
                StopCoroutine(trapCoroutine);
            }
            trapCoroutine = StartCoroutine(HandleTrapping());
        }
    }

    private IEnumerator HandleTrapping()
    {
        float elapsedTime = 0f;
        while (elapsedTime < hoelangShakey)
        {
            float x = Random.Range(-1f, 1f) * hoehardShakey;
            float y = Random.Range(-1f, 1f) * hoehardShakey;
            transform.position = trappedStart + new Vector3(x, y, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = trappedStart;
        yield return new WaitForSeconds(zitVast - hoelangShakey);

        FreeFromTrap();
    }

    // Method to free the enemy from trapped state
    private void FreeFromTrap()
    {
        CurrentState = EnemyState.Roaming;
        roamStart = transform.position; // Update roaming origin to current position
        Initializeroaming(); // Re-initialize patrol points based on new origin
    }
}
