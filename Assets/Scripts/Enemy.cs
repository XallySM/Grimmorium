using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform agentTarget;

    [SerializeField] float PathCalculateRefreshRate = .25f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(UpdatePath());
    }
    void Update()
    {
        
    }

    IEnumerator UpdatePath()
    {
        
        while (agentTarget != null)
        {
            agent.SetDestination(agentTarget.position);
            yield return new WaitForSeconds(PathCalculateRefreshRate);

        }
    }

}
