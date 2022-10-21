using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PlayerInventory : MonoBehaviour
    {
        WeaponSlotManager weaponSlotManager;

        //public WeaponItem rihtWeapon;
        public WeaponItem leftWeapon;

        private void Awake()
        {
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        }

        private void Start()
        {
            //weaponSlotManager.LoadWeponOnSlot(rihtWeapon, false);
            weaponSlotManager.LoadWeponOnSlot(leftWeapon, true);
        }

    }

