using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject player;
    public float sightRange = 10f;
    public LayerMask obstacleLayer;

    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float ShootCooldown;
    private float currentCooldown;


    void Update()
    {
        if(currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, sightRange);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player") && distanceToPlayer <= sightRange)
            {
                if(currentCooldown <= 0)
                {
                    SpawnProjectile();
                }
            }
        }



    }

    private void SpawnProjectile()
    {
        currentCooldown = ShootCooldown;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = direction * projectileSpeed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectileRb.rotation = angle;
    }
}
