using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon Item")]
public class WeaponItem : Item
{
    public GameObject modelPrefab;
    public GameObject modelPrefabShield;
    

    [Header("Sword Atack")]
    public string SwordAttack;

    [Header("Shield")]
    public string Shield;
}
