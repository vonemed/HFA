using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [Header("Buttons")]
    public Button replayBtn;
    public Button mainMenuBtn;


    // Start is called before the first frame update
    void Start()
    {
        replayBtn.onClick.AddListener(resetScene);
        mainMenuBtn.onClick.AddListener(mainMenuScene);
    }

    void resetScene()
    {
        SceneManager.LoadScene("Level.1");
    }

    void mainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
