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

    private void Start()
    {
        
        StartCoroutine(CheckDistance());

    }
    void Update()
    {
        
        FollowPlayer();
        
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
            StartCoroutine(Shoot());
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

            yield return new WaitForSeconds(fireRate);
        
        
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
