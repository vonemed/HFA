using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject tankPrefab;
    public GameObject truckPrefab;

    [SerializeField]
    private int enemyNum;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(SpawnEnemies());
	}

    IEnumerator SpawnEnemies() // Spawning enemies
    {
        // Spawn first tank
        GameObject tank = (GameObject)Instantiate(tankPrefab, gameObject.transform.position, gameObject.transform.rotation);

        // Wait for 5 seconds
        yield return new WaitForSeconds(5);

        // Spawn second tank
        GameObject tank1 = (GameObject)Instantiate(tankPrefab, gameObject.transform.position, gameObject.transform.rotation);

        // Wait for 5 seconds
        yield return new WaitForSeconds(5);

        // Spawn third tank
        GameObject tank2 = (GameObject)Instantiate(tankPrefab, gameObject.transform.position, gameObject.transform.rotation);

        // Wait for 5 seconds
        yield return new WaitForSeconds(5);

        // Spawn forth tank
        GameObject tank3 = (GameObject)Instantiate(tankPrefab, gameObject.transform.position, gameObject.transform.rotation);

        // Wait for five seconds
        yield return new WaitForSeconds(5);

        // Spawn a truck
        GameObject truck = (GameObject)Instantiate(truckPrefab, gameObject.transform.position, gameObject.transform.rotation);
    }
}
