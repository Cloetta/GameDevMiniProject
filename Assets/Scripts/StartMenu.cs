using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    //Load  new game
    public void NewGame()
    {
        SceneManager.LoadScene("Level0");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void StartMainMenu()
    {
        SceneManager.LoadScene("StartMenu");

    }


    public void ConfirmSound()
    {
        FindObjectOfType<AudioSystem>().Play("MenuClick");
    }
}
