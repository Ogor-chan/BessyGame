using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakiingDamageTrigger : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] LayerMask takingDamageLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isPlayer)
        {
            Debug.Log(gameObject.name + " taking damage.");
        }
        else if (!isPlayer && (takingDamageLayer & (1 << other.gameObject.layer)) != 0)
        {
            Debug.Log(gameObject.name + " taking damage from " + other.name);
        }
    }
}
