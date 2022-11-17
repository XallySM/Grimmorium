using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject player;
    public GameObject platform;
    public PlayerLocomotion playerLocomotion;

    public void SetParent()
    {
        player.transform.parent = platform.transform;
        playerLocomotion = GetComponentInChildren<PlayerLocomotion>();
    }

    public void SetOffParent()
    {
        player.transform.parent = null;
    }
    

    private void OnCollisionStay(Collision collision)
    {
        SetParent();
        playerLocomotion.isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        SetOffParent();
    }
}
