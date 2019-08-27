using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    public Canvas gameOver;
    public Canvas gameUI;

    public Transform heli;
    public Vector3 offset;

    // Update is called once per frame
    void FixedUpdate ()
    {
        // If helis is destroyed enable game over panel
        if(heli == null)
        {
            gameOver.enabled = true;
            gameUI.enabled = false; // Disable main game UI

        } else
        {
            transform.position = new Vector3(heli.position.x, offset.y, offset.z);
        }
    }
}
