using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CrystalLizard : MonoBehaviour
{
    public Transform groundCheckPosition;
    public float raycastLength = 10f;
    public LayerMask playerLayer;

    private bool isPlayerCheckd;
    private Rigidbody2D rb;

    private Dictionary<bool, Action> Status = new Dictionary<bool, Action>();

    private void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isPlayerCheckd)
        {
            CheckGrounded();
        }
        
    }

    void CheckGrounded()
    {
        // Wykonaj raycast w d� z pozycji groundCheckPosition
        RaycastHit2D hit = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, raycastLength, playerLayer);

        // Sprawd�, czy raycast trafi� na obiekt z warstw� groundLayer
        isPlayerCheckd = hit.collider != null;

        // Mo�esz doda� logik� lub podejmowa� decyzje na podstawie wyniku isGrounded
        if (!isPlayerCheckd)
        {
            // Gracz jest pod platform�
            Debug.Log("Gracz nie wykryty");
        }
        else
        {
            // Gracz nie jest pod platform�
            Debug.Log("Gracz wykryty");
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void Fall()
    {
        
    }
}
