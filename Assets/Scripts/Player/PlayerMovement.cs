using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{

    public float playerSpeed = 5f;
    
    public bool canMove = true;

    //Declare components attached to the player
    public Rigidbody2D rbody;
    public Animator animator;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

   
    void Update()
    { 
        //Gets the movement values from manual input
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //if canmove is true, and movement is not equal to 0, animations are in place
        if (canMove && movement != Vector2.zero)
        {

            animator.SetBool("isWalking", true);
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);

            //This is actually moving the player translating the movement value we stored before to an actual movement
            rbody.position = rbody.position + movement * Time.deltaTime * playerSpeed;

        }

        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
