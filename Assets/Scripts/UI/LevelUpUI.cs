using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject levelUpPanel;
    [SerializeField] private Button[] optionButtons;
    [SerializeField] private TextMeshProUGUI[] optionLabels;
    [SerializeField] private TextMeshProUGUI[] optionDescriptions;
    [SerializeField] private TextMeshProUGUI[] optionTiers;

    private Ball ball;

    public struct LevelUpOption
    {
        public string name;
        public string description;
        public System.Action onSelected;
        public string tierText;
    }

    private void Awake()
    {
        ball = FindObjectOfType<Ball>();
        levelUpPanel.SetActive(false);
    }

    public void ShowLevelUpUI(LevelUpOption[] options)
    {
        levelUpPanel.SetActive(true);
        Time.timeScale = 0f;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i >= options.Length)
            {
                optionButtons[i].gameObject.SetActive(false);
                continue;
            }

            optionButtons[i].gameObject.SetActive(true);

            LevelUpOption opt = options[i];

            optionLabels[i].text = opt.name;
            optionDescriptions[i].text = opt.description;
            optionTiers[i].text = opt.tierText;

            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => SelectOption(opt));
        }
    }

    private void SelectOption(LevelUpOption option)
    {
        option.onSelected?.Invoke();
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }


    // Switch for all of the abilities and their desctiptions.
    // This is what is displayed to the player.

    public LevelUpOption AbilityOptions(AbilityType abilityType)
    {
        int currentLevel = ball.GetAbilityLevel(abilityType);

        switch (abilityType)
        {
            case AbilityType.FireExplo:

                string fireDescription;
                string fireTier;
                // Some abilities will need to be unlocked initially. After that the player can upgrade these abilities further.
                if (currentLevel == 0)
                {
                    fireDescription = "Causes an explosion when hitting a wall";
                    fireTier = "NEW";
                }
                else
                {
                    fireDescription =
                        $"Increase explosion damage (current: {ball.GetFireExploDamage()})";

                    fireTier =
                        $"Level {currentLevel} -> {currentLevel + 1}";
                }

                return new LevelUpOption
                {
                    name = "Fire Explosion",
                    description = fireDescription,
                    tierText = fireTier,
                    onSelected = () => ball.UpgradeAbility(AbilityType.FireExplo)
                };

            case AbilityType.IceNova:

                string iceDescription;
                string iceTier;

                if (currentLevel == 0)
                {
                    iceDescription = "Freeze nearby enemies on impact";
                    iceTier = "NEW";
                }
                else
                {
                    iceDescription = "Increase freeze duration";
                    iceTier =
                        $"Level {currentLevel} -> {currentLevel + 1}";
                }

                return new LevelUpOption
                {
                    name = "Ice Nova",
                    description = iceDescription,
                    tierText = iceTier,
                    onSelected = () => ball.UpgradeAbility(AbilityType.IceNova)
                };
            
            case AbilityType.IceNovasomething:

                string iceSomeDescription;
                string iceSomeTier;

                if (currentLevel == 0)
                {
                    iceSomeDescription = "GABBAGABBAGOOO";
                    iceSomeTier = "NEW";
                }
                else
                {
                    iceSomeDescription = "Increase freeze duration";
                    iceSomeTier =
                        $"Level {currentLevel} -> {currentLevel + 1}";
                }

                return new LevelUpOption
                {
                    name = "Ice Nova Something",
                    description = iceSomeDescription,
                    tierText = iceSomeTier,
                    onSelected = () => ball.UpgradeAbility(AbilityType.IceNova)
                };

            default:

                return new LevelUpOption
                {
                    name = "Unknown",
                    description = "Unknown Ability",
                    tierText = "",
                    onSelected = null
                };
        }
    }
}