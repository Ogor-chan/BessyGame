using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] Transform LeftRange;
    [SerializeField] Transform RightRange;
    [SerializeField] float MoveSpeed;

    private Vector3 CurrentMovePoint;
    private bool FlipFlop; //true = right, false = left

    private void Start()
    {
        FlipFlop = true;
        CurrentMovePoint = RightRange.position;
    }
    private void Update()
    {
        if(transform.position != CurrentMovePoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, CurrentMovePoint, MoveSpeed * Time.deltaTime);
        }
        else
        {
            if (FlipFlop)
            {
                CurrentMovePoint = LeftRange.position;
            }
            else
            {
                CurrentMovePoint = RightRange.position;
            }
            FlipFlop = !FlipFlop;
        }
    }
}
