using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollider : MonoBehaviour
{
        Collider blockCollider;
        public GameObject colliderObject;

        private void Awake()
        {
            //blockCollider = GetComponentInChildren<Collider>();
            //blockCollider.gameObject.SetActive(true);
            //blockCollider.isTrigger = true;
            //blockCollider.enabled = false;
        }

        public void EnableBlockCollider()
        {
            colliderObject.SetActive(true);
            //blockCollider.enabled = true;
        }

        public void DisableBlockCollider()
        {
            colliderObject.SetActive(false);
            //blockCollider.enabled = false;
        }

}


