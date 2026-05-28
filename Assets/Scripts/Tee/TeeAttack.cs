using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float damageCooldown = 1f;

    private float lastDamageTime;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Trigger fired by: " + coll.gameObject.name);
        TryDealDamage(coll.gameObject);
        Debug.Log(coll);
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (Time.time >= lastDamageTime + damageCooldown)
        {
            TryDealDamage(coll.gameObject);
        }
    }

    private void TryDealDamage(GameObject target)
    {
        HealthController health = target.GetComponent<HealthController>();

        if (health != null)
        {
            health.TakeDamage(damageAmount);
            lastDamageTime = Time.time;
        }
    }
}