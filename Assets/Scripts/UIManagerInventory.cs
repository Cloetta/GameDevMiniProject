using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerInventory: MonoBehaviour
{ 

    static UIManagerInventory inventoryUi;

    private void Awake()
    {
        if (inventoryUi != null)
        {
            Destroy(this.gameObject);
            return;
        }

        inventoryUi = this;
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(this.gameObject);
    }

}
