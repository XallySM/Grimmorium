using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponSlotManager : MonoBehaviour
{
    public WeaponHolderSlot leftHandSlot;
    //public WeaponHolderSlot rightHandSlot;

    DamageCollider leftHandDamageCollider;
    //DamageCollider rightHandDamageCollider;

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
        if (isLeft)
        {
            leftHandSlot.LoadWeaponModel(weaponItem);
            LoadLeftWeaponDamageCollider();

        }
        /*else 
        {
            rightHandSlot.LoadWeaponModel(weaponItem);
            Debug.Log("Esta en la derecha el modelo");
            LoadLeftWeaponDamageCollider();
        }
        */
    }

    #region Handle Weapon´s Damage Colliders

    private void LoadLeftWeaponDamageCollider()
    {

        leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

    }

    /*private void LoadRightWeaponDamageCollider()
    {

        rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

    }
    */

    public void OpenLeftHandDamageCollider()
    {
        leftHandDamageCollider.EnableDamageCollider();
    }

    /*public void OpenRightHandDamageCollider()
    {
        rightHandDamageCollider.EnableDamageCollider();
    }
    */

    public void CloseLeftHandDamageCollider()
    {
        leftHandDamageCollider.DisableDamageCollider();
    }

    /*public void CloseRightHandDamageCollider()
    {
        rightHandDamageCollider.DisableDamageCollider();
    }
    */

    #endregion

}


