using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private bool destroyed = false;

    void Start()
    {

    }
    // Destruction of itself
    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject); //destroy
   
        destroyed = true;

    }

    void Update()
    {
        if(destroyed)
        {
           
        }
        // Enemy movement
        // For some reason it's pos(z,y,x)
        gameObject.transform.Translate(0, 0, gameObject.transform.position.z * (-Time.deltaTime)); // z is negative so we add -

    }
}
