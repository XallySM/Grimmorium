using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject MenuPausa;
    public bool pause;

    private void Awake()
    {
        pause = false;
    }

    void Start()
    {
        MenuPausa.SetActive(false);
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetButton("Pause"))
        {
           Pausa();
           MenuPausa.SetActive(true);
           Debug.Log("Pausado");
           pause = true;
        }
    }


    public void Pausa()
    {
        Time.timeScale = 0f;
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        pause = false;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        pause = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        pause = false;

    }

}
