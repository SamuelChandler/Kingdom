using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    private Vector2 movement;

    // Update is called once per frame
    void Update()
    {

        //get user movment input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


    }


    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (movement*moveSpeed*Time.fixedDeltaTime));
    }
}
