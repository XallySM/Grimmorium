using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedModifier : MonoBehaviour
{
    PauseMenu pauseMenu;
    WinEvent winEvent;
    public GameObject pauseButton;
    public GameObject yesButton;

    private void Awake()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenu.pause == true)
        {
            EventSystem.current.firstSelectedGameObject = pauseButton;

        } else 
        {
            EventSystem.current.firstSelectedGameObject = yesButton;

        }     
    }
}
