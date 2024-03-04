using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitUntillDetectPlayer : MonoBehaviour
{
    enum State { Wait, DetectPlayer, PlayerInRange, PlayerOutOfRange}
    [SerializeField] State currentState= State.Wait;
    [SerializeField] LayerMask bessyLayer;
    [SerializeField] GameObject enemy;
    Rigidbody2D rb;
    [SerializeField] float range= 10f;

    [Header("Attack Evants")]
    [Space]
    [SerializeField] UnityEvent StartAttack;
    [SerializeField] UnityEvent StopAttack;

    private void Awake()
    {
        rb = enemy.GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        if(currentState == State.Wait)
            DetectingPlayer();
        if (currentState != State.Wait)
            IsPlayerInRange();

    }

    private void DetectingPlayer()
    {

        bool hit = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, bessyLayer);
        if (hit)
        {
            currentState = State.DetectPlayer;
            Debug.Log("Player detected by " + gameObject.name);
            rb.bodyType = RigidbodyType2D.Dynamic;

        }

    }

    private void IsPlayerInRange()
    {
        bool hit = Physics2D.CircleCast(enemy.transform.position, range, Vector2.right, range, bessyLayer);

        if (hit)
        {
            PlayerInRange();
        }
        else
        {
            PlayerOutOfRange();
        }
    }

    private void PlayerInRange()
    {
        currentState= State.PlayerInRange;
        Debug.Log("Gracz jest w zasiêgu " + gameObject.name);
    }

    private void PlayerOutOfRange()
    {
        currentState= State.PlayerOutOfRange;
        Debug.Log("Gracz nie jest w zasiêgu " + gameObject.name);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemy.transform.position, range);
    }

    


}
