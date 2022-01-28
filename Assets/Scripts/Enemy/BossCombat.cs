using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombat : MonoBehaviour
{
    public Animator animator;
    public EnemyHealthBar enemyHpBar;

    //Public reference to the scriptable object which is going to give every different type of enemy its own stats
    public Enemy enemy;

    //References to other components and scripts
    public SpriteRenderer enemySprite;
    public GameObject currentEnemy;
    private PlayerCombat playerCombat;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    //Memo: make those private after debbugging phase ended
    public int currentEnemyHp;
    public bool canAttack;
    
    //Attack range value for the enemy
    public float attkRange = 1.5f;

    //This determines when the next attack is going to be performed by the enemy
    private float nextAttkTime;

    //Spell Variables and components
    public GameObject darknessPrefab;
    public float SpellSpeed = 20f;
    public float bossSkillSpeed = 3f;

    public bool inRange;
    
    private void Start()
    {

        //Look for the player component
        playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();

        //Setup values for the enemy hp bar
        currentEnemyHp = enemy.maxHP;
        enemyHpBar.SetMaxHealth(enemy.maxHP);
        enemyHpBar.SetHealth(currentEnemyHp);

    }


    private void Update()
    {
        //Update enemy health bar
        enemyHpBar.SetHealth(currentEnemyHp);

        //Get Axis and store float values into variables to give direction to the spell
        float spellHorizontal = playerCombat.transform.position.x;
        float spellVertical = playerCombat.transform.position.y;

        //Condition determines how often the enemy can attack
        if (Time.time >= nextAttkTime)
        {
            //when time is passed, then the enemy can attack again...
            canAttack = true;
            //... if it's in range!
            if (inRange == true)
            {
                SpellCast(spellHorizontal, spellVertical);                   
                nextAttkTime = Time.time + 1f / enemy.attackRate;
                //After the enemy attacked, he can't attack again until the condition above is met
                canAttack = false;
            }

        }

    }

    //Check collision with player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
        }
    }

    //Check collision with player
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" )
        {
            inRange = false;
        }
    }

    //Allows the enemy to take damage
    public void EnemyTakingDamage(int playerDamage)
    {
        //Plays the feedback sound
        FindObjectOfType<AudioSystem>().Play("HitByEnemy");

        //Visual feedback for damage taken 
        StartCoroutine(DamageFeedback());

        currentEnemyHp -= playerDamage;

        if (currentEnemyHp <= 0)

        {
            //Updates the health bar with the damage
            enemyHpBar.SetHealth(currentEnemyHp);
            
            //Feedback for Death 
            FindObjectOfType<AudioSystem>().Play("HitByBoss");
            //Set the win condition to true into the game manager script
            FindObjectOfType<GameManager>().bossIsDead = true;
            //Visual feedback for death
            StartCoroutine(Die());
        }

    }

    IEnumerator DamageFeedback()
    {
        for (int Index = 0; Index < 2; Index++)
        {
            enemySprite.color = new Color(255, 255, 255, 0);
            yield return new WaitForSeconds(0.07f);
            enemySprite.color = Color.white;
            yield return new WaitForSeconds(0.07f);
        }

       
    }

    
    IEnumerator Die()
    {
        //Debug.Log("Enemy died!");
        this.enabled = false;

        //Waits for 3 seconds before to destroy the enemy object (time to allow the coroutine to complete the animation)
        Destroy(currentEnemy, 3f);


        for (int Index = 0; Index < 30; Index++)
        {
            enemySprite.color = new Color(255, 255, 255, 0);
            yield return new WaitForSeconds(0.05f);
            enemySprite.color = Color.white;
            yield return new WaitForSeconds(0.05f);
        }
        
    }

    public void SpellCast(float x, float y)
    {
        //Instantiate the spell from the prefab into the scene
        GameObject Darkness = Instantiate(darknessPrefab, transform.position, transform.rotation) as GameObject;

        FindObjectOfType<AudioSystem>().Play("BossAttack");

        //Give the spell a rigidbody so that we can control his velocity as it has been shoot 
        Darkness.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * SpellSpeed : Mathf.Ceil(x) * SpellSpeed,
            (y < 0) ? Mathf.Floor(y) * SpellSpeed : Mathf.Ceil(y) * SpellSpeed,
            0
            );

        //Destroy the Spell object (with a delay of 2 seconds)
        Destroy(Darkness, 2f);

    }

    //On boss object destroy... We WON! 
    //Trigger the WinScene and finish the game
    private void OnDestroy()
    {
        FindObjectOfType<GameManager>().Win();
    }
        


}


