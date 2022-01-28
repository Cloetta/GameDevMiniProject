using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{

    public KeyCode btnKey;


    public Image icon;
    public Button removeButton;
    public Button useButton;
    private Button buttonOnKey;
    Skills skill;
    public PlayerCombat player;


    //they need to be public to be accessed from another script
    public Image imgCooldown;
    public Text txtCooldown;

    public bool isCooldown = false;
    public float cooldownTimer = 0f;


    void Awake()
    {
        buttonOnKey = GetComponent<Button>();

    }

    private void Start()
    {        
        txtCooldown.gameObject.SetActive(false);
        imgCooldown.fillAmount = 0f;
    }


    void Update()
    {
        if (Input.GetKeyDown(btnKey))
        {
            useButton.onClick.Invoke();
        }

        if (isCooldown)
        {
            ApplyCooldown();
        }
    }

    void ApplyCooldown()
    {
        //Subtrack time since last called
        cooldownTimer -= Time.deltaTime;

        //Condition to make the text and the filler of the image active according to the status of the skill
        if (cooldownTimer < 0.0f)
        {
            isCooldown = false;
            txtCooldown.gameObject.SetActive(false);
            imgCooldown.fillAmount = 0.0f;
        }
        else
        {
            txtCooldown.text = Mathf.RoundToInt(cooldownTimer).ToString();
            imgCooldown.fillAmount = cooldownTimer / skill.skillCooldown;
        }

    }


    public void AddToSkillEquip(Skills newSkill)
    {
        skill = newSkill;
        icon.sprite = skill.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        skill = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        SkillsEquipment.instance.RemoveSkill(skill);
        FindObjectOfType<AudioSystem>().Play("Remove");
    }

    public void UseSkill()
    {
        //cHeck if the cooldown of the skill is active
        if (isCooldown)
        {
            //Notify we can't use the skill
            Debug.Log("Skill in cooldown!");

            //return false;
        }
        else
        {
            if (skill != null)
            {                
                Debug.Log("Skill used!" + skill.name);
                player.SkillEffect(skill.name, skill.hpHeal, skill.sugarHeal, skill.damageToEnemy, skill.skillCooldown, skill.sugarCost);
                isCooldown = true;
                txtCooldown.gameObject.SetActive(true);
                //Set the cooldown time to the skillcooldown value of the skill assigned 
                cooldownTimer = skill.skillCooldown;
            }
            else if (skill == null)
            {               
                Debug.Log("You have no skill equipped on this slot");
            }
        }

    }
    
}