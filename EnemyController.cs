using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    // Spawn point
    public GameObject spawner;
    public float health = 100f; 

    private void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("EnemySpawner"); // Assigning an existing game object to an instance of an enemy
    }

    // Destruction of itself
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Missile")) // Destroying enemy vehicle if it collides with missile
        {
            Destroy(gameObject);
            spawner.GetComponent<EnemySpawner>().enemyDestroyed(); // After destruction, reduce the list of enemies remaining

        } else if (other.CompareTag("Case")) // Collect the case 
        {
            Debug.Log("Enemy has captured the case");
            Destroy(other.gameObject); // Destroying "Case"

        } else if (other.CompareTag("Player_heli")) // Destroy heli if it touches any enemy vehicles
        {
            Destroy(other.gameObject);
        }

    }

    void Update()
    {
        // Enemy movement
        // For some reason its pos(z,y,x)
        gameObject.transform.Translate(0, 0, 2f * Time.deltaTime); // z is negative so we add -

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
