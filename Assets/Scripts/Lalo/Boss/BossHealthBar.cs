using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    public HealthBar bossHealthBar;
    public EnemyStats bossStats;

    private void Awake()
    {
        bossStats = GetComponent<EnemyStats>();
    }
    void Start()
    {
        bossHealthBar.SetMaxHealth(bossStats.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        bossHealthBar.SetCurrentHealth(bossStats.currentHealth);
    }
}
