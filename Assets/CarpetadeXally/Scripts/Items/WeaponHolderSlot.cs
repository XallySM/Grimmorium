using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolderSlot : MonoBehaviour
{
    public Transform parentOverride;
    public Transform parentOverrideShield;

    public bool isLeftHandSlot;
    //public bool isRightHandSlot;

    public GameObject currentWeaponModel;

    PlayerAttacker playerAttacker;

    private void Awake()
    {
        playerAttacker = GetComponent<PlayerAttacker>();
    }


    /*public void UnloadWeapon()
    {
        if (currentWeaponModel != null)
        {
            currentWeaponModel.SetActive(false);
        }
    }

    public void UnloadWeaponAndDestroy()
    {
        if (currentWeaponModel != null)
        {
            Destroy(currentWeaponModel);
        }
    }
    */


    public void LoadWeaponModel(WeaponItem weaponItem)
    {

        /*if (weaponItem == null)

        {
            Debug.Log("Nohaymodelo");
            UnloadWeapon();
            return;
        }
        */

        if (currentWeaponModel == null)
        {
            if (playerAttacker.isSword == true)
            {
                Debug.Log("VerEspada"); //No funciona
                GameObject model = Instantiate(weaponItem.modelPrefab) as GameObject;

                if (model != null)
                {
                    if (parentOverride != null)
                    {
                        model.transform.parent = parentOverride;
                    }
                    else
                    {
                        model.transform.parent = transform;
                    }

                    model.transform.localPosition = Vector3.zero;
                    model.transform.localRotation = Quaternion.identity;
                    model.transform.localScale = Vector3.one;

                    currentWeaponModel = model;

                }
            }
            else if (playerAttacker.isShield == true)
            {
                GameObject modelShield = Instantiate(weaponItem.modelPrefabShield) as GameObject;

                if (modelShield != null)
                {
                    if (parentOverrideShield != null)
                    {
                        modelShield.transform.parent = parentOverrideShield;
                    }
                    else
                    {
                        modelShield.transform.parent = transform;
                    }

                }


                modelShield.transform.localPosition = Vector3.zero;
                modelShield.transform.localRotation = Quaternion.identity;
                modelShield.transform.localScale = Vector3.one;

                currentWeaponModel = modelShield;
            }
            else
            {
                Debug.Log("SinArmas");
            }

        }
    }
}


