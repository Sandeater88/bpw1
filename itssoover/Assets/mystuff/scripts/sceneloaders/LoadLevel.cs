using UnityEngine;
using UnityEngine.SceneManagement;

public class Loadlevel : MonoBehaviour
{
    public void LoadSceneOne()
    {
        // Load scene with build index 1
        SceneManager.LoadScene("level");
    }
}
