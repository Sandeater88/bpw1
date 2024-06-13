using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorialscene : MonoBehaviour
{
    void Update()
    {
        // Reload the current scene when pressing "1"
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ReloadCurrentScene();
        }

        // Load scene number 1 when pressing "2"
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadSceneOne();
        }
    }

    void ReloadCurrentScene()
    {
        // Get the current active scene and reload it
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    void LoadSceneOne()
    {
        // Load scene with build index 1
        SceneManager.LoadScene(1);
    }
}
