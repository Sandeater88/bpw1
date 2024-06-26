using UnityEngine;
using System.Collections;

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
    public float attackRange = 2f; 
    public float moveSpeed = 2f; // Speed of enemy movement
    public float trapShakeDuration = 0.2f; // Duration of shaking when trapped
    public float trapShakeMagnitude = 0.1f; // Magnitude of shaking when trapped
    public float trapDuration = 3f; // Duration of being trapped

    // Public property with private setter
    public EnemyState CurrentState { get; private set; }

    private Transform playerTransform;
    private Transform enemyTransform;
    private Vector2[] patrolPoints; // Array to store patrol points for roaming
    private int currentPatrolIndex; // Index of the current patrol point
    private Vector2 roamingOrigin; // Origin point for roaming
    private Vector3 trappedOriginalPosition; // Original position when trapped
    private Coroutine trapCoroutine; // Coroutine for trapping logic

    private void Start()
    {
        CurrentState = EnemyState.Roaming;
        playerTransform = GameObject.FindGameObjectWithTag(lamaTag).transform;
        enemyTransform = transform;
        roamingOrigin = enemyTransform.position;
        InitializePatrolPoints();
        currentPatrolIndex = 0;
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
                // Do nothing when trapped
                break;
        }
    }

    private void InitializePatrolPoints()
    {
        // Define patrol points for roaming in a 4x4 square relative to the current position
        patrolPoints = new Vector2[]
        {
            roamingOrigin + new Vector2(2, 0), // Move right
            roamingOrigin + new Vector2(2, 2), // Move up
            roamingOrigin + new Vector2(0, 2), // Move left
            roamingOrigin + new Vector2(0, 0)  // Move down
        };
    }

    private void Roam()
    {
        // Move towards the next patrol point
        enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, patrolPoints[currentPatrolIndex], moveSpeed * Time.deltaTime);

        // Check if the enemy reached the current patrol point
        if (Vector2.Distance(enemyTransform.position, patrolPoints[currentPatrolIndex]) < 0.1f)
        {
            // Move to the next patrol point
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    private void CheckForAttack()
    {
        // Check if player is within attack range
        if (Vector2.Distance(enemyTransform.position, playerTransform.position) <= attackRange)
        {
            CurrentState = EnemyState.Attacking;
        }
    }

    private void MoveTowardsPlayer()
    {
        // Move towards player
        enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, playerTransform.position, moveSpeed * Time.deltaTime);

        // Check if player is out of attack range
        if (Vector2.Distance(enemyTransform.position, playerTransform.position) > attackRange)
        {
            CurrentState = EnemyState.Roaming;
            roamingOrigin = enemyTransform.position; // Update roaming origin to current position
            InitializePatrolPoints(); // Re-initialize patrol points based on new origin
        }
    }

    // Method to set the enemy state to trapped
    public void SetTrappedState()
    {
        if (CurrentState != EnemyState.Trapped)
        {
            CurrentState = EnemyState.Trapped;
            trappedOriginalPosition = enemyTransform.position;
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
        while (elapsedTime < trapShakeDuration)
        {
            float x = Random.Range(-1f, 1f) * trapShakeMagnitude;
            float y = Random.Range(-1f, 1f) * trapShakeMagnitude;
            enemyTransform.position = trappedOriginalPosition + new Vector3(x, y, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        enemyTransform.position = trappedOriginalPosition;
        yield return new WaitForSeconds(trapDuration - trapShakeDuration);

        FreeFromTrap();
    }

    // Method to free the enemy from trapped state
    private void FreeFromTrap()
    {
        CurrentState = EnemyState.Roaming;
        roamingOrigin = enemyTransform.position; // Update roaming origin to current position
        InitializePatrolPoints(); // Re-initialize patrol points based on new origin
    }
}
