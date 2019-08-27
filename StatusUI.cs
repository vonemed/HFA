using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    public RawImage star;

    public Text statText;

    // Update is called once per frame
    void Update()
    {
        // If player completed main objective activate status text and panel
        if(star.enabled == true)
        {
            gameObject.GetComponent<Image>().enabled = true;
            statText.enabled = true;
        }
    }
}
