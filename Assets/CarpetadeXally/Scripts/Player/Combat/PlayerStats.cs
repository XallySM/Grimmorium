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

    [Header("Audio Source")]
    public AudioSource RecoverS;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerManager = GetComponent<PlayerManager>();
        noDamage = false;
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

            healthBar.SetCurrentHealth(currentHealth);

            animatorManager.PlayTargetAnimation("Damage", true);
         
        }

        if (currentHealth <= 0)
        {

           currentHealth = 0;

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
            Destroy(collision.gameObject);

            if (currentHealth < maxHealth)
            {
                RecoverS.Play();
                currentHealth = maxHealth;
                healthBar.SetCurrentHealth(currentHealth);
            }
        }
    }

}
