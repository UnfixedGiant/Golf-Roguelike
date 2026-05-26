using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InDashRange : MonoBehaviour
{
    public bool DashRange { get; private set; }
    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField] private float dashableRange = 3f;

    private Transform player;

    private void Awake()
    {
        player = FindObjectOfType<Ball>().transform;
    }

    private void Update()
    {
        Vector2 enemyToPlayer = player.position - transform.position;
        DirectionToPlayer = enemyToPlayer.normalized;
        DashRange = enemyToPlayer.magnitude <= dashableRange;
    }
}
