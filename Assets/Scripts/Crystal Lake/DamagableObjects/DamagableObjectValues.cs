using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableObjectValues : MonoBehaviour
{
    [SerializeField] float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player taking damage from: " + gameObject.name);
            Destroy(gameObject);
        }
    }

    

}
