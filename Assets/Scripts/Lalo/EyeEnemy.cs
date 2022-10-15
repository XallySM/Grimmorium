using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeEnemy : MonoBehaviour
{
    public bool followsPlayer = false;
    public bool targetOnRange = false;
    public float minDistanceFromTarget = 5f; 
   
    public Transform ShootPivot;
    [SerializeField] float fireRate = 1f;

    public Transform target;

    private void Start()
    {
        
    }
    void Update()
    {
        StartCoroutine(Shoot());
        FollowPlayer();
        CalculateDistanceFromTarget();
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
        }

        else
        {
            targetOnRange = false;
        }
    }

    IEnumerator Shoot()
    {
        while (targetOnRange == true)
        {
            GameObject bullet = ObjectPool.instance.GetPooledObject();

            if (bullet != null)
            {
                Projectile projectile = bullet.GetComponent<Projectile>();
                projectile.fireOrigin = ShootPivot;
                bullet.transform.position = ShootPivot.position;
                bullet.SetActive(true);
                
            }

            yield return new WaitForSeconds(fireRate);
        }
        
    }

    
}
