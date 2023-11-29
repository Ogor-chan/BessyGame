using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSpawn : MonoBehaviour
{

    [Header("ThrowingCrystals")]
    [SerializeField] private float CrystalSpawnCooldown = 1f;
    [SerializeField] private float CrystalDeleteCooldown = 3f;

    [SerializeField] private GameObject goblinCrystal;
    [SerializeField] private Transform goblinCrystalSpawnPoint;

    [SerializeField] private float crystalForce = 10f;
    [SerializeField] private float reverseForceMultiplier = 0.5f; // Przyk³adowa wartoœæ



    private void Start()
    {
        InvokeRepeating("CrystalSpawner", 1f, CrystalSpawnCooldown);

    }



    private void CrystalSpawner()
    {
        GameObject crystal = Instantiate(goblinCrystal, goblinCrystalSpawnPoint.position, Quaternion.identity);

        Rigidbody rb = crystal.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Dodanie si³y w górê
            rb.AddForce(Vector3.up * crystalForce, ForceMode.Impulse);

            // Dodanie lekkiej si³y w przeciwnym kierunku
            Vector3 reverseForce = new Vector3(-1f, 0f, 0f);
            rb.AddForce(reverseForce * crystalForce * reverseForceMultiplier, ForceMode.Impulse);
        }

        Destroy(crystal, CrystalDeleteCooldown);
    }
}