using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject MenuPausa;

    void Start()
    {

        MenuPausa.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pausa()
    {
        Time.timeScale = 0f;
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
