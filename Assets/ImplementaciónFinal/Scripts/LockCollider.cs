using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCollider : MonoBehaviour
{
    public GameObject lockCollider;

    private void Awake()
    {
        lockCollider.SetActive(false);
    }

    /*private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            lockCollider.gameObject.SetActive(true);
        }
    }
    */

}
