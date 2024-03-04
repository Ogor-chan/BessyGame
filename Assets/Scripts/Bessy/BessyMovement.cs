using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BessyMovement : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] GameObject BessySkin;
     Rigidbody2D bessyRigitbody;
    //Animator animator;
     Transform BessyRotation;
     CapsuleCollider2D BessyCollider;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private LayerMask walkableLayers;

    [Space(5)]
    [Header("Speed")]
    [SerializeField] private float moveSpeed = 7f;

    private enum MovementState {idle, running, jumping, falling, attack, dead, takeAHit }


    //Private things

    private float dirX;

    private void Awake()
    {
        bessyRigitbody = BessySkin.GetComponent<Rigidbody2D>();
        BessyRotation = BessySkin.GetComponent<Transform>();
        BessyCollider = BessySkin.GetComponent<CapsuleCollider2D>();
        
    }


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

        bessyRigitbody.velocity = new Vector2(dirX * moveSpeed, bessyRigitbody.velocity.y);
    }

    private void Jump()
    {
        bessyRigitbody.velocity = new Vector2(bessyRigitbody.velocity.x, jumpForce);
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            BessyRotation.rotation = Quaternion.Euler(0, 0, 0);
           // attackRotation.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            BessyRotation.rotation = Quaternion.Euler(0, 180f, 0);
           // attackRotation.rotation = Quaternion.Euler(0, 180f, 0);

        }
        else
        {
            state = MovementState.idle;

        }

        if(bessyRigitbody.velocity.y > .1f) 
        {
            state= MovementState.jumping;
        
        
        }
        else if(bessyRigitbody.velocity.y< -.1f)
        {
            state= MovementState.falling;
        
        }

        //animator.SetInteger("state", (int)state);
    }


    private bool IsGrounded()
    {
        return Physics2D.BoxCast(BessyCollider.bounds.center, BessyCollider.bounds.size, 0f, Vector2.down, .1f, walkableLayers);
    }
}
