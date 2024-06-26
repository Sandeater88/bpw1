using UnityEngine;

public class Graven : MonoBehaviour
{
    public GameObject hole; // prefab van de val moet in de inspector worden gesleept

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))  //wanneer G wordt ingedrukt, dan verschijnt de val
        {
            SpawnTrap();
        }
    }

    private void SpawnTrap()
    {
        if (hole != null)
        {
            Instantiate(hole, transform.position, Quaternion.identity); //val verschijnt op de plek van de speler
        }
        else
        {
            Debug.LogError("Trap prefab is not assigned in the Inspector.");
        }
    }
}
