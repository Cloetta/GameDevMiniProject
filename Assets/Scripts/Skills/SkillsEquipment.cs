using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsEquipment : MonoBehaviour
{
    //Instantiating a not destroyable object so that we carry the items / skills and relative data between scenes
    #region Singleton

    public static SkillsEquipment instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Equipment successfully passed!");
            Destroy(this.gameObject);
            return; //returning out of this method
        }
       
        instance = this;
        
        GameObject.DontDestroyOnLoad(this.gameObject);
      
        
    }

    #endregion    

    public delegate void OnSkillChanged();
    public OnSkillChanged onSkillChangedCallBack;

    public int skillSlots = 4;

    //Slightly same mechanism of the inventory, i adapted it for the skills too
    public List<Skills> skills = new List<Skills>();

    public bool EquipSkill(Skills skill)
    {
        if (!skill.isDefaultSkill)
        {
            if (skills.Count >= skillSlots)
            {
                Debug.Log("Not enough room");
                return false;
            }

            skills.Add(skill);
                       

            if (onSkillChangedCallBack != null)
            {
                //we are triggering the event
                onSkillChangedCallBack.Invoke();

            }


        }

        return true;
    }

    public void RemoveSkill(Skills skill)
    {
        skills.Remove(skill);

        Inventory.instance.AddItem(skill);

        if (onSkillChangedCallBack != null)
        {
            //we are triggering the event
            onSkillChangedCallBack.Invoke();

        }


    }

}


