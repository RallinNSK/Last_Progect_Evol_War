using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonPanel : UpgradePanel
{
    [Header("Cannon Button")]
    public Button[] cannonUpgradeButtons;

    private void Awake()
    {
        SelectionUnitUpdgrade(0);
        foreach (Button button in cannonUpgradeButtons)
        {
            button.interactable = false;
        }
    }

    private void OnEnable()
    {
        int playerCastleLVL = GameController.instance.playerCasleLVL;

        switch (playerCastleLVL)
        {
            case 1:
                cannonUpgradeButtons[0].interactable = true;
                break;
            case 2:
                cannonUpgradeButtons[0].interactable = true;
                cannonUpgradeButtons[1].interactable = true;
                break;
            case 3:
                cannonUpgradeButtons[0].interactable = true;
                cannonUpgradeButtons[1].interactable = true;
                cannonUpgradeButtons[2].interactable = true;
                break;
        }
    }


    public new void SelectionUnitUpdgrade(int select)
    {
        foreach (GameObject button in selectButton)
        {
            button.SetActive(false);
        }
        selectButton[select].SetActive(true);

        foreach (GameObject panel in selectionPanel)
        {
            panel.SetActive(false);
        }
        selectionPanel[select].SetActive(true);

        GameSettings.instance.SoundInterface(0);
    }
}
