using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float rotationSpeed;
    Transform target;
    public string searchedTag;
    public float radius;
    public LayerMask LM;
    EnemyTarget enemyTarget;

    private void Awake()
    {
        enemyTarget = FindObjectOfType<EnemyTarget>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.T) || Input.GetButton("HombroDerecho"))
        {
            enemyTarget.ActivateTarget();
            Collider[] C = Physics.OverlapSphere(transform.position, radius, LM);
            float D = 500;
            for (int i = 0; i < C.Length; i++)
            {
                float N = Vector3.Distance(transform.position, C[i].transform.position);
                if(N<D)
                {
                    D = N;
                    target = C[i].transform;
                }
            }

            if (target)
            {
                Quaternion Rot = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, Rot, Time.deltaTime * rotationSpeed);
                LimitRotation();
                enemyTarget.ActivateTarget();
                //transform.Rotate(0, rotationSpeed, 0);
            }

        } else if (Input.GetKeyUp(KeyCode.T) || Input.GetButtonUp("HombroDerecho"))
        {
            enemyTarget.DesactivateTarget();
        }


    }

    private void LimitRotation()
    {
        Vector3 playerEulerAngles = transform.rotation.eulerAngles;
        playerEulerAngles.x = 0f;
        playerEulerAngles.z = 0f;
        transform.rotation = Quaternion.Euler(playerEulerAngles);
    }
}
