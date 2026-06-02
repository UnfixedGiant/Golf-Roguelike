using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class TeeEnemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float dashSpeed = 12f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1.5f;

    [Header("Experience")]
    [SerializeField] private float experienceReward = 25f;
    private ExperienceController playerXPController;



    private Rigidbody2D rb;
    private InDashRange inDashRange;
    private Vector2 targetDirection;

    private bool isDashing = false;
    private bool dashOnCooldown = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inDashRange = GetComponent<InDashRange>();
        playerXPController = FindObjectOfType<ExperienceController>();
    }

private void FixedUpdate()
{
    targetDirection = inDashRange.DirectionToPlayer;

    RotateTowardsTarget();

    if (isDashing) return;

    if (inDashRange.DashRange && !dashOnCooldown)
    {
        rb.velocity = Vector2.zero; // stop moving before dash
        StartCoroutine(Dash());
    }
    else if (!inDashRange.DashRange) // only chase when outside dash range
    {
        rb.velocity = -transform.up * moveSpeed;
    }
    else
    {
        // in dash range but on cooldown — stop and wait
        rb.velocity = Vector2.zero;
    }
}

    private void RotateTowardsTarget()
    {
        if (targetDirection == Vector2.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion flipped = targetRotation * Quaternion.Euler(0, 0, 180f);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, flipped, rotationSpeed * Time.deltaTime);
        rb.SetRotation(rotation);
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        dashOnCooldown = true;

        Vector2 dashDirection = targetDirection; // lock direction at dash start
        float elapsed = 0f;

        while (elapsed < dashDuration)
        {
            rb.velocity = dashDirection * dashSpeed;
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        dashOnCooldown = false;
    }

    public void AddXP()
    {
        playerXPController.AddExperience(experienceReward);
    }


}
