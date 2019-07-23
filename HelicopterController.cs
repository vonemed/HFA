using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{

    public bool isFlat = true;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tilt = Input.acceleration;
        if (isFlat)
        {
            //tilt = Quaternion.Euler(90, 0, 0) * tilt;
        }

        transform.Translate(0, 0, tilt.x * Time.deltaTime * 9);
    }
}