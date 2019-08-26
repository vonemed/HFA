using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCol : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Missile"))
        {
            Destroy(gameObject);
            Debug.Log("Object was destroyed by misiile, with love");
        }
    }
}
