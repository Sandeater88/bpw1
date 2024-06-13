using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public void LoadSceneTwo()
    {
        // Load scene with build index 2
        SceneManager.LoadScene("tutorial");
    }
}
