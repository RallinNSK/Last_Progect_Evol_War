using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    [Header("Panel")]
    public GameObject playPanel;
    public GameObject gameModePanel;
    public GameObject achivementsPanel;
    public GameObject rateAppPanel;
    public GameObject settingsPanel;
    public GameObject victoryPanel;

    [Header("Custome Game Mode Panel")]
    public CustomeGameScriptVer2 customePanel;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
    }

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Time.timeScale = 1f;
        if (PlayerPrefs.HasKey("Win"))
        {
            StartCoroutine(ShowVictoryPanel());
            PlayerPrefs.DeleteKey("Win");
        }
    }

    public void TogglePlayPanel()
    {
        if (!playPanel.activeSelf)
        {
            playPanel.SetActive(true);
            gameModePanel.SetActive(false);
        }
        else
        {
            playPanel.SetActive(false);
        }
        MenuSettings.instance.SoundInterface(0);
    }

    public void ToggleGameModePanel()
    {
        if (!gameModePanel.activeSelf)
        {
            gameModePanel.SetActive(true);
            playPanel.SetActive(false);
        }
        else
        {
            gameModePanel.SetActive(false);
        }
        MenuSettings.instance.SoundInterface(0);
    }

    public void ToggleAchivementsPanel()
    {
        if (!achivementsPanel.activeSelf)
            achivementsPanel.SetActive(true);
        else
            achivementsPanel.SetActive(false);
        MenuSettings.instance.SoundInterface(0);
    }

    public void ToggleRateAppPanel()
    {
        if (!rateAppPanel.activeSelf)
            rateAppPanel.SetActive(true);
        else
            rateAppPanel.SetActive(false);
        MenuSettings.instance.SoundInterface(0);
    }

    public void ToggleSettingsPanel()
    {
        if (!settingsPanel.activeSelf)
            settingsPanel.SetActive(true);
        else
            settingsPanel.SetActive(false);
        MenuSettings.instance.SoundInterface(0);
    }

    public void ChooseDifficult(int difficult)
    {
        MenuSettings.instance.SoundInterface(0);
        customePanel.DefaltSettings();
        PlayerPrefs.SetInt("Dufficult", difficult);
        SceneManager.LoadScene(1);
    }

    public void ChooseGameMode(int gamemode)
    {
        MenuSettings.instance.SoundInterface(0);
        PlayerPrefs.SetInt("GameMode", gamemode);
        TogglePlayPanel();
    }

    public void CustomeGameMode()
    {
        MenuSettings.instance.SoundInterface(0);
        customePanel.gameObject.SetActive(true);
        ToggleGameModePanel();
    }

    IEnumerator ShowVictoryPanel()
    {
        MenuSettings.instance.SoundInterface(1);
        victoryPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        victoryPanel.SetActive(false);
    }
}
