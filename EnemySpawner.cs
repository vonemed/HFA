using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For UI elements
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

    [Header("Prefabs for enemies")]
    // Prefabs for enemies
    public GameObject tankPrefab;
    public GameObject truckPrefab;

    // A number of enemies remaining
    [SerializeField]
    private int enemyCount;

    [Header("UI")]
    public RawImage secondStar;

    // Use this for initialization
    void Start ()
    {
        // Starting spawning routine
        StartCoroutine(SpawnEnemies());
	}

    public void Update()
    {
       if(enemyCount == 0) // If the whole convoy is dead
        {
            Debug.Log("All enemies are destroyed");
            secondStar.enabled = true; // Display completed mission
        }
    }

    // A function to decrease the number of remaining enemies upon the call
    public void enemyDestroyed()
    {
        enemyCount--;
    }

    IEnumerator SpawnEnemies() // Spawning enemies
    {
        // Spawn 4 tanks 
        for(int i = 0; i < 4; i++)
        {
            // Spawn a tank
            GameObject tank = (GameObject)Instantiate(tankPrefab, gameObject.transform.position, gameObject.transform.rotation);

            enemyCount++;

            // Wait for 5 seconds
            yield return new WaitForSeconds(5);
        }

        // Spawn a truck
        GameObject truck = (GameObject)Instantiate(truckPrefab, gameObject.transform.position, gameObject.transform.rotation);
        enemyCount++;
    }


}
