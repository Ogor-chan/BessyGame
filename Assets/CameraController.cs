using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float cameraZPosition = -10f;
    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.z, cameraZPosition);
    }
}
