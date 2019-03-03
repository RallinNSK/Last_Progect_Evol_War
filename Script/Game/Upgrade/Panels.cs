using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panels : MonoBehaviour {

    private GameController gameController;

    [Header("Panel")]
    public GameObject castleUpgradePanel;
    public GameObject cannonUpgadePanel;
    public GameObject unitUpgadePanel;

    public void CasteUpgradePanel(bool state)
    {
        if (state)
        {
            castleUpgradePanel.SetActive(true);
            cannonUpgadePanel.SetActive(false);
            unitUpgadePanel.SetActive(false);
        }
        else
            castleUpgradePanel.SetActive(false);
        GameSettings.instance.SoundInterface(0);
    }

    public void CannonUpgradePanel(bool state)
    {
        if (state)
        {
            castleUpgradePanel.SetActive(false);
            cannonUpgadePanel.SetActive(true);
            unitUpgadePanel.SetActive(false);
        }
        else
            cannonUpgadePanel.SetActive(false);
        GameSettings.instance.SoundInterface(0);
    }

    public void UnitUpgradePanel(bool state)
    {
        if (state)
        {
            castleUpgradePanel.SetActive(false);
            cannonUpgadePanel.SetActive(false);
            unitUpgadePanel.SetActive(true);
        }
        else
            unitUpgadePanel.SetActive(false);
        GameSettings.instance.SoundInterface(0);
    }

}
