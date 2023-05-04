using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalMove = 0f;
    public float runSpeed = 40f;

    private CharacterController2D Controller;

    public void Start()
    {
        Controller = GetComponent<CharacterController2D>();
    }

    public void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump") && Controller.IsGrounded())
        {
            Controller.Jump();
        }

        Controller.Move(horizontalMove * Time.fixedDeltaTime);
    }
}

