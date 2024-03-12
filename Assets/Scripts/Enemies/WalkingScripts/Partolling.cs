using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class Partolling : MonoBehaviour
{
    //[SerializeField] DamagableObjectSpawn _damagableObjectSpawn;
    [Header("Patrolling dir and angle")]
    [SerializeField] Vector3 dir;
    [Range(0, 360)]
    [SerializeField] float angle;
    [SerializeField] float radius;
    Transform enemySkinTransform;
    [SerializeField] GameObject Light;
    Light2D light2D;
    [SerializeField] LayerMask bessyLayer;
    [SerializeField] LayerMask obstructionMask;

    [SerializeField] Transform playerTarget;
    public bool canSeePlayer;

    public Vector3 worldDir;
    public Vector3 midPoint;

    [SerializeField] UnityEvent startAttack;
    [SerializeField] UnityEvent stopAttack;

    private void Awake()
    {
        light2D = Light.GetComponent<Light2D>();
        enemySkinTransform = gameObject.GetComponentInParent<Transform>();
        StartCoroutine(FOVRoutine());
    }
    private void Update()
    {
        worldDir = new Vector3(enemySkinTransform.position.x + dir.x, enemySkinTransform.position.y + dir.y, 0);
        midPoint = CalculateMidPoint(transform.position, worldDir, -angle / 2);

        SetLightSettings();
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);

        while (true)
        {
            yield return wait;
            FieldOfVievCheck();
        }
    }
    Vector2 diriectionToTarget2D;
    private void FieldOfVievCheck()
    {
        Collider2D[] rangeChecks2D = Physics2D.OverlapCircleAll(transform.position, radius, bessyLayer);


        if (rangeChecks2D.Length != 0)
        {
            Transform target2D = rangeChecks2D[0].transform;
            diriectionToTarget2D = target2D.position;
            Debug.DrawLine(transform.position, diriectionToTarget2D, Color.blue);

            Vector3 side1 = playerTarget.position - transform.position;
            Vector3 side2 = worldDir - transform.position;
            Vector3.Angle(side1, side2);

            if (Vector3.Angle(side1, side2) < angle / 2)
            {

                float distanceToTarget2D = Vector2.Distance(transform.position, playerTarget.position);

                if (!Physics2D.Raycast(transform.position, diriectionToTarget2D, distanceToTarget2D, obstructionMask))
                {

                    Debug.DrawLine(transform.position, playerTarget.position, Color.red);
                    startAttack.Invoke();
                    canSeePlayer = true;
                }
                else CantSeePlayer();
            }
            else
            {
                CantSeePlayer();
            }

        }
        else if (canSeePlayer)
        {
            CantSeePlayer();
        }

    }

    private void CantSeePlayer()
    {
        stopAttack.Invoke();
        canSeePlayer= false;
    }

    private void SetLightSettings()
    {
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, worldDir - transform.position);
        Light.transform.rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z);
        //radius
        light2D.pointLightInnerRadius = radius;
        light2D.pointLightOuterRadius = radius * 2;

        //angle
        light2D.pointLightInnerAngle = angle;
        light2D.pointLightOuterAngle = angle * 2;

    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR

        Gizmos.color = Color.yellow;



        // Narysuj ³uk k¹ta
        Handles.DrawWireArc(transform.position, Vector3.forward, midPoint - transform.position, angle, radius);


        // Narysuj liniê reprezentuj¹c¹ worldDir
        Handles.DrawLine(transform.position, worldDir, 3);

        Vector3 side1 = playerTarget.position - transform.position;
        Vector3 side2 = worldDir - transform.position;
         Vector3.Angle(side1, side2);

        Handles.DrawWireArc(transform.position, Vector3.forward, playerTarget.position - transform.position, Vector3.Angle(side1, side2), 2);


#endif
    }

    private Vector3 CalculateMidPoint(Vector3 startPoint, Vector3 endPoint, float angle)
    {
        // Normalizuj wektor kierunkowy
        Vector3 direction = (endPoint - startPoint).normalized;

        // Oblicz nowy wektor po obrocie o po³owê k¹ta
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Vector3 rotatedDirection = rotation * direction;

        // Wylicz punkt na linii
        float distance = (endPoint - startPoint).magnitude;
        Vector3 midPoint = startPoint + rotatedDirection * distance;

        return midPoint;
    }
}
