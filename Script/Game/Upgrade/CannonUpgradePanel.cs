using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonUpgradePanel : MonoBehaviour {

    [Header("Floor")]
    public int floor;

    public Image[] imageCannons;
    public GameObject[] infoUpgrades;
    public Text[] costUpgadeText;
    public int[]  costUpgrade;

    public bool cannonBuy;
    private bool cannonEnemyBuy;

    public void BuyCannon(int cannonlvl)
    {
        if (!cannonBuy && GameController.instance.playerMoney >= costUpgrade[cannonlvl])
        {
            imageCannons[cannonlvl].color = Color.green;
            cannonBuy = true;

            GameController.instance.playerMoney -= costUpgrade[cannonlvl];
            CannonController.instance.CheckFirepowerAchiv();
            GameSettings.instance.SoundInterface(2);
            GameController.instance.InfoUpdate();

            foreach(var button in costUpgadeText)
            {
                button.GetComponentInParent<Button>().interactable = false;
            }
            costUpgadeText[cannonlvl].GetComponentInParent<Button>().interactable = true;

            CannonController.instance.SetupPlayerCannon(cannonlvl, floor-1);

            if (Application.systemLanguage == SystemLanguage.Russian)
                costUpgadeText[cannonlvl].text = "Продать";
            else
                costUpgadeText[cannonlvl].text = "Sell";
        }
        else if(cannonBuy)
        {
            imageCannons[cannonlvl].color = Color.white;
            cannonBuy = false;

            GameController.instance.playerMoney += costUpgrade[cannonlvl]/10;
            AchivmentsController.instance.Gunsmith();
            GameSettings.instance.SoundInterface(0);
            GameController.instance.InfoUpdate();

            foreach (var button in costUpgadeText)
            {
                button.GetComponentInParent<Button>().interactable = true;
            }

            CannonController.instance.RemovePlayerCannon(floor-1);
            costUpgadeText[cannonlvl].text = costUpgrade[cannonlvl].ToString();
        }
    }

    public void BuyCannonEnemy()
    {
        int cannonlvl = 0;
        if (!cannonEnemyBuy && GameController.instance.enemyMoney >= costUpgrade[2])
        {
            cannonlvl = 2;
            CannonEmeny(cannonlvl);
        }
        else if (!cannonEnemyBuy && GameController.instance.enemyMoney >= costUpgrade[1])
        {
            cannonlvl = 1;
            CannonEmeny(cannonlvl);
        }
    }

    private void CannonEmeny(int cannonlvl)
    {
        cannonEnemyBuy = true;

        GameController.instance.enemyMoney -= costUpgrade[cannonlvl];
        GameController.instance.InfoUpdate();

        CannonController.instance.SetupEnemyCannon(cannonlvl, floor - 1);
    }

    public void ToggleInfo(int cannonlvl)
    {
        infoUpgrades[cannonlvl].SetActive(!infoUpgrades[cannonlvl].activeSelf);
    }
}
