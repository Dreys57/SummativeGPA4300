using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    private Transform transform_;
    private Animator animator;

    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float trapCheckRadius;
    [SerializeField] private float checkpointCheckRadius;

    [SerializeField] private LayerMask whatIsTrap;
    [SerializeField] private LayerMask whatIsCheckpoint;

    private float movementInputDirection;
    private float velocityLimitUp = 10.0f;
    private float maxVelocityUp = 8.0f;
    private float velocityLimitDown = -10.0f;
    private float maxVelocityDown = -8.0f;
    
    
    private int baseGravity = 5;
    
    private Vector3 restartPos;

    private bool isInDialog = false;
    private bool hasTouchedTrap;
    private bool hasTouchedCheckpoint;
    private bool isWalking;
    private bool isFacingRight= true;
    
    public Vector3 RestartPos
    {
        get => restartPos;
        set => restartPos = value;
    }

    public bool HasTouchedTrap
    {
        get => hasTouchedTrap;
        set => hasTouchedTrap = value;
    }

    public bool IsInDialog
    {
        get => isInDialog;
        set => isInDialog = value;
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        transform_ = GetComponent<Transform>();
        animator = GetComponent<Animator>();

        restartPos = transform.position;
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
            isWalking = false;

            UpdateAnimations();
            return;
        }
        else
        {
            CheckInput();
            
            CheckMovementDirection();
            
            UpdateAnimations();
            
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

        if (body.velocity.y > velocityLimitUp)
        {
            body.velocity = new Vector2(body.velocity.x, maxVelocityUp);
        }
        
        if (body.velocity.y < velocityLimitDown)
        {
            body.velocity = new Vector2(body.velocity.x, maxVelocityDown);
        }
    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        if (Mathf.Abs(body.velocity.x) >= 0.1f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
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
            restartPos = transform.position;
        }
        
        if (hasTouchedTrap)
        {
            transform.position = restartPos;
            body.velocity = Vector2.zero;
            body.gravityScale = baseGravity;
        }
    }

    private void UpdateAnimations()
    {
        animator.SetBool("isWalking",isWalking);
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
