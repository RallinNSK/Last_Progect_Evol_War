using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : MonoBehaviour {

    //private GameController gameController;

    [Header("Upgrade Selection")]
    public GameObject[] selectButton;
    public GameObject[] selectionPanel;

    private void Start()
    {
        SelectionUnitUpdgrade(0);
    }

    public void SelectionUnitUpdgrade(int select)
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
