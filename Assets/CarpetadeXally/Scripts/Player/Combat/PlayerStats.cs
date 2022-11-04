using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;
  

    public bool playerIsDead;
    

    public HealthBar healthBar;

    AnimatorManager animatorManager;

    public Rigidbody playerRB;

    public PlayerManager playerManager;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerManager = GetComponent<PlayerManager>();
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


    }

    /*public void TakeHealth(int health)
    {
        currentHealth = currentHealth + health;

        healthBar.SetCurrentHealth(currentHealth);
    }
    */

}
