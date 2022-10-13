using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeEnemy : MonoBehaviour
{
    public bool followsPlayer = false;
    
    public Transform target;
    
    void Update()
    {
        if (followsPlayer)
        {
            transform.LookAt(target);
        }
        
    }
}
