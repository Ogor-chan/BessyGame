using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the triggering object has the "Player" tag
        {
            Debug.Log("Player entered trigger zone!");
            // You can add your logic here for what should happen when the player enters the trigger zone
        }
    }
}
