using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    public Canvas gameOver;

    public Transform heli;
    public Vector3 offset;

    // Update is called once per frame
    void FixedUpdate ()
    {
        if(heli == null)
        {
            gameOver.enabled = true;

        } else
        {
            transform.position = new Vector3(heli.position.x, offset.y, offset.z);
        }
    }
}
