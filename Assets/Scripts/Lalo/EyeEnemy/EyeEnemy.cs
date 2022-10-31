using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeEnemy : MonoBehaviour
{
    public bool followsPlayer = false;
    public bool targetOnRange = false;
    public float minDistanceFromTarget = 5f;
    public float checkDistanceRefreshRate = 1f;
   
    public Transform ShootPivot;
    [SerializeField] float fireRate = 1f;

    public Transform target;

    private float fireTimer = 0.0f;

    private void Start()
    {
        
        StartCoroutine(CheckDistance());

    }
    void Update()
    {
        
        FollowPlayer();
        if(fireTimer < fireRate + 1.0f)
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
        if(distanceFromTarget <= minDistanceFromTarget)
        {
            targetOnRange = true;

            if(fireTimer > fireRate)
            {
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

            if (bullet != null)
            {
                Projectile projectile = bullet.GetComponent<Projectile>();
                projectile.fireOrigin = ShootPivot;
                projectile.fireDirection = projectile.fireOrigin.forward;
                bullet.transform.position = ShootPivot.position;
                bullet.SetActive(true);

                
            }

        //yield return new WaitForSeconds(fireRate);
        yield return null;


    }

    IEnumerator CheckDistance()
    {
        while(target != null)
        {
            CalculateDistanceFromTarget();
            yield return new WaitForSeconds(checkDistanceRefreshRate);
        }
        
    }
    
}
