using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform agentTarget;

    [SerializeField] float PathCalculateRefreshRate = .25f;
    [SerializeField] float AttackDistance = 4.5f;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;

    CharacterController EnemyCont;



    float distanceToTarget;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        EnemyCont = GetComponent<CharacterController>();
    }

    private void Start()
    {
        StartCoroutine(UpdatePath());
    }
    void Update()
    {
        CalculateDistanceToTarget();
    }

    IEnumerator UpdatePath()
    {
        
        while (agentTarget != null)
        {
            agent.SetDestination(agentTarget.position);
            yield return new WaitForSeconds(PathCalculateRefreshRate);

        }
    }

    void CalculateDistanceToTarget()
    {
        distanceToTarget = Vector3.Distance(transform.position, agentTarget.position);
        //Debug.Log(distanceToTarget);

        /*if ( distanceToTarget <= AttackDistance)
        {
            StartCoroutine(DashAttack());
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.CompareTag("Player"))
        {
            StartCoroutine(DashAttack());
        }
    }

    IEnumerator DashAttack()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            EnemyCont.Move(transform.forward * dashSpeed * Time.deltaTime);

            

            yield return null;
        }

    }
}
