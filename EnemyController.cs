using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    void Start()
    {

    }
    // Destruction of itself
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Missile")) // Destroying enemy vehicle if it collides with missile
        {
            Destroy(gameObject); 

        } else if (other.gameObject.CompareTag("Case")) 
        {
            Debug.Log("Enemy has captured the case");
            Destroy(other.gameObject); // Destroying "Case"
        }

    }

    void Update()
    {
        // Enemy movement
        // For some reason it's pos(z,y,x)
        gameObject.transform.Translate(0, 0, gameObject.transform.position.z * (-Time.deltaTime)); // z is negative so we add -

    }
}
