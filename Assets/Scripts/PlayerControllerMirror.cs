using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMirror : MonoBehaviour
{
    private Rigidbody2D body;

    [SerializeField] private float speed = 10.0f;

    private float movementInputDirection;
   
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }


    void Update()
    {
        CheckInput();
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
}
