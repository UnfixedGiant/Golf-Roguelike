using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private GameObject fireExploPrefab;

    [Header("Attributes")]
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 2f;
    
    [Header("Abilities")]
    [SerializeField] private bool fireExploUnlock;
    [SerializeField] private float speedThreshold = 2f;




    private bool isDragging;



    private void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        if (!IsReady()) return;

        Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(transform.position, inputPos);


        if (Input.GetMouseButtonDown(0) && distance <= 0.5f) DragStart();
        if (Input.GetMouseButton(0) && isDragging) DragChange(inputPos);
        if (Input.GetMouseButtonUp(0) && isDragging) DragRelease(inputPos);

    }

// Can change the isReady later so that the player
// can get some kind of upgrades to reduce time between each ball hit
    private bool IsReady()
    {
        return rb.velocity.magnitude <= 0.3f;
    }


    private void DragStart()
    {
        isDragging = true;
        lr.positionCount = 2;
    }
    private void DragChange (Vector2 pos)
    {
        Vector2 dir = (Vector2)transform.position - pos;

        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, (Vector2)transform.position + Vector2.ClampMagnitude((dir * power) / 2, maxPower / 2));
    }

    private void DragRelease(Vector2 pos)
    {
        float distance = Vector2.Distance((Vector2)transform.position, pos);
        isDragging = false;
        lr.positionCount = 0;

        if (distance < 1f)
        return;

        Vector2 dir = (Vector2) transform.position - pos;
        rb.velocity = Vector2.ClampMagnitude(dir * power, maxPower);
    }


    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (!coll.gameObject.CompareTag("Wall"))
        {
            return;
        }

        if (rb.velocity.magnitude < speedThreshold)
        {
            return;
        }

        if (!fireExploUnlock)
        {
            fireExplosion(coll.contacts[0].point);
        }


    }

    private void fireExplosion(Vector2 pos)
    {
        if (fireExploPrefab == null)
        {
            return;
        }
        
        GameObject explosion = Instantiate(fireExploPrefab, pos, Quaternion.identity);
        Destroy(explosion, 0.5f);
    }

}