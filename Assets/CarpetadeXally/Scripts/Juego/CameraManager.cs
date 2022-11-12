using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

    CinemachineVirtualCamera vcam;
    public Transform playercurrentTransform;
    

    private void Awake()
    {
        vcam = GetComponentInChildren<CinemachineVirtualCamera>();
        vcam.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            vcam.gameObject.SetActive(true);
            vcam.Follow = playercurrentTransform;
            vcam.LookAt = playercurrentTransform;
        }
    }

    private void OnTriggerExit(Collider collision)
    {

        if (collision.CompareTag("Jugador"))
        {
            vcam.gameObject.SetActive(false);
        }
    }

}
