using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingItems: MonoBehaviour
{
    public bool canPickup = false;

    public ItemsManager item;    

    void Update()
    {
        if (canPickup == true && Input.GetKeyDown(KeyCode.Space))
        {
           
            
            PickUpItem();

            //Possible implementation: text appears on screen? On hover on the item to inform the player of the item stats/skills effects?
        }
        else if (canPickup == false && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Not in range");
            
            //nothing happens
        }
    }

    //Check collision with the player to determine if we can interact with the item or not
    private void OnTriggerEnter2D(Collider2D collidedObject)
    {
        if (collidedObject.tag == "Player")
        {
            canPickup = true;          
        }
    }

    //Check collision with the player to determine if we can interact with the item or not
    private void OnTriggerExit2D(Collider2D collidedObject)
   {
        if (collidedObject.tag == "Player")
        {
            canPickup = false;
        }
               
    }


    public void PickUpItem()
    {
        //Feedback for picking the item
        FindObjectOfType<AudioSystem>().Play("CollectedItem");

        Debug.Log("Picked up: " + item.name);

        //Item is added to the inventory
        bool pickedUp = Inventory.instance.AddItem(item);
        
        if (pickedUp == true)
        {
            //Object into the environment is destroyed
            Destroy(gameObject);
        }

    }
}
