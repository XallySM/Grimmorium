using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLoseLv2 : MonoBehaviour
{

    public void GameOver()
    {

        SceneManager.LoadScene("LoseP2");
    }
}
