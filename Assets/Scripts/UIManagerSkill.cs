using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerSkill : MonoBehaviour
{
    static UIManagerSkill playerUi;

    private void Awake()
    {
        if (playerUi != null)
        {
            Destroy(this.gameObject);
            return;
        }

        playerUi = this;
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(this.gameObject);
    }
}

