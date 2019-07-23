using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroyer : MonoBehaviour
{
    public float _delay = 2f;
    float countdown;

    void Start()
    {
        countdown = _delay;
    }
    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0) //destroys particle effect after 2 seconds
        {
            Debug.Log("Times up");
            Destroy(gameObject);
        }


    }

}
