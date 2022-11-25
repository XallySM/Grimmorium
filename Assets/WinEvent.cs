using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinEvent : MonoBehaviour
{
    public GameObject winPanel;
    EspadaEscudo espadaEscudo;
    PlayerLocomotion playerLocomotion;
    bool noPressed;
    public int nextScene;

    public AudioSource DoorS;

    private void Awake()
    {
        espadaEscudo = FindObjectOfType<EspadaEscudo>();
        playerLocomotion = FindObjectOfType<PlayerLocomotion>();


    }

    private void OnTriggerEnter(Collider other)
    {

        DoorS.Play();

        if (noPressed == false)
        {
            winPanel.SetActive(true);
            espadaEscudo.isFreeze = true;
            playerLocomotion.cantJump = true;
        }
        else
        {
            return;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (noPressed == false)
        {
            winPanel.SetActive(true);
            espadaEscudo.isFreeze = true;
            playerLocomotion.cantJump = true;
        } else
        {
            return;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        winPanel.SetActive(false);
        playerLocomotion.cantJump = false;
        espadaEscudo.isFreeze = false;
        noPressed = false;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextScene);
    }


    public void NoButton()
    {
        winPanel.SetActive(false);
        noPressed = true;
    }

}
