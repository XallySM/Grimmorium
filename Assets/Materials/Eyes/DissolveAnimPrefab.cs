using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveAnimPrefab : MonoBehaviour
{

    Animator dissolveAnim;

    // Start is called before the first frame update
    void Start()
    {
        dissolveAnim = gameObject.GetComponent<Animator>();
        dissolveAnim.SetTrigger("Dissolve");
        Invoke("DelayAct", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DelayAct()
    {

        //Aquí la animación de cuando se muere el enemigo

        Destroy(this.gameObject);
    }
}
