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

    public GameObject PrefabDeath;

    private Transform Sphere;
    private GameObject Jugador;

    Animator dissolveAnim;

    private GameObject eyeinstance;

    public bool es_Ojo = true;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;

        HitS = this.transform.Find("EnemySounds").transform.Find("HitS").GetComponent<AudioSource>();
        Jugador = GameObject.FindGameObjectWithTag("Jugador");
        CryS = Jugador.transform.Find("EnemySounds").transform.Find("CryS").GetComponent<AudioSource>();
        dissolveAnim = gameObject.GetComponent<Animator>();

        if (es_Ojo)
        {
            Sphere = this.transform.Find("EyeGraphics").transform.Find("Sphere").GetComponent<Transform>();
        }

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

            Destroy(this.gameObject);
            //dissolveAnim.SetTrigger("Dissolve");
            currentHealth = 0;
            //Invoke("DelayAct", 2f);
            

            if (es_Ojo)
            {
                eyeinstance = Instantiate(PrefabDeath, Sphere.transform.position, Sphere.transform.rotation);
                eyeinstance.transform.localScale = Sphere.transform.localScale;
            }

        }

    }

    
    private void DelayAct()
    {
        
        //Aquí la animación de cuando se muere el enemigo

        Destroy(this.gameObject);
    }

}
