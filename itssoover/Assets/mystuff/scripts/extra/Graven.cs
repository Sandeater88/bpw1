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

        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, 1); //val verschijnt op plek van de speler, maar achter de speler
        Instantiate(hole, spawnPosition, Quaternion.identity);

    }
}
