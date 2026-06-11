using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    private LevelUpUI levelUpUI;
    private ExperienceController experienceController;

    private readonly AbilityType[] allAbilities =
    {
        AbilityType.FireExplo,
        AbilityType.IceNova,
        AbilityType.IceNovasomething
    };

    private void Awake()
    {
        levelUpUI = FindObjectOfType<LevelUpUI>();
        experienceController = FindObjectOfType<ExperienceController>();

        experienceController.OnLevelUp.AddListener(TriggerLevelUp);
    }


    // Choosing 3 random abilities that the player can level.
    private void TriggerLevelUp()
    {
        List<AbilityType> pool = new List<AbilityType>(allAbilities);

        LevelUpUI.LevelUpOption[] options = new LevelUpUI.LevelUpOption[3];

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, pool.Count);

            options[i] = levelUpUI.AbilityOptions(pool[randomIndex]);

            pool.RemoveAt(randomIndex);
        }

        levelUpUI.ShowLevelUpUI(options);
    }
}