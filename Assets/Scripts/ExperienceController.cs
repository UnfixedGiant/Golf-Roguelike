using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExperienceController : MonoBehaviour
{
    [SerializeField] private float currentExperience;
    [SerializeField] private float experienceToNextLevel;

    public float ExperienceProgressPercentage
    {
        get
        {
            return currentExperience / experienceToNextLevel;
        }
    }

    public int CurrentLevel { get; private set; } = 1;

    public UnityEvent OnLevelUp;
    public UnityEvent OnExperienceGained;


    public void AddExperience(float amountToAdd)
    {
        currentExperience += amountToAdd;

        if (currentExperience >= experienceToNextLevel)
        {
            currentExperience -= experienceToNextLevel;
            CurrentLevel++;
            experienceToNextLevel *= 1.5f;
            OnLevelUp.Invoke();
        }
        else
        {
            OnExperienceGained.Invoke();
        }
    }
}