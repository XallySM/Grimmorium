using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    Animator animator;

    [Header("Audio Source")]
    public AudioSource HitS;
    public AudioSource CryS;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;

        HitS = this.transform.Find("EnemySounds").transform.Find("HitS").GetComponent<AudioSource>();
        CryS = this.transform.Find("EnemySounds").transform.Find("CryS").GetComponent<AudioSource>();

    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        //Aquí la animación si el enemigo recibe daño

        HitS.Play();

        if (currentHealth <= 0)
        {
            CryS.Play();
            currentHealth = 0;
            Invoke("DelayAct", 2f);
        }

    }


    private void DelayAct()
    {
        
        //Aquí la animación de cuando se muere el enemigo

        Destroy(this.gameObject);
    }

}
