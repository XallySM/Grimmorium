using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeEnemy : MonoBehaviour
{
    public bool followsPlayer = false;
    public bool targetOnRange = false;
    public float minDistanceFromTarget = 5f;
    public float checkDistanceRefreshRate = 1f;

    private Material materialEye;

    public Transform ShootPivot;
    [SerializeField] float fireRate = 1f;

    public Transform target;

    private float fireTimer = 0.0f;


    Animator blinkAnim; // = playerTransform.gameObject.GetComponent<Animator>();

    public AudioSource ShootS;

    public EnemyStats enemyStats;

    private void Start()
    {
        /*GameObject stchild = transform.GetChild(0).gameObject;
        GameObject child = stchild.transform.GetChild(0).gameObject;
        materialEye = child.GetComponent<MeshRenderer>().sharedMaterial;

        materialEye.SetFloat("_WinkP", 1);
        */

        ShootS = this.transform.Find("EnemySounds").transform.Find("ShootS").GetComponent<AudioSource>();

        blinkAnim = gameObject.GetComponent<Animator>();

        enemyStats = gameObject.GetComponent<EnemyStats>();

        StartCoroutine(CheckDistance());

    }
    void Update()
    {

        FollowPlayer();
        if (fireTimer < fireRate + 1.0f)
        {
            fireTimer += Time.deltaTime;

        }
    }


    void FollowPlayer()
    {
        if (followsPlayer)
        {
            transform.LookAt(target);

        }
    }

    void CalculateDistanceFromTarget()
    {

        float distanceFromTarget = Vector3.Distance(transform.position, target.position);
        if (distanceFromTarget <= minDistanceFromTarget)
        {
            targetOnRange = true;

            if (fireTimer > fireRate)
            {
                blinkAnim.SetTrigger("Wink");
                StartCoroutine(Shoot());
                fireTimer = 0.0f;
            }

        }

        else
        {
            targetOnRange = false;
        }
    }

    IEnumerator Shoot()
    {


        GameObject bullet = ObjectPool.instance.GetPooledObject();

        if (bullet != null && enemyStats.currentHealth != 0)
        {
            


            Projectile projectile = bullet.GetComponent<Projectile>();
            projectile.fireOrigin = ShootPivot;
            projectile.fireDirection = projectile.fireOrigin.forward;
            bullet.transform.position = ShootPivot.position;
            bullet.SetActive(true);

            ShootS.Play();

            //InvokeRepeating("OpenEyeWink", 0f, WinkSpeed);
            


        }

        //yield return new WaitForSeconds(fireRate);
        yield return null;


    }

    IEnumerator CheckDistance()
    {
        while (target != null)
        {
            CalculateDistanceFromTarget();
            yield return new WaitForSeconds(checkDistanceRefreshRate);
        }

    }

    /*
    public float WinkSpeed = 0.3f;
    public bool OjoAbierto = true;
    float porcOjo = 1f;


    

    void OpenEyeWink()
    {
        print(porcOjo);

    if (OjoAbierto)
    {
            if (porcOjo >= 0.857)
            {
                

                porcOjo = porcOjo - 0.007f;

                //print(porcOjo);

                materialEye.SetFloat("_WinkP", porcOjo);
            }

        if (porcOjo <= 0.857)
        {
            OjoAbierto = false;
        }
    }
       
        if (OjoAbierto = false)
        {

                if (porcOjo <= 1)
                {
                    porcOjo = porcOjo + 0.007f;

                    print(OJO);

                    materialEye.SetFloat("_WinkP", porcOjo);
                }

            if (porcOjo >= 1)
            {
                OjoAbierto = true;
            }
        }
       
    }
    */


}
