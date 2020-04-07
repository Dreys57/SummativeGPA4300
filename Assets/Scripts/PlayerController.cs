using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    private Transform transform_;

    [SerializeField] private float speed = 10.0f;

    private float movementInputDirection;

    private bool isInDialog = false;

    public bool IsInDialog
    {
        get => isInDialog;
        set => isInDialog = value;
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        transform_ = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (isInDialog)
        {
            body.velocity= Vector2.zero;

            return;
        }
        else
        {
            ApplyMovement(); 
        }
    }


    void Update()
    {
        if (isInDialog)
        {
            return;
        }
        else
        {
            CheckInput();  
        }
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            ChangeGravity();
        }
    }

    private void ApplyMovement()
    {
        body.velocity = new Vector2(speed * movementInputDirection, body.velocity.y);
    }

    private void ChangeGravity()
    {
        body.gravityScale = -1 * body.gravityScale;
        
        FlipUpsideDown();
    }

    private void FlipUpsideDown()
    {
        transform.Rotate(180.0f, 0f, 0f);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform_.parent = other.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform_.parent = null;
        }
    }
}
