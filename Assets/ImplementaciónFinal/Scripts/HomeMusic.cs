using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeMusic : MonoBehaviour
{

    private AudioSource Audio;

    // Start is called before the first frame update
    void Start()
    {
        Audio = this.GetComponent<AudioSource>();
        Audio.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Creditos()
    {
        DontDestroyOnLoad(this.gameObject);

    }

    public void Controles()
    {
        DontDestroyOnLoad(this.gameObject);
    }

  
}
