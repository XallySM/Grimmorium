using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorManager animatorManager;

        private void Awake()
        {
            animatorManager = GetComponent<AnimatorManager>();
        }

        public void HandleSwordAtack(WeaponItem weapon)
        {
            animatorManager.PlayTargetAnimation(weapon.SwordAttack, true);
        }

        /*
        public void HandleHeavyAttack(WeaponItem weapon)
        {
            animatorManager.PlayTargetAnimation(weapon.Heavy_Atack, true);
        }
        */

    }

