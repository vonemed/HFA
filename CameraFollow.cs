using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform _heli;
    public Vector3 offset;

    // Update is called once per frame
    void FixedUpdate ()
    {
        transform.position =  new Vector3(_heli.position.x, offset.y, offset.z);
    }
}
