using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public float deltaTime = 0.0f;
    public Text _fpcCounter;

    private float fps = 0.0f;

    void Update()
    {
        deltaTime += Time.deltaTime;
        deltaTime /= 2.0f;
        fps = 1.0f / deltaTime;

        _fpcCounter.text = "" + fps; // Displaying frames per second
    }
}
