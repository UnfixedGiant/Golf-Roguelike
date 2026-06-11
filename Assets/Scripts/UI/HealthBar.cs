using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HealthController playerHealthController;
    [SerializeField] private RectMask2D rectMask;
    private float fullWidth;
 
    private void Start()
    {

        fullWidth = rectMask.GetComponent<RectTransform>().rect.width;

        playerHealthController.OnDamaged.AddListener(UpdateHealthBar);
        playerHealthController.OnDeath.AddListener(UpdateHealthBar);
 
        UpdateHealthBar();
    }
 
    private void OnDestroy()
    {
        if (playerHealthController != null)
        {
            playerHealthController.OnDamaged.RemoveListener(UpdateHealthBar);
            playerHealthController.OnDeath.RemoveListener(UpdateHealthBar);
        }
    }
 
    private void UpdateHealthBar()
    {
        float percentage = playerHealthController.RemainingHealthPercentage;

        float rightPadding = fullWidth * (1f - percentage);

            rectMask.padding = new Vector4(
            rectMask.padding.x, 
            rectMask.padding.y, 
            rightPadding,       
            rectMask.padding.w  
        );
    }


}

