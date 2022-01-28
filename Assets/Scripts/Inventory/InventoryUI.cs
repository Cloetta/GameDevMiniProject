//https://www.youtube.com/watch?v=YLhj7SfaxSE&t=95s Brackeys

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public Transform itemsParent;
    public GameObject inventoryUI;
    private PlayerMovement playerScript;

    InventorySlot[] slots;

    Inventory inventory;

    public bool inventoryOn = false;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;

        inventory.onItemChangedCallBack += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        UpdateUI();

    }

    // Update is called once per frame
    void Update()
    {
        //Call the player script so we can disable it temporarily while the inventory is open
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryOn == false)
            {
                inventoryUI.SetActive(true);
                playerScript.enabled = false;
                Time.timeScale = 0f; //Game is paused while the inventory is open
                inventoryOn = true;
                FindObjectOfType<AudioSystem>().Play("MenuClick"); //Feedbackfor opening the menu
            }
            else
            {
                inventoryUI.SetActive(false);
                playerScript.enabled = true;
                Time.timeScale = 1f;
                inventoryOn = false;
                FindObjectOfType<AudioSystem>().Play("Remove"); //Feedback for closing the menu
            }
        }
    }

    //Updates UI with the items collected and added into the List
    void UpdateUI()
    {
        Debug.Log("updating ui");

        for( int index = 0; index < slots.Length; index++)
        {
            if (index < inventory.items.Count)
            {
                slots[index].AddToInventory(inventory.items[index]);
            }
            else
            {
                slots[index].ClearSlot();
            }
        }


    }
}
