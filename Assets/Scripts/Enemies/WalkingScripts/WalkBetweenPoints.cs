using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class WalkBetweenPoints : MonoBehaviour
{
    public bool isWalking;
    [SerializeField] DamagableObjectSpawn _damagableObjectSpawn;

    [Header("Points")]
    [SerializeField] Transform[] points;

    [Header("Body")]
    [SerializeField] GameObject walkableBody;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    [Header("Variables")]
    [SerializeField] float speed;
     float time;
     float previousTimeTest;
     bool directionRight;
    [Header("Follow player")]
    public bool followPlayer=false;
    [SerializeField] Transform playerSkin;


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
    private Vector2 velocity = Vector3.zero;
    private void Walking()
    {
        time = (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f;

        directionRight = CheckTimeTestDirection();

        SetSpriteAndSpawnPrefabDirection();

        if (followPlayer)
            MoveTowardsPlayer();
        else
            MoveBetweenPoints();

    }

    private void SetSpriteAndSpawnPrefabDirection()
    {
        walkableBody.transform.rotation = directionRight ? Quaternion.Euler(0f, 0f, 0f) : Quaternion.Euler(0f, 180f, 0f);
        _damagableObjectSpawn.enemyDiriection = directionRight ? DamagableObjectSpawn.DirState.Right : DamagableObjectSpawn.DirState.Left;
    }

    private void MoveTowardsPlayer()
    {
        Debug.Log("Follows player");
       // walkableBody.transform.position = Vector2.LerpUnclamped(walkableBody.transform.position, new Vector2(playerSkin.position.x, walkableBody.transform.position.y), time/5);
        
        walkableBody.transform.position = Vector2.SmoothDamp(walkableBody.transform.position, new Vector2(playerSkin.position.x, walkableBody.transform.position.y), ref velocity, 1f, 3f);
       // walkableBody.transform.position = Vector2.MoveTowards(walkableBody.transform.position, new Vector2(playerSkin.position.x, walkableBody.transform.position.y), 3);
    }

    private void MoveBetweenPoints()
    {
        walkableBody.transform.position = Vector2.LerpUnclamped(points[0].position, points[1].position, time);
    }

    public void FollowPlayer(bool value)
    {
        if(followPlayer!=value) 
        { 
        followPlayer = value;
        
        }
    }



    private bool CheckTimeTestDirection()
    {
        return time> previousTimeTest;
        
    }
}
