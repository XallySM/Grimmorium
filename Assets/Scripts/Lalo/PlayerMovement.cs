using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    CharacterController cont;
    void Awake()
    {
        cont = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
    }

    void playerMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 movePosition = new Vector3(x, 0, z);

        //transform.Translate(movePosition * speed * Time.deltaTime);
        cont.Move(movePosition * speed * Time.deltaTime);
        
    }
}
