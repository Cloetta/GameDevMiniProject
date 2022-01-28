using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExitManager : MonoBehaviour
{
    public string nextScene;

    public bool canExit;

    private PlayerCombat player;
    private GameManager gameManager;
 
    //Game objects array to check how many items and enemies are in the current scene
    public GameObject[] itemsInRoom;
    public GameObject[] enemiesInRoom;
    public GameObject playerPosition;

    //Declare position values so that we can control where the player is "spawning" on scene change
    public float x;
    public float y;
  

    private void Start()
    {
       
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        //Populating the arrays with the references
        itemsInRoom = GameObject.FindGameObjectsWithTag("PickUp");
        enemiesInRoom = GameObject.FindGameObjectsWithTag("Enemy");

        //Finding the player to access its position
        playerPosition = GameObject.Find("Player");

        //Transform the player position according to the values we assigned into the inspector
        playerPosition.transform.position = new Vector2(x, y);
    }

    private void Update()
    {
        if (canExit == true && Input.GetKeyDown(KeyCode.Space))
        {
            //next scene
            SaveCurrentPlayerStatus();
            SceneManager.LoadScene(nextScene);
           
        }
        else if (canExit == false && Input.GetKeyDown(KeyCode.Space))
        {
            //MEMO: 
            //Make it a pop-up window/text somewhere?

            Debug.Log("You need to defeat all enemies and pick up all items before to leave the room!");
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject item in itemsInRoom)
            {
                if (item == null)
                {
                    foreach (GameObject enemies in enemiesInRoom)
                    {
                        if (enemies == null)
                        {
                            canExit = true;
                        }
                    }
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canExit = false;
        }
    }

    //Note for me: maybe i can get rid of this function
    public void SaveCurrentPlayerStatus()
    {
        gameManager.playerHp = player.currentHealth;
        gameManager.playerSugar = player.currentSugar;

    }
}

