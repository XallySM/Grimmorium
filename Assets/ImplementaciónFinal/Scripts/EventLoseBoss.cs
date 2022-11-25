using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLoseBoss : MonoBehaviour
{
    public void GameOver()
    {

        SceneManager.LoadScene("LoseBoss");
    }
}
