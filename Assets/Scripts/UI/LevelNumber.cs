using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.VersionControl;

public class LevelNumber : MonoBehaviour
{
    [SerializeField] private ExperienceController xpController;

    [SerializeField] private TMP_Text messageText;


    void Update()
    {
        messageText.SetText(xpController.CurrentLevel.ToString());
    }
}
