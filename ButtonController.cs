using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public Button _startButton;
    public Button _exitButton;
	// Use this for initialization
	void Start ()
    {
        Button _btn = _startButton.GetComponent<Button>();
        Button _btn2 = _exitButton.GetComponent<Button>();

        _btn.onClick.AddListener(_startLevel);
        _btn2.onClick.AddListener(_exit);
        
	}

    void _startLevel() //load level on start press
    {
        SceneManager.LoadScene("Level.1", LoadSceneMode.Single);
    }

    void _exit() //exits on exit button
    {
        Debug.Log("EXIT");
        Application.Quit();
    }
}
