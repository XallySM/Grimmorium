using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    public bool isInteracting; //Una bool que va a servir para llamar a cualquier bool del animator y así saber si estás haciendo una acción

    InputManager inputManager; //Llamar a input manager
    PlayerLocomotion playerLocomotion; //Llamar a player locomotion
    PlayerStats playerStats;
   
    
    public bool isUsingRootMotion;
    public bool isInvulnerablePlayer;
    //public bool isBlocking;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        animator = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if(playerStats.playerIsDead == false)
        { 
        inputManager.HandleAllInputs();
        //animator.SetBool("isBlocking", isBlocking);

        }
        else
        {

        }

    }

    private void FixedUpdate()
     {
        if (playerStats.playerIsDead == false)
        {
            playerLocomotion.HandleAllMovement();
            isInteracting = animator.GetBool("isInteracting");
            playerLocomotion.isJumping = animator.GetBool("isJumping");
            animator.SetBool("isGrounded", playerLocomotion.isGrounded);
            animator.GetBool("isInvulnerable"); //Puede ir en Update?
            //animator.SetBool("isBlocking", isBlocking);


        }
        else
        {

        }
        
    }

    private void LateUpdate()
    {
        
        isUsingRootMotion = animator.GetBool("isUsingRootMotion");
        inputManager.swordAttack_input = false;
    }


    public void EnableIsInvulnerable()
    {
        isInvulnerablePlayer = true; 
        animator.SetBool("isInvulnerable", true);
    }

    public void DisableIsInvulnerable()
    {
        isInvulnerablePlayer = false;
        animator.SetBool("isInvulnerable", false);
    }
}
