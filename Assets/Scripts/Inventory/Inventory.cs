//https://www.youtube.com/watch?v=HQNl3Ff2Lpo  Brackeys tutorial (adapted), inventory system in RPGs


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //NOTE:
    //A singleton is a class that allows only a single instance of itself to be created and gives access to that created instance.
    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one instance of inventory found");
            Destroy(this.gameObject);
            return; //returning out of this method
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    #endregion    

    //NOTE:
    //A Delegate is a reference pointer to a method. It allows us to treat method as a variable and pass method as a variable for a callback. When it get called , it notifies all methods that reference the delegate. The basic idea behind them is exactly the same as a subscription magazine. Anyone can subscribe to the service and they will receive the update at the right time automatically.
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public int inventorySlots = 20;

    public List<ItemsManager> items = new List<ItemsManager>();


    public bool AddItem (ItemsManager item)
    {
        if (!item.isDefaultItem)
        {
            //Checks if all the slots are occupied
            if (items.Count >= inventorySlots)
            {
                Debug.Log("Not enough room");
                return false;
            }

            items.Add(item);

            if (onItemChangedCallBack != null)
            {
                //Triggers the event created before
                onItemChangedCallBack.Invoke();
            }
        }
        return true;
    }

    public void DeleteItem (ItemsManager item)
    {
        items.Remove(item);

        if (onItemChangedCallBack != null)
        {
            //Trigger the event we created before
            onItemChangedCallBack.Invoke();

        }
    }
}
