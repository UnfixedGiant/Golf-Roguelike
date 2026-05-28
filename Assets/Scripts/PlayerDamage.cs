using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private Rigidbody2D rb;
    
    [Header("Damage")]
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float damageMultiplier = 1f;


    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        TryDealDamage(coll.gameObject);
    }

    private void TryDealDamage(GameObject target)
    {
        HealthController health = target.GetComponent<HealthController>();
        if (health == null) return;

        float speed = rb.velocity.magnitude;
        float damage = damageAmount + (speed * damageMultiplier);
        health.TakeDamage(damage);
        Debug.Log(damage);
    }



}
