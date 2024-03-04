using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBetweenPoints : MonoBehaviour
{
    public bool isWalking;

    [Header("Points")]
    [SerializeField] Transform[] points;

    [Header("Body")]
    [SerializeField] GameObject walkableBody;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [Header("Variables")]
    [SerializeField] float speed;
     float time;
     float previousTimeTest;
     bool directionRight;

    private void Awake()
    {
        rb = walkableBody.GetComponent<Rigidbody2D>();
        spriteRenderer = walkableBody.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isWalking)
            Walking();
    }

    private void Walking()
    {
        time = (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f;

        directionRight = CheckTimeTestDirection();

        var spriteFlip = directionRight ? walkableBody.transform.rotation = Quaternion.Euler(0f, 0f, 0f) : walkableBody.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        walkableBody.transform.position = Vector3.Lerp(points[0].transform.position, points[1].transform.position, time);

        previousTimeTest = time;

    }

    private bool CheckTimeTestDirection()
    {
        return time> previousTimeTest;
        
    }
}
