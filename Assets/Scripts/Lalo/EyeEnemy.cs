using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeEnemy : MonoBehaviour
{
    public bool followsPlayer = false;

   
    [SerializeField] Transform ShootPivot;
    [SerializeField] float fireRate = 1f;

    public Transform target;

    private void Start()
    {
        StartCoroutine(Shoot());
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

    IEnumerator Shoot()
    {
        while (followsPlayer)
        {
            GameObject bullet = ObjectPool.instance.GetPooledObject();

            if (bullet != null)
            {
                bullet.transform.position = ShootPivot.position;
                bullet.SetActive(true);
            }

            yield return new WaitForSeconds(fireRate);
        }
        
    }

    
}
