using UnityEngine;
using UnityEngine.SceneManagement;

public class PeasantChecker : MonoBehaviour
{
    void Update()
    {
        // Check if there are no more GameObjects with the "peasant" tag
        if (GameObject.FindGameObjectsWithTag("peasant").Length == 0)
        {
            // Load scene with build index 4
            SceneManager.LoadScene(4);
        }
    }
}
