using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class ExplosionDmg : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [Header("Damage")]
    [SerializeField] private float damageAmount = 10f;


    private void OnTriggerStay2D(Collider2D coll)
    {
        TryDealDamage(coll.gameObject);
    }

    private void TryDealDamage(GameObject target)
    {
        if (!target.CompareTag("Enemy"))
        {
            return;
        } 

        HealthController health = target.GetComponent<HealthController>();
        if (health == null)
        {
            return;
        }

        health.TakeDamage(damageAmount);
        Debug.Log("Explosion Damage: " + damageAmount);
    }






}
