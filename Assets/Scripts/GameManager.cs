using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //private PlayerCombat currentSession;

    public Animator sceneTransition;
    public float sceneTransitionTime = 1f;
    
    //Static instance of GameManager which allows it to be accessed by any other script
    public static GameManager instance = null;

    public CollectingItems items;

    //Player stats passed from a scene to another
    public int playerHp = 100;
    public int playerSugar = 100;

    bool isgameOver = false;

    public bool bossIsDead;

    void Awake()
    {
        //Getting the players default values when a new game manager instance is created
        playerHp = PlayerStats.currentHP;
        playerSugar = PlayerStats.currentSugar;
        bossIsDead = false;

        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(this.gameObject);
    }

    public void GameOver()
    {
        if (isgameOver == false)
        {
            isgameOver = true;
            LoadGameOverScene();
            //https://www.youtube.com/watch?v=VbZ9_C4-Qbo min 10:30

            PlayerStats.currentHP = 100;
            PlayerStats.currentSugar = 100;

        }

    }
 
    void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");

        //To consider: this load the same scene active, like a "checkpoint" system
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("StartMenu");  
    }

    public void LoadNextScene(/*int currentHealth*/)
    {
        //Memo and notes:
        //Load next scene checking the index
        StartCoroutine(LoadLevel(/*SceneManager.GetActiveScene().buildIndex + 1)*/));
        //Passing health stat to the next scene
        //playerHP = currentHealth;
    }


    IEnumerator LoadLevel(/*int levelIndex*/)
    {
        //The coroutine allows us to make a scene transition
        sceneTransition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        //Memo and notes:
        //SceneManager.LoadScene(/*levelIndex*/);
    }

    public void Win()
    {
        if (bossIsDead == true)
        {
            SceneManager.LoadScene("WinScene");
        }
        
    }
}
