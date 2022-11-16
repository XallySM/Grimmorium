using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EspadaEscudo : MonoBehaviour
{
    public GameObject currentModel;

    public bool isSword;

    public bool isShield;

    public GameObject modelShield;

    public GameObject modelSword;

    PlayerControls playerControls;

    public bool swordAttack_Input;

    public bool isFreeze;

    AnimatorManager animatorManager;

    //public bool block_Input;

    DamageCollider swordDamageCollider;

    BlockCollider shieldBlockCollider;

    PlayerManager playerManager;

    Animator animator;

    Rigidbody playerRB;

    PlayerStats playerStats;

    private bool isBlocking;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerManager = GetComponent<PlayerManager>();
        animator = GetComponent<Animator>();
        playerRB= GetComponent<Rigidbody>();
        playerStats = GetComponent<PlayerStats>();
        isFreeze = false;
    }


    private void Update()
    {

        //isBlocking = playerControls.PlayerActions.Block.ReadValue<float>() > 0;
        //HandleSwordShieldInput();

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("GatilloDerecho"))
        {
            //Debug.Log("TeclaDown");
            animatorManager.PlayTargetAnimation("ShieldStart", true);
            //HandleBlock();
            //isBlocking = false;
            isShield = true;
            isSword = false;
            LoadWeaponModel();
            playerRB.constraints = RigidbodyConstraints.FreezePosition;
            playerRB.constraints = RigidbodyConstraints.FreezeRotation;
        }

        if (Input.GetKey(KeyCode.Z) || Input.GetButton("GatilloDerecho"))
        {
            //Debug.Log("Tecla");
            animator.SetBool("ShieldActive", true);
            OpenShieldBlockCollider();
            playerRB.constraints = RigidbodyConstraints.FreezePosition;
            playerRB.constraints = RigidbodyConstraints.FreezeRotation;
        }

        if (Input.GetKeyUp(KeyCode.Z) || Input.GetButtonUp("GatilloDerecho"))
        {
            //Debug.Log("TeclaUp");
            CloseShieldBlockCollider();
            animator.SetBool("ShieldActive", false);
        }

        CantMove();
    }
    

    #region Abrir y cerrar el damage collider de la espada
    private void LoadSwordDamageCollider()
    {
        swordDamageCollider = GetComponentInChildren<DamageCollider>();
    }

    private void OpenSwordDamageCollider()
    {
        swordDamageCollider.EnableDamageCollider();
        Debug.Log("open");
    }

    private void CloseSwordDamageCollider()
    {
        swordDamageCollider.DisableDamageCollider();
        Debug.Log("close");
    }
    #endregion


    #region Abrir y cerrar el damage collider del escudo
    private void LoadShieldBlockCollider()
    {
        shieldBlockCollider = GetComponentInChildren<BlockCollider>();
    }

    private void OpenShieldBlockCollider()
    {
        shieldBlockCollider.EnableBlockCollider();
        //Debug.Log("open");
    }

    private void CloseShieldBlockCollider()
    {
        shieldBlockCollider.DisableBlockCollider();
        //Debug.Log("close");
    }
    #endregion


    private void LoadWeaponModel()
    {
        if (isSword == true)
        {
            isShield = false;
            Debug.Log("Sword");
            

            if (currentModel==modelShield)
            {
                currentModel = modelSword;
                modelSword.SetActive(true);
                LoadSwordDamageCollider();
                modelShield.SetActive(false);
                //currentModel = Instantiate(modelSword) as GameObject;
            }else
                currentModel = modelSword;
                modelSword.SetActive(true);
                LoadSwordDamageCollider();
               //currentModel = Instantiate(modelSword) as GameObject;
        }

        if (isShield == true)
        {
            isSword = false;
            Debug.Log("Shield");

            if (currentModel == modelSword)
            {
                currentModel = modelShield;
                modelShield.SetActive(true);
                LoadShieldBlockCollider();
                modelSword.SetActive(false);
                //currentModel = Instantiate(modelSword) as GameObject;
            }
            else
                currentModel = modelShield;
                modelShield.SetActive(true);
                LoadShieldBlockCollider();
                //currentModel = Instantiate(modelSword) as GameObject;
        }

        if (currentModel == null)
        {

            Debug.Log("Unarmed");
           
        }
        
    }
   

    private void OnEnable()
    {
        if (playerControls == null)     //Si no se está haciendo nada
        {
            playerControls = new PlayerControls();  //Entonces hace algo

            //Llama a la clase script del input sistem, de ahí al mapa de acción del player, de ahí la acción de movement y lee los valores en x y y usando un vector 2

            playerControls.PlayerActions.SwordAttack.performed += i => swordAttack_Input = true;

           



            //playerControls.PlayerActions.Block.performed += i => swordAttack_Input = true;
            //playerControls.PlayerActions.Block.canceled += i => block_Input = false;
        }

        playerControls.Enable();
    }

    /*public void HandleSwordShieldInput()
    {
        if (swordAttack_Input == true)
        {
            animatorManager.PlayTargetAnimation("SwordAttack", true);
            swordAttack_Input = false;
            isSword = true;
            isShield = false;
            LoadWeaponModel();  
            
        }

        if (isBlocking == true)
        {

            Debug.Log("IsBlock");
            HandleBlock();
            isBlocking = false;
            isShield = true;
            isSword = false;
            LoadWeaponModel();
            
        }
        else
        {
            isBlocking = false;
        }
    }
    */

    private void FixedUpdate()
    {
        if (swordAttack_Input == true)
        {
            animatorManager.PlayTargetAnimation("SwordAttack", true);
            playerStats.noDamage = true;
            swordAttack_Input = false;
            isSword = true;
            isShield = false;
            LoadWeaponModel();   

        }

        /*if (isBlocking == true)
        {
            
            Debug.Log("IsBlock");
            //HandleBlock();
            //isBlocking = false;
            isShield = true;
            isSword = false;
            LoadWeaponModel();
            animatorManager.PlayTargetAnimation("ShieldStart", true);
        } else
        {

        /*}
        
        
       

        /*else
        {
            isBlocking = false;
        }
        */
    }

    public void Freeze()
    {
        isFreeze = true;
    }

    public void UnFreeze()
    {
        isFreeze = false;
    }

    public void CantMove()
    {
        if (isFreeze == true)
        {
            playerRB.constraints = RigidbodyConstraints.FreezePosition;
            playerRB.constraints = RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            return;
        }
    }

    /*public void HandleBlock()
    {
        if (playerManager.isInteracting)
            return;

        if (isBlocking)
            return;
        

        
        isBlocking = true;
    }
    */

}
