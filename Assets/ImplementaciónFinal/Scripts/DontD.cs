using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontD : MonoBehaviour
{

    public bool Activate = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Activate)
        {
            Actionate();
        }
    }

    private void Actionate()
    {
        transform.parent = null;

        DontDestroyOnLoad(this.gameObject);
    }

}
