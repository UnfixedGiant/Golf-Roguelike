using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ExperienceController playerEXPController;
    [SerializeField] private RectMask2D rectMask;
    private float fullWidth;
 
    private void Start()
    {

        fullWidth = rectMask.GetComponent<RectTransform>().rect.width;

        playerEXPController.OnExperienceGained.AddListener(UpdateEXPBar);
        playerEXPController.OnLevelUp.AddListener(UpdateEXPBar);
 
        UpdateEXPBar();
    }

    private void UpdateEXPBar()
    {
        float percentage = playerEXPController.ExperienceProgressPercentage;

        float rightPadding = fullWidth * (1f - percentage);

            rectMask.padding = new Vector4(
            rectMask.padding.x, 
            rectMask.padding.y, 
            rightPadding,       
            rectMask.padding.w  
        );
    }


}
