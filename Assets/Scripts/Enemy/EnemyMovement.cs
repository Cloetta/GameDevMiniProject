using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    public Rigidbody2D enemyRigidbody;
    public Animator animator;

    public float moveSpeed = 3f;

    //MEMO: set it to private after debugging
    public float stopDistance = 1.5f;

    public bool canMove;

    public float distance;

    private void Start()
    {
        canMove = true;

        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {
        Vector2 Target = player.transform.position;
        //Debug.Log("Target" + Target);

        Vector2 Position = this.transform.position;
        //Debug.Log("Position" + Position);

        Vector2 Direction = Target - Position;
        //Debug.Log("Direction" + Direction);

        distance = Vector2.Distance(Position, Target);


        //Checking distance and stops when it's too close to the player
        //With those condition set in this way, enemies and player won't push each other (ONLY when they're near, the constraints are activated. Enabling constraints entirely would make the enemy ignore collisions with the environment)
        if (Vector2.Distance(Position, Target) > stopDistance)
        {
            Vector2 movement = Vector2.MoveTowards(Position, Target, moveSpeed * Time.deltaTime);

            transform.position = movement;

            canMove = true;

            
            enemyRigidbody.constraints = RigidbodyConstraints2D.None;
            enemyRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        }
        else
        {
            canMove = false;
            enemyRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        //If the enemy is moving, animation is triggered
        if (canMove == true && Position != Vector2.zero)
        {
            animator.SetBool("isWalking", true);

            animator.SetFloat("Horizontal", Direction.x);
            animator.SetFloat("Vertical", Direction.y);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        

    }

}



