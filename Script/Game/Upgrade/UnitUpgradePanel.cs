using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Upgrades
{
    [Tooltip("Name upgrade")]
    public string name;
    [Tooltip("Progres bag upgrade")]
    public Image barProgress;
    [Tooltip("Text bonus value upgrade (percent value)")]
    public Text upgradeBonusText;
    [Tooltip("Text cost upgrade")]
    public Text costUpgradesText;
    [Tooltip("Additional modification upgrade")]
    public float[] bonusUpgrade;

    public int enemyStage = 0;
}

public enum TypeUpgrade
{
    Footman = 0, Refleman = 1, Aviation = 2, SuperUnit = 3, Cannon = 4, Income = 5
}

public class UnitUpgradePanel : MonoBehaviour {

    public TypeUpgrade type;    

    [Header("Upgrade")]
    public Upgrades[] upgrades;

    [Header("Button ")]
    public int[] coastUpgrades;

    private void Start()
    {
        foreach(Upgrades upgrade in upgrades)
        {
            upgrade.barProgress.fillAmount = 0f;
            upgrade.costUpgradesText.text = coastUpgrades[0].ToString();
            upgrade.upgradeBonusText.text = "";
        }
    }


    public void Upgrade(int upgradeIndex)
    {
        float multiplayer = 0f;
        
        switch (upgrades[upgradeIndex].barProgress.fillAmount.ToString())
        {
            case "0":
                if (GameController.instance.playerMoney >= coastUpgrades[0])
                {
                    upgrades[upgradeIndex].barProgress.fillAmount += 0.334f;
                    upgrades[upgradeIndex].upgradeBonusText.text = (upgrades[upgradeIndex].bonusUpgrade[0] * 100).ToString() + "%";
                    upgrades[upgradeIndex].costUpgradesText.text = coastUpgrades[1].ToString();
                    multiplayer = upgrades[upgradeIndex].bonusUpgrade[0];

                    GameController.instance.playerMoney -= coastUpgrades[0];
                    GameSettings.instance.SoundInterface(3);
                    GameController.instance.InfoUpdate();
                }
                else
                {
                    GameSettings.instance.SoundInterface(0);
                }
                break;
            case "0.334":
                if (GameController.instance.playerMoney >= coastUpgrades[1])
                {
                    upgrades[upgradeIndex].barProgress.fillAmount += 0.334f;
                    upgrades[upgradeIndex].upgradeBonusText.text = (upgrades[upgradeIndex].bonusUpgrade[1] * 100).ToString() + "%";
                    upgrades[upgradeIndex].costUpgradesText.text = coastUpgrades[2].ToString();
                    multiplayer = upgrades[upgradeIndex].bonusUpgrade[1];

                    GameController.instance.playerMoney -= coastUpgrades[1];
                    GameSettings.instance.SoundInterface(3);
                    GameController.instance.InfoUpdate();
                }
                else
                {
                    GameSettings.instance.SoundInterface(0);
                }
                break;
            case "0.668":
                if (GameController.instance.playerMoney >= coastUpgrades[2])
                {
                    upgrades[upgradeIndex].barProgress.fillAmount += 0.334f;
                    upgrades[upgradeIndex].upgradeBonusText.text = (upgrades[upgradeIndex].bonusUpgrade[2] * 100).ToString() + "%";
                    multiplayer = upgrades[upgradeIndex].bonusUpgrade[2];

                    GameController.instance.playerMoney -= coastUpgrades[2];
                    GameSettings.instance.SoundInterface(3);
                    GameController.instance.InfoUpdate();

                    GameObject button = upgrades[upgradeIndex].costUpgradesText.transform.parent.gameObject;
                    button.SetActive(false);
                }
                else
                {
                    GameSettings.instance.SoundInterface(0);
                }
                break;
        }

        switch(upgrades[upgradeIndex].name)
        {
            case "Attack":
                UnitsController.instance.multipliersPlayer[(int)type].attackMultiplier = 1 + multiplayer;
                UnitsController.instance.UpdateStatsUnit(false);
                if (gameObject.name == "UpgradeCannon")
                {
                    CannonController.instance.PlayerCannonUpdate();
                }
                break;
            case "Range":
                UnitsController.instance.multipliersPlayer[(int)type].rangeMultiplier = 1 + multiplayer;
                UnitsController.instance.UpdateStatsUnit(false);
                if (gameObject.name == "UpgradeCannon")
                {
                    CannonController.instance.PlayerCannonUpdate();
                }
                break;
            case "Attack Speed":
                UnitsController.instance.multipliersPlayer[(int)type].attackSpeedMultiplier = 1 + multiplayer;
                UnitsController.instance.UpdateStatsUnit(false);
                if (gameObject.name == "UpgradeCannon")
                {
                    CannonController.instance.PlayerCannonUpdate();
                }
                break;
            case "Defence":
                UnitsController.instance.multipliersPlayer[(int)type].defenceMultiplier = 1 + multiplayer;
                UnitsController.instance.UpdateStatsUnit(false);
                break;
            case "Health":
                UnitsController.instance.multipliersPlayer[(int)type].healthMultiplier = 1 + multiplayer;
                UnitsController.instance.UpdateStatsUnit(false);
                break;
            case "Income":
                UnitsController.instance.incomeMultiplayerPlayer = 1 + multiplayer;
                UnitsController.instance.UpdateStatsUnit(true);
                break;
        }
    }

    public void EnemyUpgrade(int upgradeIndex)
    {
        float multiplayer = 0f;
        int stage = upgrades[upgradeIndex].enemyStage;
        if ( upgrades[upgradeIndex].enemyStage < 3 && GameController.instance.enemyMoney >= coastUpgrades[stage])
        {
            multiplayer = upgrades[upgradeIndex].bonusUpgrade[stage];
            upgrades[upgradeIndex].enemyStage++;

            GameController.instance.enemyMoney -= coastUpgrades[stage];
            GameController.instance.InfoUpdate();
        }

        if (multiplayer > 0) // Дополнительная проверка
        {
            switch (upgrades[upgradeIndex].name)
            {
                case "Attack":
                    UnitsController.instance.multipliersEnemy[(int)type].attackMultiplier = 1 + multiplayer;
                    UnitsController.instance.UpdateStatsUnit(true);
                    break;
                case "Range":
                    UnitsController.instance.multipliersEnemy[(int)type].rangeMultiplier = 1 + multiplayer;
                    UnitsController.instance.UpdateStatsUnit(true);
                    break;
                case "Attack Speed":
                    UnitsController.instance.multipliersEnemy[(int)type].attackSpeedMultiplier = 1 + multiplayer;
                    UnitsController.instance.UpdateStatsUnit(true);
                    break;
                case "Defence":
                    UnitsController.instance.multipliersEnemy[(int)type].defenceMultiplier = 1 + multiplayer;
                    UnitsController.instance.UpdateStatsUnit(true);
                    break;
                case "Health":
                    UnitsController.instance.multipliersEnemy[(int)type].healthMultiplier = 1 + multiplayer;
                    UnitsController.instance.UpdateStatsUnit(true);
                    break;
                case "Income":
                    UnitsController.instance.incomeMultiplayerEnemy = 1.35f + multiplayer;
                    UnitsController.instance.UpdateStatsUnit(false);
                    break;
            }
        }
    }
}
