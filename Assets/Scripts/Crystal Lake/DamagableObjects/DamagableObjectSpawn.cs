using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamagableObjectSpawn : MonoBehaviour
{ 
    public enum State { Attacking , NotAttacking}
    [SerializeField] State currentState;
    public bool isActive = true;
    public bool isPlayerDirection = false;

    
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform objectContainer;

    [SerializeField] float spawnDuration;
    [SerializeField] float objectLifetime;
    [SerializeField] float upwardForce = 10f;



    private void Awake()
    {
        var currentStateCheck = isActive? currentState==State.NotAttacking : currentState==State.Attacking ;

        if (isActive)
            StartCoroutine(SpawnPrefab());
    }

    public void StopAttacking()
    {
        if (currentState == State.Attacking)
        {
            isActive= false;
            currentState= State.NotAttacking;
            StopCoroutine(SpawnPrefab());
        }
        
    }

    public void StartAttacking()
    {
        if(currentState== State.NotAttacking)
        {
            isActive = true;
            StartCoroutine(SpawnPrefab());
        }
        
    }



    IEnumerator SpawnPrefab()
    {
        currentState = State.Attacking;

        GameObject clone = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation, objectContainer);
        Rigidbody2D rb= clone.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector3(1,1,0) * upwardForce, ForceMode2D.Impulse);
        Destroy(clone, objectLifetime);

        yield return new WaitForSeconds(spawnDuration);
        StartCoroutine(SpawnPrefab());

    }
}
