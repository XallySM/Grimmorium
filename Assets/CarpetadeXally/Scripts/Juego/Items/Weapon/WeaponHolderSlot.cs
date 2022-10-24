using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class WeaponHolderSlot : MonoBehaviour
    {
        public Transform parentOverride;
        public bool isLeftHandSlot;
        //public bool isRightHandSlot;

        public GameObject currentWeaponModel;

        //Generar el modelo del arma

        public void UnloadWeapon()
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


        public void LoadWeaponModel(WeaponItem weaponItem)
        {
            UnloadWeaponAndDestroy();

            //Si no hay arma entonces no hay arma y no pasa nada 
            if (weaponItem == null)
            {
                UnloadWeapon();
                return;
            }

            //El transform del modelo del arma va a ser el asignado por este script 

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

                //Se determina la posición, rotación y escala del modelo del arma 

                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
            }

            currentWeaponModel = model;
        }
        
    }
