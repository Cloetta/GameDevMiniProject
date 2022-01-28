using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    //Public references to components
    public SlidersManager hpBar;
    public SlidersManager sugarBar;
    public SpriteRenderer playerSprite;

    //Private references to components
    private SkillSlot equippedSkill;
    private GameObject skillAnimation;
    private PlayerMovement playerMovement;
    private Animator animator;

    //LayerMask and attack point where the player magic (and animation) generates
    public LayerMask enemyLayers;
    public Transform attackPoint;

    //Variables representing player statistics and values
    public float playerAttackRange;

    public int maxHealth;
    public int currentHealth;

    public int maxSugar;
    public int currentSugar;
    private float sugarRegenTime;

    public static PlayerCombat instance;


    private void Awake()
    {

        //Create a non-destroyable instance of the player so that we don't lose data between scenes
        if(instance != null)
        {
            Destroy(this.gameObject);

            return;
        }


        instance = this;

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(this.gameObject);

    }


    // Start is called before the first frame update
    void Start()
    {
        hpBar.SetMaxHealth(maxHealth);
        hpBar.SetHealth(currentHealth);

        sugarBar.SetMaxSugar(maxSugar);
        sugarBar.SetSugar(currentSugar);

        playerSprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        equippedSkill = GameObject.FindGameObjectWithTag("Skill").GetComponent<SkillSlot>();


        skillAnimation = GameObject.Find("SkillAnim");
        currentHealth = GameManager.instance.playerHp;
        currentSugar = GameManager.instance.playerSugar;
        playerAttackRange = 2.0f;

    }


    void Update()
    {
        //Condition to trigger sugar regeneration, 1 point per second
        if (maxSugar > currentSugar)
        {
            if (Time.time >= sugarRegenTime)
            {
                
                currentSugar++;
                sugarRegenTime = Time.time + 1f ;
            }
        }

        //Check the current health/sugar value is not going over the estabilished max
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentSugar > maxSugar)
        {
            currentSugar = maxSugar;
        }

        //Updating the player hp and sugar(mana) bars
        sugarBar.SetSugar(currentSugar);
        hpBar.SetHealth(currentHealth);
    }

    //Player is taking damage and checking if it's still alive after the enemy attacked
    public void PlayerTakingDamage(int damage)
    {

        FindObjectOfType<AudioSystem>().Play("HitByBoss");


        StartCoroutine(DamageFeedback());

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());            
        }
    }

    //Corutine: ad a feedback for the damaged player, this is making the player sprite flashing for a very little amount of time
    IEnumerator DamageFeedback()
    {
        for (int Index = 0; Index < 2; Index++)
        {
            playerSprite.color = new Color(255, 255, 255, 0);
            yield return new WaitForSeconds(0.07f);
            playerSprite.color = Color.white;
            yield return new WaitForSeconds(0.07f);
        }
    }

    public void SkillEffect(string skillName, int skillHpHeal, int skillSugarHeal, int skillDamage, float skillCooldown, int skillSugarCost)
    {
        //Look for the Play() function in order to play the relevant audio effect for the skill
        FindObjectOfType<AudioSystem>().Play(skillName);

        //Validate the HP/Sugar(mana) value doesn't go above the max causing irregularities
        if (currentSugar >= skillSugarCost)
        {
            //animation of the skill effect, same trigger or whatever to make it work :\
            skillAnimation.GetComponent<Animator>().SetTrigger(skillName);

            //Look for all the enemy hit in the range area 
            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, playerAttackRange, enemyLayers);

            //For each enemy identified, look for its "combat" component and trigger its function to apply the damage
            foreach (Collider2D enemy in hitEnemy)
            {
                Debug.Log("Enemy name is: " + enemy.name);

                if (enemy.name == "Boss")
                {
                    enemy.GetComponent<BossCombat>().EnemyTakingDamage(skillDamage);
                }
                else
                {
                    enemy.GetComponent<EnemyCombat>().EnemyTakingDamage(skillDamage);
                }

                //Line for debugging purposes
                //Debug.Log("We hit " + enemy.name);
            }

            //Apply skill effects to player values
            currentSugar -= skillSugarCost;
            currentHealth += skillHpHeal;
            currentSugar += skillSugarHeal;

            //Line for debugging purposes
            //Debug.Log("Sugar spent:" + skillSugarCost + "/ Damage: " + skillDamage + "/ Heal: " + skillHpHeal);
        }
        else
        {
            //Line for debugging purposes
            Debug.Log("Not enough mana");
        }
    }

    IEnumerator Die()
    {
        hpBar.SetHealth(currentHealth);  
        //Debug.Log("Player died!");

        //Play dying animation
        animator.SetBool("isDead", true);

        //Wait until the end of the animation (1.5 seconds) to run the rest of the script
        yield return new WaitForSeconds(1.5f);

        //disable the script so the player doesn't move anymore
        this.enabled = false;
        playerMovement.enabled = false;

        //Look for the "GameOver" Function into the gamemanager script, so that the GameOver scene is loaded on player's death
        FindObjectOfType<GameManager>().GameOver();

    }


    //Draw a circle representing the attack range of the player
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, playerAttackRange);
    }



}
