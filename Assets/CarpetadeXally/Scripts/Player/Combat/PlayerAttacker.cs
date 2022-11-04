using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PlayerAttacker : MonoBehaviour
    {
        public bool isShield;
        public bool isSword;
        AnimatorManager animatorManager;
        PlayerInventory playerInventory;
        WeaponItem weaponItem;

       


    private void Awake()
        {
            animatorManager = GetComponent<AnimatorManager>();
           
        }

        public void HandleSwordAtack(WeaponItem weapon)
        {
            if (isSword == true)
            {
                animatorManager.PlayTargetAnimation(weapon.SwordAttack, true);
            }
            if (isShield == true)
            {
                animatorManager.PlayTargetAnimation(weapon.Shield, true);
            }
        }

        /*
        public void HandleHeavyAttack(WeaponItem weapon)
        {
            animatorManager.PlayTargetAnimation(weapon.Heavy_Atack, true);
          }
        */

    }

