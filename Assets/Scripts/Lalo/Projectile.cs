using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField] private float speed = 24f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float MinDistanceToEye = 10f;
    [SerializeField] private Transform fireOrigin;

    private void Awake()
    {

        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rb.velocity = fireOrigin.forward * speed;
        CalculateDistanceToEye();
    }

    private void CalculateDistanceToEye()
    {
        float distanceFromEye = Vector3.Distance(transform.position, fireOrigin.position);

        if (distanceFromEye >= MinDistanceToEye)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Environment") || collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
