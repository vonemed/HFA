using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelicopterController : MonoBehaviour
{ 
    public bool isFlat = true;

    public RawImage firstStar; // The image of first star to indicate the completion of first mission.
  
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Case"))  // If heli is colliding with a case
        {
            Destroy(other.gameObject);
            Debug.Log("You collected the intle"); // Debug purposes

            // Update the mission screen
            firstStar.enabled = true;
        }
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