using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public enum EnemyState
    {
        Roaming,
        Attacking
    }

    public string playerTag = "Player"; // Tag of the player object
    public float attackRange = 2f; // Range within which enemy attacks
    public float moveSpeed = 2f; // Speed of enemy movement

    private EnemyState currentState;
    private Transform playerTransform;
    private Transform enemyTransform;
    private Vector2[] patrolPoints; // Array to store patrol points for roaming
    private int currentPatrolIndex; // Index of the current patrol point

    private void Start()
    {
        currentState = EnemyState.Roaming;
        playerTransform = GameObject.FindGameObjectWithTag(playerTag).transform;
        enemyTransform = transform;

        // Define patrol points for roaming in a 4x4 square
        patrolPoints = new Vector2[]
        {
            enemyTransform.position + new Vector3(2, 0, 0), // Move right
            enemyTransform.position + new Vector3(2, 2, 0), // Move up
            enemyTransform.position + new Vector3(0, 2, 0), // Move left
            enemyTransform.position + new Vector3(0, 0, 0)  // Move down
        };

        currentPatrolIndex = 0;
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Roaming:
                Roam();
                CheckForAttack();
                break;
            case EnemyState.Attacking:
                MoveTowardsPlayer();
                break;
        }
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
            currentState = EnemyState.Attacking;
        }
    }

    private void MoveTowardsPlayer()
    {
        // Move towards player
        enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, playerTransform.position, moveSpeed * Time.deltaTime);

        // Check if player is out of attack range
        if (Vector2.Distance(enemyTransform.position, playerTransform.position) > attackRange)
        {
            currentState = EnemyState.Roaming;
        }
    }
}
