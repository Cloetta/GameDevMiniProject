using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class Skills : ItemsManager
//Inherits partially statistics and attributes of the itemmanager script 
{

    public int damageToEnemy = 0;
    public float skillCooldown = 0;
    public int sugarCost = 0;
    public bool isDefaultSkill = false;
    public bool targetIsSelf = false;

    public override void Use()
    {
        //On use, we equip the skill into the SkillUI
        SkillsEquipment.instance.EquipSkill(this);
        base.Use();
        //use the item
        //add the stats into the player 

        //something might happen
        Debug.Log("Equipping " + name);
    }

    //This function allows us to remove the skill from the UI and not lose it
    public void RemoveSkillFromUI()
    {
        SkillsEquipment.instance.RemoveSkill(this);

        Debug.Log("This skill has been removed");

    }
}
