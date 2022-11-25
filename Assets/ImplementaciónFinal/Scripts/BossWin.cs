using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossWin : MonoBehaviour
{
    EnemyStats enemystats;
    //private GameObject boss;

    // Start is called before the first frame update
    private void Awake()
    {
        enemystats = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStats>();

    }

    // Update is called once per frame
    void Update()
    {
        //Invoke("Ending", 0.6f);
        if (enemystats.currentHealth <= 0)
        {
            SceneManager.LoadScene("End");
        }
    }

    void Ending()
    {
        if (enemystats.currentHealth <= 0)
        {
            SceneManager.LoadScene("End");
        }
    }

}
