using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WaitUntillDetectPlayer : MonoBehaviour
{
    public enum State { Wait, DetectPlayer, PlayerInRange, PlayerOutOfRange }
   
    [Header("Enemy actual State")]
    public State currentState = State.Wait;
    [SerializeField] bool attacking;

    [Header("Detecting by trigger")]
    public bool detectingByCircleTrigger;
   // [SerializeField] CircleCollider2D circleCollider;
    [SerializeField] GameObject circleTriggerRange;

  // [Header("Detecting by circlecast")]
  // public bool detectingByCircleCast;
  // [Space(15)]
  // public bool detectRight;
  // public bool detectLeft;
  // public bool detectUp;
  // public bool detectDown;


    public LayerMask BessyLayer;
    public LayerMask GroundLayer;
    public GameObject enemySkin;
    Rigidbody2D rb;
    [SerializeField] CircleCollider2D circleCollider;

    [Header("Attack Events")]
    [Space]
    [SerializeField] UnityEvent StartAttack;
    [SerializeField] UnityEvent StopAttack;



    private void Awake()
    {
        circleTriggerRange.SetActive(false);
        rb = enemySkin.GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if (currentState == State.Wait)
            DetectPlayerByRaycast();
        if (currentState != State.Wait)
            IsPlayerInRange();

    }

    float oldValue;
    float currValue;

    float distanceToClosestCollider;
    private void DetectPlayerByRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(enemySkin.transform.position, -Vector2.up, Mathf.Infinity, BessyLayer);
        oldValue = enemySkin.transform.position.x;

        if (hit)
        {
            
            Debug.Log("Player detected by " + gameObject.name);
            rb.bodyType = RigidbodyType2D.Dynamic;

            
            if (currValue != oldValue) 
            {
                
            }
            else
            {
                currentState = State.DetectPlayer;
 
            }
            currValue = enemySkin.transform.position.x;
            
        }

        

    }




    private void IsPlayerInRange()
    {
        circleTriggerRange.SetActive(true);

        // if (detectingByCircleCast)
        // RangeDetectByRaycast();
    }






    // bool hitLeft;
    // bool hitRight;
    // bool hitUp;
    // bool hitDown;
    //rivate void RangeDetectByRaycast()
    //
    //     if (detectRight) hitLeft = DirectionCircleCast(Vector2.left);
    //     if (detectLeft) hitRight = DirectionCircleCast(Vector2.right);
    //     if (detectUp) hitUp = DirectionCircleCast(Vector2.up);
    //     if (detectDown) hitDown = DirectionCircleCast(Vector2.down);
    //
    //
    //
    //     if (hitLeft || hitRight || hitUp || hitDown)
    //     {
    //        // EnemyDirection(hitLeft);
    //         PlayerInRange();
    //     }
    //     else
    //     {
    //
    //         PlayerOutOfRange();
    //     }
    // }
    // private bool DirectionCircleCast(Vector2 dir)
    // {
    //    // return Physics2D.CircleCast(enemySkin.transform.position, range, dir, range, bessyLayer);
    //     return Physics2D.Raycast(enemySkin.transform.position, dir, range);
    // }

  // private void EnemyDirection(bool left)
  // {
  //     if(left) 
  //     { 
  //     enemySkin.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
  //     }
  //     else { enemySkin.transform.rotation = Quaternion.Euler(0f, 0f, 0f); }
  // } 

    public void PlayerInRange(float angle)
    {
        currentState = State.PlayerInRange;
        if (!attacking)
        {
            StartAttack.Invoke();
            attacking = true;
        }

        Debug.Log("Gracz jest w zasiêgu " + gameObject.name);
    }

    public void PlayerOutOfRange()
    {
        currentState = State.PlayerOutOfRange;
        if (attacking)
        {
            attacking = false;
            StopAttack.Invoke();
        }
        Debug.Log("Gracz nie jest w zasiêgu " + gameObject.name);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemySkin.transform.position, circleCollider.radius);

        
        Gizmos.DrawLine(enemySkin.transform.position, new Vector3(enemySkin.transform.position.x, enemySkin.transform.position.y - 10, enemySkin.transform.position.y));
    }




}
