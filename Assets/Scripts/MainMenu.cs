using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }
    public void MainGame()
    {
        SceneManager.LoadScene("Stage1");
        Time.timeScale = 1f;
    }

    public void Credits()
    {
        //SceneManager.LoadScene("LevelsSelection");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is Exiting");
    }
}