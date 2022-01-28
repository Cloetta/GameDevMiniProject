using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button deleteButton;
    ItemsManager item;

    public void AddToInventory(ItemsManager newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        deleteButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        deleteButton.interactable = false;
    }

    public void OnDeleteButton()
    {
        FindObjectOfType<AudioSystem>().Play("Remove");
        Inventory.instance.DeleteItem(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}


