using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : MonoBehaviour
{

    public Transform itemsParent;
    public GameObject skillsUI;

    SkillSlot[] skillSlots;

    SkillsEquipment skillsEquipment;


    // Start is called before the first frame update
    void Start()
    {
        skillsEquipment = SkillsEquipment.instance;
        skillsEquipment.onSkillChangedCallBack += UpdateUI;

        skillSlots = itemsParent.GetComponentsInChildren<SkillSlot>();

        UpdateUI();
    }

    void UpdateUI()
    {
        Debug.Log("updating skill ui");


        for (int index = 0; index < skillSlots.Length; index++)
        {
            if (index < skillsEquipment.skills.Count)
            {
                skillSlots[index].AddToSkillEquip(skillsEquipment.skills[index]);
            }
            else
            {
                skillSlots[index].ClearSlot();
            }
        }


    }
}
