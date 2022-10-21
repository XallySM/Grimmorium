using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;           //Recibe el animator
    PlayerManager playerManager; //Referencia a Player Manager
    PlayerLocomotion playerLocomotion; //Referencia a Player Locomotion
    PlayerStats playerStats;
    int horizontal;             //Int para movimiento horizontal
    int vertical;               //Int para movimiento vertical 

    private void Awake()
    {
        animator = GetComponent<Animator>(); //Acceder al animator 
        horizontal = Animator.StringToHash("Horizontal"); //Convertir el parametro en hash
        vertical = Animator.StringToHash("Vertical");     //Convertir el parametro en hash 
        playerManager = GetComponent<PlayerManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerStats = GetComponent<PlayerStats>();
    }

    //Actualizar animator

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        if (playerStats.playerIsDead == false)
        {
            //Animation Snapping
            float snappedHorizontal;
            float snappedVertical;

            #region Snapping Horizontal

            //Para que aunque el parametro sea float siempre se sepa qué ejecutar

            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                //Es walk

                snappedHorizontal = 0.5f;
            }
            else if (horizontalMovement > 0.55f)
            {
                //Es run

                snappedHorizontal = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                snappedHorizontal = -0.5f;
            }
            else if (horizontalMovement < -0.55f)
            {
                snappedHorizontal = -1;
            }
            else
            {
                snappedHorizontal = 0;
            }

            #endregion

            #region Snapped Vertical

            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
                snappedVertical = 0.5f;
            }
            else if (verticalMovement > 0.55f)
            {
                snappedVertical = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f)
            {
                snappedVertical = -0.5f;
            }
            else if (verticalMovement < -0.55f)
            {
                snappedVertical = -1;
            }
            else
            {
                snappedVertical = 0;
            }

            #endregion

            animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
            animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
        } else
        {

        }
    }

    //Reproducir animación adicional a las de movmiento básicas

    public void PlayTargetAnimation(string targetAnimation, bool isInteracting, bool useRootMotion = false)
    {
        if (playerStats.playerIsDead == false)
        {
            animator.SetBool("isInteracting", isInteracting);
            animator.CrossFade(targetAnimation, 0.2f);
            animator.SetBool("isUsingRootMotion", useRootMotion);
        }
        else
        {
            Debug.Log("Perdiste");
        }

    }

    private void OnAnimatorMove()
    {
        if(playerManager.isUsingRootMotion)
        {
            playerLocomotion.playerRigidbody.drag = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / Time.deltaTime;
            playerLocomotion.playerRigidbody.velocity = velocity;
        }
    }
}
