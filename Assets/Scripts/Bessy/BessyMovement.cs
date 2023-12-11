using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BessyMovement : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] Transform attackRotation;
    [SerializeField] Transform BessyRotation;
    [SerializeField] BoxCollider2D BessyCollider;

    [Header("Player Movement")]
    [Header("Jump")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private LayerMask walkableLayers;

    [Space(5)]
    [Header("Speed")]
    [SerializeField] private float moveSpeed = 7f;

    private enum MovementState {idle, running, jumping, falling, attack, dead, takeAHit }


    //Private things

    private float dirX;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if(Input.GetKeyDown("space") && IsGrounded())
        {
            Jump();
        }

        UpdateAnimationState();

        
    }

    private void Move()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            BessyRotation.rotation = Quaternion.Euler(0, 0, 0);
            attackRotation.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            BessyRotation.rotation = Quaternion.Euler(0, 180f, 0);
            attackRotation.rotation = Quaternion.Euler(0, 180f, 0);

        }
        else
        {
            state = MovementState.idle;

        }

        if(rb.velocity.y > .1f) 
        {
            state= MovementState.jumping;
        
        
        }
        else if(rb.velocity.y< -.1f)
        {
            state= MovementState.falling;
        
        }

        animator.SetInteger("state", (int)state);
    }


    private bool IsGrounded()
    {
        return Physics2D.BoxCast(BessyCollider.bounds.center, BessyCollider.bounds.size, 0f, Vector2.down, .1f, walkableLayers);
    }
}
