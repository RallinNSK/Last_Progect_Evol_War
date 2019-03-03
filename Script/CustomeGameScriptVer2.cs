using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomeGameScriptVer2 : MonoBehaviour {

    [Header("Panels&Buttons")]
    public GameObject[] choosed;
    public GameObject[] panels;
    public GameObject enemyCastleLeavel;
    [Header("Text Button")]
    public Text[] playerPanelTextButton;
    public Text[] enemyPanelTextButton;
    [Header("Tower Text")]
    public bool towerEnable;
    public Text towerText;

    private void OnEnable()
    {
        foreach(GameObject choose in choosed)
        {
            choose.SetActive(false);
        }
        choosed[0].SetActive(true);
        panels[0].SetActive(true);
    }
    public void DefaltSettings()
    {
        PlayerPrefs.SetInt("Dufficult", 0);
        PlayerPrefs.SetInt("EnemyCastleLVL", 0);
        PlayerPrefs.SetInt("EnemyAge", 0);
        PlayerPrefs.SetInt("PlayerAge", 0);
        PlayerPrefs.SetString("Side", "Left");
        PlayerPrefs.SetString("EnableTower", "NO");
    }
    public void OpenPanel(int index)
    {
        foreach(GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        panels[index].SetActive(true);
        foreach (GameObject choose in choosed)
        {
            choose.SetActive(false);
        }
        choosed[index].SetActive(true);
        MenuSettings.instance.SoundInterface(0);
    }
    public void ChooseSideLeft()
    {
        PlayerPrefs.SetString("Side", "Left");
        MenuSettings.instance.SoundInterface(0);
    }
    public void ChooseSideRight()
    {
        PlayerPrefs.SetString("Side", "Right");
        MenuSettings.instance.SoundInterface(0);
    }

    public void ChoosePlayerCaslte(int index)
    {
        foreach(Text textButton in playerPanelTextButton)
        {
            textButton.text = Application.systemLanguage == SystemLanguage.Russian ? "ВЫБРАТЬ" : "SELECT";
            textButton.color = Color.white;
        }
        playerPanelTextButton[index].text = Application.systemLanguage == SystemLanguage.Russian ? "ВЫБРАН" : "SELECTED";
        playerPanelTextButton[index].color = Color.yellow;
        PlayerPrefs.SetInt("PlayerAge", index);
        MenuSettings.instance.SoundInterface(0);
    }
    public void ChooseEnemyCaslte(int index)
    {
        foreach (Text textButton in enemyPanelTextButton)
        {
            textButton.text = Application.systemLanguage == SystemLanguage.Russian ? "ВЫБРАТЬ" : "SELECT";
            textButton.color = Color.white;
        }
        enemyPanelTextButton[index].text = Application.systemLanguage == SystemLanguage.Russian ? "ВЫБРАН" : "SELECTED";
        enemyPanelTextButton[index].color = Color.yellow;
        PlayerPrefs.SetInt("EnemyAge", index);
        enemyCastleLeavel.SetActive(true);
        MenuSettings.instance.SoundInterface(0);
    }
    public void ChouseEnemyCastleLVL(int lvl)
    {
        PlayerPrefs.SetInt("EnemyCastleLVL", lvl);
        enemyCastleLeavel.SetActive(false);
        MenuSettings.instance.SoundInterface(0);
    }
    public void TowerSwitch()
    {
        switch (towerEnable)
        {
            case false:
                towerEnable = true;
                towerText.text = Application.systemLanguage == SystemLanguage.Russian ? "ВКЛЮЧИТЬ" : "ENABLE";
                towerText.color = Color.yellow;
                PlayerPrefs.SetString("EnableTower", "YES");
                break;
            case true:
                towerEnable = false;
                towerText.text = Application.systemLanguage == SystemLanguage.Russian ? "ОТКЛЮЧИТЬ" : "DISABLE";
                towerText.color = Color.white;
                PlayerPrefs.SetString("EnableTower", "NO");
                break;
        }
        MenuSettings.instance.SoundInterface(0);
    }
    //Закрыть Панель
    public void ClosePanel()
    {
        gameObject.SetActive(false);
        MenuSettings.instance.SoundInterface(0);
    }
    //Начать Игру
    public void PlayCustomeMode()
    {
        PlayerPrefs.SetInt("GameMode", 2);
        MenuSettings.instance.SoundInterface(0);
        SceneManager.LoadScene(1);
    }
}
