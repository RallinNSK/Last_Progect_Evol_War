using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleUpgrade : MonoBehaviour {

    //private GameController gameController;


    [Header("Castle Upgrade")]
    public Image[] castleImages;
    public Image[] block;
    public Button[] buttons;
    public Text[] textCost;
    public int[] costUpgrade;


    private void Start()
    {
        //gameController = FindObjectOfType<GameController>();

        for (int i = 0; i < textCost.Length; i++)
        {
            textCost[i].text = costUpgrade[i].ToString();
        }
    }

    public void UpgradeCastle(int lvl)
    {
        if (GameController.instance.playerMoney >= costUpgrade[lvl] && castleImages[lvl].color != Color.green && !block[lvl].gameObject.activeSelf)
        {
            GameController.instance.CastleUpgrade(costUpgrade[lvl], lvl + 1);
            AchivmentsController.instance.GainAchivmentForUpgradeCastle(lvl);
            castleImages[lvl].color = Color.green;
            if (lvl < 2)
                block[lvl + 1].gameObject.SetActive(false);
            textCost[lvl].text = Application.systemLanguage == SystemLanguage.Russian ? "Купленно": "Buyed";
            GameSettings.instance.SoundInterface(3);
        }
    }
}
