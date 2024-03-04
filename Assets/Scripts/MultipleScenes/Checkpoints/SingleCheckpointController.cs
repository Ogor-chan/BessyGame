using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCheckpointController : GameSaveStateByLastCheckpoint
{
    [Header("Single Checkpoint Controller")]

    public Vector3 checkpointPosition;
    public int checkpointNumber;

    private void Awake()
    {
        checkpointPosition= transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("New checkpoint saved(TEST)");
        }
    }
}
