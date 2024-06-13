using UnityEngine;
using UnityEngine.SceneManagement;

public class Peasantchecker : MonoBehaviour
{
    private bool sceneLoaded = false;  // To prevent multiple loads

    void Update()
    {
        if (!sceneLoaded)
        {
            int deadCount = 0;  // Counter for dead enemies

            // Find all GameObjects with the "peasant" tag
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("peasant");

            // Check each enemy's isDead status
            foreach (GameObject enemyObject in enemies)
            {
                EnemyHealthManager enemyHealth = enemyObject.GetComponent<EnemyHealthManager>();
                if (enemyHealth != null && enemyHealth.isDead)
                {
                    deadCount++;
                }
            }

            // Load scene if 10 or more enemies are dead
            if (deadCount >= 10)
            {
                sceneLoaded = true;
                SceneManager.LoadScene(4);
            }
        }
    }
}
