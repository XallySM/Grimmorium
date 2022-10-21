using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(menuName = "Items/Weapon Item")]

    public class WeaponItem : Item
    {
        public GameObject modelPrefab;  //Recibir modelo de arma
        //public bool isUnarmed;

        [Header("Sword Attack")]
        public string SwordAttack;
        //public string Heavy_Atack;
    }



