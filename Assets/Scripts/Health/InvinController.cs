using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvinController : MonoBehaviour
{
    private HealthController healthController;

    private void Awake()
    {
        healthController = GetComponent<HealthController>();

    }

    public void StartInvin(float invinsibilityDuration)
    {
        StartCoroutine(InvinCoroutine(invinsibilityDuration));
    }

    private IEnumerator InvinCoroutine(float invinsibilityDuration)
    {
        healthController.IsInvincible = true;
        yield return new WaitForSeconds(invinsibilityDuration);
        healthController.IsInvincible = false;
    }
}
