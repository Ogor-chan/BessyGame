using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectPlayerByCollider2D : MonoBehaviour
{
    [SerializeField] WaitUntillDetectPlayer _detectPlayerScript;
    [SerializeField] DamagableObjectSpawn _damagableObjectSpawn;
    float angle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _detectPlayerScript.PlayerInRange(angle);
        }
    }

    //Rotation enemy
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {

        Vector2 dir = collision.transform.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            if(angle >-90 && angle < 90)
            {
                EnemyDir(0);

            }
            else
            {
                EnemyDir(180f);
                
            }
        }
    }

    private void EnemyDir(float rotation)
    {
        _detectPlayerScript.enemySkin.transform.rotation = Quaternion.Euler(0, rotation, 0);

        if (rotation == 0)
            _damagableObjectSpawn.enemyDiriection = DamagableObjectSpawn.DirState.Right;
        else
            _damagableObjectSpawn.enemyDiriection = DamagableObjectSpawn.DirState.Left;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _detectPlayerScript.PlayerOutOfRange();
        }
    }
}
