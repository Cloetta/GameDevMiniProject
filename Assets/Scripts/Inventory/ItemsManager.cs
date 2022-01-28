using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemsManager: ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public int hpHeal = 0;
    public int sugarHeal = 0;
    public bool isDefaultItem = false;
    public int permanentHealth = 0;
    public int permanentSugar = 0;
    public string description = "";
    

    public virtual void Use()
    {
        //use the item
        //add the stats into the player 

        //something might happen

        FindObjectOfType<AudioSystem>().Play("Use");

        RemoveFromInventory();

        PlayerCombat playerheal = FindObjectOfType<PlayerCombat>();
        playerheal.currentHealth += hpHeal;
        playerheal.currentSugar += sugarHeal;
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        playerheal.maxHealth += permanentHealth;
        playerheal.maxSugar += permanentSugar;


        playerheal.hpBar.SetMaxHealth(playerheal.maxHealth);
        playerheal.sugarBar.SetMaxSugar(playerheal.maxSugar);


        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.DeleteItem(this);
    }

    


}


