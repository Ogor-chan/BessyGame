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
    [SerializeField] Transform goblinTransform;

    private Vector3 CurrentMovePoint;
    private Dictionary<bool, Action> FlipFlop = new Dictionary<bool, Action>();
    private bool isMovingRight = true; // Initialize to the default state


    private void Start()
    {
        
        CurrentMovePoint = LeftRange.position;

        FlipFlop[true] = () =>
        {
            CurrentMovePoint = LeftRange.position;
            goblinTransform.Rotate(0f, 0f, 0f);


        };

        FlipFlop[false] = () =>
        {
            CurrentMovePoint = RightRange.position;
            goblinTransform.Rotate(0f, -180f, 0f);


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

}
