using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

    public class InputManager : MonoBehaviour
    {
        PlayerControls playerControls;   //Llamar a los inputs del script PlayerControls (de actions)

        PlayerLocomotion playerLocomotion; //Llamar a player locomotion

        public Vector2 movementInput;           //Vector de movimiento

        public float moveAmount;         //Cantidad de movimiento

        public float verticalInput;      //Float de eje vertical

        public float horizontalInput;    //Float de eje horizontal

        AnimatorManager animatorManager; //Referencia al animator  manager

        PlayerAttacker playerAttacker; //Referencia al player atacker

        PlayerInventory playerInventory; //Referencia al player inventory

        PlayerStats playerStats;

        //Jump
        public bool jump_Input;

        //Dodge
        public bool dodge_Input;

        //Sword Atack
        public bool swordAttack_input;
        //public bool heavyAttack_input;

        private void Awake()
        {
            playerLocomotion = GetComponent<PlayerLocomotion>(); //Acceder al player locomotion
            animatorManager = GetComponent<AnimatorManager>(); //Acceder al animator manager
            playerAttacker = GetComponent<PlayerAttacker>();   //Acceder al player attacker
            playerInventory = GetComponentInParent<PlayerInventory>(); //Acceder al player inventory
            playerStats = GetComponent<PlayerStats>();
        }


        //Función para habilitar controles 

        private void OnEnable()
        {
       
            if (playerControls == null)     //Si no se está haciendo nada
            {
                playerControls = new PlayerControls();  //Entonces hace algo

                //Llama a la clase script del input sistem, de ahí al mapa de acción del player, de ahí la acción de movement y lee los valores en x y y usando un vector 2

                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

                playerControls.PlayerActions.Jump.performed += i => jump_Input = true;

                playerControls.PlayerActions.Dodge.performed += i => dodge_Input = true;

                playerControls.PlayerActions.SwordAttack.performed += i => swordAttack_input = true;
            }

            playerControls.Enable();
        }
    

        //Función para deshabilitar controles

        private void OnDisable()
        {
            playerControls.Disable();
        }

        public void HandleAllInputs()
        {
        if (playerStats.playerIsDead == false)
        {
            HandleMovementInput();
            HandleJumpingInput();
            HandleDodgeInput();
            HandleSwordAttackInput();
        }else
        {
            OnDisable();
        }

      
        
        }

        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;    //Moverse en y es el eje vertical
            horizontalInput = movementInput.x;  //Moverse en x es el eje horizontal

            //Convertir siempre el movimiento en un valor absoluto
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

            animatorManager.UpdateAnimatorValues(0, moveAmount);
        }

        private void HandleJumpingInput()
        {

            if (jump_Input == true)
            {
                jump_Input = false;
                playerLocomotion.HandleJumping();
            }
        }

        private void HandleDodgeInput()
        {
            if (dodge_Input == true)
            {
                dodge_Input = false;
                playerLocomotion.HandleDodge();
            }
        }

        private void HandleSwordAttackInput()
        {
            
            //playerControls.PlayerActions.HeavyAttack.performed += i => heavyAttack_input = true;

            if (swordAttack_input)
            {
                
                playerAttacker.HandleSwordAtack(playerInventory.leftWeapon);
            }

            /*if (heavyAtack_input)
             {
                playerAttacker.HandleHeavyAtack(playerInventory.leftWeapon);
             }
            */
        }

    }

