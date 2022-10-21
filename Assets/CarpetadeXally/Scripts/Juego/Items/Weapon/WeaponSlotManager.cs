using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class WeaponSlotManager : MonoBehaviour
    {
        public WeaponHolderSlot leftHandSlot;
        //public WeaponHolderSlot rightHandSlot;

        private void Awake()
        {
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                    Debug.Log("Esta en la izquierda");
                }
                /*else if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                    Debug.Log("Esta en la derecha");
                }
                */
            }

        }

        public void LoadWeponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if(isLeft)
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
                Debug.Log("Esta en la izquierda el modelo");

            }
            /*else 
            {
                rightHandSlot.LoadWeaponModel(weaponItem);
                Debug.Log("Esta en la derecha el modelo");
            }
            */
        }
    }

