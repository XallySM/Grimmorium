using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    public bool playerIsDead;

    public bool noDamage;

    public HealthBar healthBar;

    AnimatorManager animatorManager;

    public Rigidbody playerRB;

    public PlayerManager playerManager;

    PlayerLocomotion playerLocomotion;

    [Header("Audio Source")]
    public AudioSource RecoverS;
    public AudioSource TakeDamageS;
    public AudioSource RealDeathS;
    public AudioSource GameOverS;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerManager = GetComponent<PlayerManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        noDamage = false;

        RecoverS = this.transform.Find("AmySounds").transform.Find("Recover").GetComponent<AudioSource>();
        TakeDamageS = this.transform.Find("AmySounds").transform.Find("TakeDamage").GetComponent<AudioSource>();
        RealDeathS = this.transform.Find("AmySounds").transform.Find("RealDeathS").GetComponent<AudioSource>();
        GameOverS = this.transform.Find("AmySounds").transform.Find("GameOverS").GetComponent<AudioSource>();

    }

    void Start()
    {
        maxHealth = SetMaxhealthFromHealthLevel();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerIsDead = false;
     
    }

    private int SetMaxhealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (playerIsDead)
            return;

        if (playerManager.isInvulnerablePlayer == false)
        {

            currentHealth = currentHealth - damage;

            TakeDamageS.Play();

            healthBar.SetCurrentHealth(currentHealth);

            animatorManager.PlayTargetAnimation("Damage", true);
         
        }

        if (currentHealth <= 0)
        {
            TakeDamageS.Stop();
           currentHealth = 0;

            RealDeathS.Play();

            GameOverS.Play();

            Dontd = GameObject.Find("GameOverS").GetComponent<DontD>();
            Dontd.Activate = true;

            animatorManager.PlayTargetAnimation("Dead", true);

            playerRB.constraints = RigidbodyConstraints.FreezePosition;
            playerRB.constraints = RigidbodyConstraints.FreezeRotation;

            playerIsDead = true;
            //Aquí lo que pasa cuando muere el jugador
        }

        if (noDamage == true)
        {
            return;
        }

    }

    private DontD Dontd;

    public void NoDamage()
    {
        noDamage = true;
    }

    public void Damage()
    {
        noDamage = false;
    }

    /*public void TakeHealth(int health)
    {
        currentHealth = currentHealth + health;

        healthBar.SetCurrentHealth(currentHealth);
    }
    */

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "DeadCollider")
        {
            SceneManager.LoadScene("LoseP1");
        }

        if (collision.tag == "Apple")
        {
            RecoverS.Play();

            Destroy(collision.gameObject);

            if (currentHealth < maxHealth)
            {
                
                currentHealth = maxHealth;
                healthBar.SetCurrentHealth(currentHealth);
            }
        }
    }

}
