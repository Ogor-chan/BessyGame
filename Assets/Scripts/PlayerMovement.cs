using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float acceleration;
    public float deceleration;
    public float topSpeed;
    public float jumpForce;

    private Rigidbody2D rb;
    public float horizontalInput;
    public Vector2 currentVelocity;

    public Transform groundCheck;
    public float groundcheckrad;
    public LayerMask groundLayer;
    public bool isGrounded;

    public float attackRange = 1f;
    public LayerMask enemyLayer;
    private bool isAttacking = false;

    [SerializeField] Animator animator;
    private SpriteRenderer SR;
    private GameObject CameraObject;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        CameraObject = GameObject.Find("MainCamera");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isAttacking)
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



        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundcheckrad, groundLayer);
        if (isGrounded)
        {
            animator.SetBool("IsJumping", false);
            if (Input.GetKeyDown(KeyCode.Space))
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





