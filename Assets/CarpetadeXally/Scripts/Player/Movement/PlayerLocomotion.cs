using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    AnimatorManager animatorManager; //Referencia a animator manager
    PlayerManager playerManager; //Referencia a player manager
    InputManager inputManager; //Referencia a input manager
    Vector3 moveDirection;     //Crear vector moveDirection
    Transform cameraObject;    //Para transform del player
    public Rigidbody playerRigidbody; //Para rigidbody del player
    PlayerStats playerStats;


    [Header("Falling")]
    public float inAirTimer;   //Tiempo que pasa en el aire
    public float leapingVelocity;
    public float fallingVelocity;
    public float raycastHeighOffset = 0.5f; //Float para la distancia a la que debe estar del suelo el jugador
    public LayerMask groundLayer;
    public float maxDistance = 1;

    [Header("Movement Flags")]
    public bool isGrounded;     //Detectar si está grounded
    public bool isJumping;      //Detectar si está saltando

    [Header("Movement Speeds")]
    public float runningSpeed = 7;  //Velocidad de movmiento
    public float walkingSpeed = 1.5f; //Velocidad al caminar
    public float rotationSpeed = 15; //Velocidad de rotación

    [Header("Jump Speeds")]
    public float jumpHeight = 5;
    public float gravityIntensity = -15;
   

    public void Awake()
    {
        inputManager = GetComponent<InputManager>();    //Accede a input manager
        playerRigidbody = GetComponent<Rigidbody>();    //Accede al rigidbody del player
        cameraObject = Camera.main.transform;           //Acceder a la cámara
        animatorManager = GetComponent<AnimatorManager>(); //Acceder al animator manager
        playerManager = GetComponent<PlayerManager>(); //Acceder al player manager
        playerStats = GetComponent<PlayerStats>();
       
    }

    public void HandleAllMovement()
    {
        if (playerStats.playerIsDead == false)
        {
            HandleFallingAndLanding();

            if (playerManager.isInteracting)
                return;

            HandleMovement();
            HandleRotation();
        }
        else
        {
            
        }
         
    }

    private void HandleMovement()
    {
        
        //La dirección de movimiento vertical depende de a donde ve la cámara

        moveDirection = cameraObject.forward * inputManager.verticalInput;

        //La dirección de movimiento horizontal depende del vector de dirección de movimiento y del eje horizontal

        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;

        //Normalizar el vector de movimiento

        moveDirection.Normalize();

        //Para que el personaje no se separe del suelo 

        moveDirection.y = 0;

        //Si estás corriendo agarra la velocidad paa correr
        //Si estás caminando agarra la velocidad para caminar


        //El vector de movimiento se multiplica por la velocidad de movimiento
        if (inputManager.moveAmount >= 0.5f)
        {
            moveDirection = moveDirection * runningSpeed;
        }
        else
        {
            moveDirection = moveDirection * walkingSpeed;
        }
        

        //El vector de velocidad de movimiento es igual al de la dirección de movimiento

        Vector3 movementVelocity = moveDirection;

        //La velocidad es igual al vector creado de movementVelocity

        if (!isGrounded && !isJumping)
        {
            Debug.Log("Semueve");
            movementVelocity = moveDirection;
            playerRigidbody.velocity = new Vector3(movementVelocity.x, playerRigidbody.velocity.y, movementVelocity.z);
        }
        else if (isGrounded && !isJumping)
        {
            Debug.Log("Semueve2");
            movementVelocity = moveDirection;
            playerRigidbody.velocity = new Vector3(movementVelocity.x, playerRigidbody.velocity.y, movementVelocity.z);
        }
        else if (!isGrounded && isJumping)
        {
            Debug.Log("Semueve2");
            movementVelocity = moveDirection;
            playerRigidbody.velocity = new Vector3(movementVelocity.x, playerRigidbody.velocity.y, movementVelocity.z);
        }
        
    }

    private void HandleRotation()
    {

       
        //Crear vector para rotar

        Vector3 targetDirection = Vector3.zero;

        //Rotar en eje vertical

        targetDirection = cameraObject.forward * inputManager.verticalInput;

        //Rotar en eje horizontal 

        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;

        //Normalizar vector de rotación

        targetDirection.Normalize();

        //No rotar en vertical

        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;
        
        //Quaternions para rotar

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    //Gestionar caída y aterrizaje

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + raycastHeighOffset; //Localizar el raycast de acuerdo a la distancia que esté el player del suelo 


        //Si está cayendo...

        if (!isGrounded && !isJumping)
        {
            if(!playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Falling", true);
            }

            animatorManager.animator.SetBool("isUsingRootMotion", false);
            inAirTimer = inAirTimer + Time.deltaTime;  //El tiempo en el aire crece con los frames que pasas en el aire
            playerRigidbody.AddForce(transform.forward * leapingVelocity); //Pequeño boost al saltar
            playerRigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer); //Fuerza al caer, es más rápido conforme más tiempo caes
        }

        //Solo detecta lo que esté en la máscara de capa de grounded

        if (Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, maxDistance, groundLayer))
        {
            
            //Si está cayendo puede aterrizar

            if (!isGrounded && playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Land", true);
            }

            //Reiniciar tiempo en el aire y la bool de grounded

            inAirTimer = 0;
            isGrounded = true;
        }

        else //Si no has aterrizado estás en el aire
        {
            isGrounded = false;
        }


    }

    //Función para brincar

    public void HandleJumping()
    {
        //Si está grounded puede brincar

        if (isGrounded)
        {
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jump", true);

         //Velocidad del salto

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            playerRigidbody.velocity = playerVelocity;
        }       

    }

    public void HandleDodge()
    {
        //Para no poder rodar si estás en el aire
        if (playerManager.isInteracting)
            return;
        animatorManager.PlayTargetAnimation("Dodge", true, true);

        //Invulnerabilidad a los golpes durante la animación de dodge    

    }
}

