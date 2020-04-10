﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    private Transform transform_;

    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float trapCheckRadius;
    [SerializeField] private float checkpointCheckRadius;

    [SerializeField] private LayerMask whatIsTrap;
    [SerializeField] private LayerMask whatIsCheckpoint;

    private float movementInputDirection;
    private int baseGravity = 5;

    private Vector3 restarsPos;

    private bool isInDialog = false;
    private bool hasTouchedTrap;

    public bool HasTouchedTrap
    {
        get => hasTouchedTrap;
        set => hasTouchedTrap = value;
    }

    private bool hasTouchedCheckpoint;

    public bool IsInDialog
    {
        get => isInDialog;
        set => isInDialog = value;
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        transform_ = GetComponent<Transform>();

        restarsPos = transform.position;
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
            
            CheckSurroundings();
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
            
            ResetPlayer();
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

    private void ResetPlayer()
    {
        if (hasTouchedCheckpoint)
        {
            restarsPos = transform.position;
        }
        
        if (hasTouchedTrap)
        {
            transform.position = restarsPos;
            body.velocity = Vector2.zero;
            body.gravityScale = baseGravity;
        }
    }
    
    private void CheckSurroundings()
    {
        hasTouchedTrap = Physics2D.OverlapCircle(transform.position, trapCheckRadius, whatIsTrap);

        hasTouchedCheckpoint = Physics2D.OverlapCircle(transform.position, checkpointCheckRadius, whatIsCheckpoint);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, trapCheckRadius);
        
        Gizmos.DrawWireSphere(transform.position, checkpointCheckRadius);
    }
}
