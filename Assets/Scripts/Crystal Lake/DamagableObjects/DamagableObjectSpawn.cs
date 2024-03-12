using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamagableObjectSpawn : MonoBehaviour
{
    public enum State { Attacking, NotAttacking }
    public enum DirState { Left, Right }

    [Header("Actua lState")]
    [SerializeField] State attackState;
    public DirState enemyDiriection = DirState.Right;

    [SerializeField] bool attackFromBegin;
    bool attack = false;


    //[Header("Enemy Settings")]

   // [Tooltip("If enemy aim the player, check bool and add playerSkin")]
    //[SerializeField] bool isEnemyAimToPlayer;
    //[SerializeField] GameObject playerSkin;

    [Header("Prefab Manager")]
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform objectContainer;

    [Header("Spawn Prefab Values")]
    [SerializeField] float spawnDuration;
    [SerializeField] float objectLifetime;
    [SerializeField] float upwardForceMaxValue = 10f;
    [SerializeField] float rangeForceMaxValue = 10f;


    public void Awake()
    {
       // if (!isEnemyAimToPlayer)
        //    playerSkin = null;


        var setAttackState = attackFromBegin ? attackState = State.Attacking : attackState = State.NotAttacking;

        if (attackState == State.Attacking)
            StartCoroutine(SpawnPrefab());


    }

    public void InvokeAttack(bool v)
    {
        if (v && attack == false)
        {
            attack = true;
            attackState = State.Attacking;
            StartCoroutine(SpawnPrefab());
        }
        else if (!v && attack == true)
        {
            attack = false;
            attackState = State.NotAttacking;
            StopAllCoroutines();
        }
    }


    IEnumerator SpawnPrefab()
    {

        GameObject clone = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation, objectContainer);
        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        //if (!isEnemyAimToPlayer)
            ForceSpawnPrefab(rb);

        Destroy(clone, objectLifetime);

        yield return new WaitForSeconds(spawnDuration);
        StartCoroutine(SpawnPrefab());

    }


    private void ForceSpawnPrefab(Rigidbody2D rb)
    {
        float randomUpwardForce = Random.Range(upwardForceMaxValue/2, upwardForceMaxValue);
        float randomRangeForce = Random.Range(rangeForceMaxValue / 2, rangeForceMaxValue);


        if (enemyDiriection == DirState.Right)
            rb.AddForce(new Vector3(1 * randomRangeForce, 1.5f * randomUpwardForce, 0), ForceMode2D.Impulse);
        else if (enemyDiriection == DirState.Left)
            rb.AddForce(new Vector3(-1 * randomRangeForce, 1.5f * randomUpwardForce, 0), ForceMode2D.Impulse);

    }
}
