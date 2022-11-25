using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineM_CopyY : MonoBehaviour
{

    private GameObject Amy;

    public Transform PosObject;

    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        Amy = GameObject.Find("Amy_Model (Final)");
    }

    // Update is called once per frame
    void Update()
    {
        PosObject.position = new Vector3(this.transform.position.x, Amy.transform.position.y + 2.8f, Amy.transform.position.z - 16f);

        this.transform.position = Vector3.SmoothDamp(this.transform.position, PosObject.position, ref velocity, smoothTime);
    }
}
