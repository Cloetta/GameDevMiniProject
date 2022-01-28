using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    public Animator animator;
    public EnemyHealthBar enemyHpBar;

    //Public reference to the scriptable object which is going to give every different type of enemy its own stats
    public Enemy enemy;
    
    //References to other components and scripts
    public SpriteRenderer enemySprite;
    public GameObject currentEnemy;
    private PlayerCombat playerCombat;
    public EnemyMovement enemyMovement;
    public Transform attackPoint;
    public LayerMask enemyLayers;


    //MEMO: Make those private after debbugging phase ended
    public int currentEnemyHp;
    public bool canAttack;

    //More stats for the enemy
    public float attkRange = 1.5f;
    private float nextAttkTime;



    private void Start()
    {
        playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();

        //Set up the health bar and its values for the enemy
        currentEnemyHp = enemy.maxHP;
        enemyHpBar.SetMaxHealth(enemy.maxHP);
        enemyHpBar.SetHealth(currentEnemyHp);
        canAttack = false;
    }


    private void Update()
    {
        enemyHpBar.SetHealth(currentEnemyHp);
        //Debug.Log("EnemyHP" + currentEnemyHp);
        //enemyDamage = enemy.meleeDamage;

        if (Time.time >= nextAttkTime)
        {
            canAttack = true;
             
            if (enemyMovement.distance < attkRange) 
            { 
                AttackPlayer();
                nextAttkTime = Time.time + 1f / enemy.attackRate;
                canAttack = false;
            }

        }

    }

    public void AttackPlayer()
    {

        FindObjectOfType<AudioSystem>().Play("EnemyAttack");
        animator.SetTrigger("Attack");

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attkRange, enemyLayers);

        //Apply damage to enemies
        foreach (Collider2D player in hitPlayer)
        {
            //Debug line
            //Debug.Log("We hit " + player.name);

            //The number in "takeDamage" function is the damage taken by the player
            playerCombat.PlayerTakingDamage(enemy.meleeDamage);
        }
    }

    public void EnemyTakingDamage(int playerDamage)
    {
        FindObjectOfType<AudioSystem>().Play("HitByEnemy");
        StartCoroutine(DamageFeedback());

        //Subctact the damage give by the player
        currentEnemyHp -= playerDamage;

        //After the enemy took damage, checks if it's still alive or it needs to destroy the object 
        if (currentEnemyHp <= 0)
            
        {
            enemyHpBar.SetHealth(currentEnemyHp);
            //Call the coroutine and the dying animation for the enemy
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
        //Debug:
        //Debug.Log("Enemy died!");

        for (int Index = 0; Index < 5; Index++)
        {
            enemySprite.color = new Color(255, 255, 255, 0);
            yield return new WaitForSeconds(0.02f);
            enemySprite.color = Color.white;
            yield return new WaitForSeconds(0.02f);
        }
        enemyMovement.enabled = false;
        this.enabled = false;

        Destroy(currentEnemy, 0.5f);
    }


    //Draw a circle representing the attack range of the enemy
    void OnDrawGizmosSelected()
    {

        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attkRange);
    }




}
