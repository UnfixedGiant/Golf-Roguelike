using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInvin : MonoBehaviour
{
    [SerializeField] private float invinsibilityDuration;
    private InvinController invinController;

    void Awake()
    {
        invinController = GetComponent<InvinController>();
    }
    public void StartInvin()
    {
        invinController.StartInvin(invinsibilityDuration);
    }
}
