using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public int points = 0;


    [SerializeField] string[] tags;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach(var tag in tags)
        {

            if(collision.gameObject.CompareTag(tag))
             {
                Debug.Log("Wykryto collectable item: " + tag);
                Destroy(collision.gameObject);
                points++;
              }

        }
        
    }
}
