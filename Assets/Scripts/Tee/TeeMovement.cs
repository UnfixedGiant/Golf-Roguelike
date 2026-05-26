using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TeeEnemy : MonoBehaviour
{
[SerializeField]
    private float moveSpeed;
[SerializeField]
    private float rotationSpeed;

    private Rigidbody2D rb;

    private InDashRange inDashRange;
    private Vector2 targetDirection;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inDashRange = GetComponent<InDashRange>();

    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        targetDirection = inDashRange.DirectionToPlayer;
    }

    private void RotateTowardsTarget()
    {
        if (targetDirection == Vector2.zero)
        {
            return;
        }
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


        rb.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        if (targetDirection == Vector2.zero)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = transform.up * moveSpeed;
        }
    }






}
