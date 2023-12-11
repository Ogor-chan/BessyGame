using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMovement : MonoBehaviour
{
    [Header("Moving Inputs")]
    [SerializeField] KeyCode MoveRight = KeyCode.None;
    [SerializeField] KeyCode MoveLeft = KeyCode.None;
    [SerializeField] KeyCode MoveJump = KeyCode.None;
    [SerializeField] KeyCode Attack = KeyCode.None;


    [Header("Moving Actions")]
    public bool isGrounded;
    public bool isAttacking = false;


    [Header("Moving Controller")]
    public float acceleration;
    public float deceleration;
    public float topSpeed;
    public float jumpForce;

    private Rigidbody2D rb;
    public float horizontalInput;
    public Vector2 currentVelocity;

    public Transform groundCheck;
    public float groundcheckrad;
    public LayerMask[] groundLayer;


    public float attackRange = 1f;
    public LayerMask enemyLayer;


    [SerializeField] Animator animator;
    private SpriteRenderer SR;
    private GameObject CameraObject;

    private Dictionary<bool, Action> FlipFlop = new Dictionary<bool, Action>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        CameraObject = GameObject.Find("MainCamera");
    }

    void Update()
    {

        if (Input.GetKeyDown(Attack) && !isAttacking && isGrounded)
        {
            print("Attack!");
            animator.SetTrigger("Attack");
            rb.velocity = new Vector2(0, 0);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                Destroy(enemy.gameObject);
            }

            isAttacking = true;
            Invoke("ResetAttack", 0.5f);
        }
        if (!isAttacking)
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }



        foreach (int layer in groundLayer)
        {

            isGrounded = Physics2D.Raycast(gameObject.transform.position, Vector2.down,10f, layer);
           // isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundcheckrad, layer);
            // Do something with the 'isGrounded' result
            if(isGrounded) { break; }
        }

        if (isGrounded)
        {
            animator.SetBool("IsJumping", false);
            if (Input.GetKeyDown(MoveJump))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        else { animator.SetBool("IsJumping", true); }
    }

    void FixedUpdate()
    {
        currentVelocity = rb.velocity;

        float targetVelocity = horizontalInput * topSpeed;

        if (horizontalInput > 0)
        {
            SR.flipX = false;
            if (currentVelocity.x < targetVelocity && horizontalInput != 0)
            {
                currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, targetVelocity, acceleration * Time.fixedDeltaTime);
            }
        }
        else if (horizontalInput < 0)
        {
            SR.flipX = true;
            if (currentVelocity.x > targetVelocity && horizontalInput != 0)
            {
                currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, targetVelocity, acceleration * Time.fixedDeltaTime);
            }

        }
        else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            if (currentVelocity.x != 0)
            {

                currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
        }

        animator.SetFloat("Velocity", Mathf.Abs(currentVelocity.x));
        rb.velocity = currentVelocity;
        if (isAttacking)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    void ResetAttack()
    {
        animator.ResetTrigger("Attack");
        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CameraObject.GetComponent<CameraBehaviour>().PlayerCollision(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CameraObject.GetComponent<CameraBehaviour>().PlayerLeaveCollision(collision);
    }
}





