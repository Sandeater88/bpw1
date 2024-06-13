using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    public void LoadMenu()
    {
        // Load scene with build index 0
        SceneManager.LoadScene(0);
    }
}