using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMirror : MonoBehaviour
{
    private Rigidbody2D body;
    private Transform transform_;

    [SerializeField] private float speed = 10.0f;

    [SerializeField] private LayerMask whatIsTrap;

    [SerializeField] private float trapCheckRadius;

    private PlayerController playerController;

    private bool mirrorHasTouchedTrap;

    private float movementInputDirection;
   
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        transform_ = GetComponent<Transform>();

        playerController = FindObjectOfType<PlayerController>();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        
        CheckSurroundings();
    }


    void Update()
    {
        CheckInput();

        if (mirrorHasTouchedTrap)
        {
            playerController.HasTouchedTrap = true;
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
    
    private void CheckSurroundings()
    {
        mirrorHasTouchedTrap = Physics2D.OverlapCircle(transform.position, trapCheckRadius, whatIsTrap);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, trapCheckRadius);
    }
}
