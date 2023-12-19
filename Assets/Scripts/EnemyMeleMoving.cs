using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] Transform LeftRange;
    [SerializeField] Transform RightRange;
    [SerializeField] float MoveSpeed;
    [SerializeField] SpriteRenderer goblinSprite;

    private Vector3 CurrentMovePoint;
    private Dictionary<bool, Action> FlipFlop = new Dictionary<bool, Action>();
    private bool isMovingRight = true; // Initialize to the default state

    [Header("Attack")]
    [SerializeField] bool Attack;
    [SerializeField] Animator animator;
    [SerializeField] private float EnemyAttackReaction;

    private void Start()
    {
        Attack= false;
        
        CurrentMovePoint = LeftRange.position;

        FlipFlop[true] = () =>
        {
            CurrentMovePoint = LeftRange.position;

           
        };

        FlipFlop[false] = () =>
        {
            CurrentMovePoint = RightRange.position;
           

        };
    }

    private void Update()
    {
        
        Moving();
        
        

    }

    private void Moving()
    {

    if (transform.position != CurrentMovePoint)
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentMovePoint, MoveSpeed * Time.deltaTime);
    }
    else
    {
        FlipFlop[isMovingRight].Invoke();
        isMovingRight = !isMovingRight;

    }
    }

    
  //  private void OnTriggerEnter2D(Collider2D collider)
  //  {
  //      
  //      if (collider.gameObject.CompareTag("Bessy"))
  //      {
  //          Attack = true;
  //          Debug.Log("Gracz wykryty przez Enemy");
  //          animator.SetBool("Attack", true);
  //      }
  //
  //  }
  //
  //  private void OnTriggerExit2D(Collider2D collision)
  //  {
  //      
  //      if (collision.gameObject.CompareTag("Bessy"))
  //      {
  //          Attack = false;
  //          Debug.Log("Gracz wyszed³ z pola Enemy");
  //          animator.SetBool("Attack", false);
  //      }
  //  }

}
