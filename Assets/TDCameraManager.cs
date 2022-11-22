using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TDCameraManager : MonoBehaviour
{
    CinemachineVirtualCamera vcamTD;


    private void Awake()
    {
        vcamTD = GetComponentInChildren<CinemachineVirtualCamera>();
        
    }

    private void Update()
    {
        LockXZMove();
    }

    private void LockXZMove()
    {
        vcamTD.transform.position = new Vector3(-1.252836f, -0.751414f, 7.104036f);
        Debug.Log("Freeze");
    }
}
