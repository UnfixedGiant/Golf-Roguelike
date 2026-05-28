using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;


    private void OnTriggerStay2D(Collider2D coll)
    {
        TryDealDamage(coll.gameObject);
    }

    private void TryDealDamage(GameObject target)
    {
        
        HealthController health = target.GetComponent<HealthController>();
        if (health == null) return;

        health.TakeDamage(damageAmount);
        
    }
}