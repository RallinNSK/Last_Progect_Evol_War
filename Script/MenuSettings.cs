using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour {

    [System.Serializable]
    public class SettingButton
    {
        public string name;
        public Image image;
        public Sprite spriteON;
        public Sprite spriteOFF;
    }

    [Header("ExitPanel")]
    public GameObject exitPanel;
    [Header("Settings Buttons Sprite")]
    public SettingButton[] settingButtons;

    [Header("Audio Source")]
    public AudioSource music;
    public AudioSource sound;
    [Header("Interface Sound Clip")]
    public AudioClip[] soundInterfaceClips;
    [Header("Params")]
    public bool vibrate_bool;
    public bool music_bool;
    public bool sound_bool;

    public static MenuSettings instance;

    private void Awake()
    {
        instance = FindObjectOfType<MenuSettings>();
        SetupParam();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            exitPanel.SetActive(true);
        }   
    }
    private void SetupParam()
    {
        // Проверка наличия Вибрации
        if (!PlayerPrefs.HasKey("Vibro") || PlayerPrefs.GetString("Vibro") == "YES")
        {
            settingButtons[0].image.sprite = settingButtons[0].spriteON;
            vibrate_bool = true;
        }
        else if (PlayerPrefs.GetString("Vibro") == "NO")
        {
            settingButtons[0].image.sprite = settingButtons[0].spriteOFF;
            vibrate_bool = false;
        }
        // Проверка наличия Музыки
        if (!PlayerPrefs.HasKey("Music") || PlayerPrefs.GetString("Music") == "YES")
        {
            settingButtons[2].image.sprite = settingButtons[2].spriteON;
            music.volume = 100f;
            music_bool = true;
        }
        else if (PlayerPrefs.GetString("Music") == "NO")
        {
            settingButtons[2].image.sprite = settingButtons[2].spriteOFF;
            music.volume = 0f;
            music_bool = false;
        }
        // Проверка на наличие Звуков
        if (!PlayerPrefs.HasKey("Sound") || PlayerPrefs.GetString("Sound") == "YES")
        {
            PlayerPrefs.SetString("Sound", "YES");
            settingButtons[1].image.sprite = settingButtons[1].spriteON;
            sound.volume = 100f;
            sound_bool = true;
        }
        else if (PlayerPrefs.GetString("Sound") == "NO")
        {
            PlayerPrefs.SetString("Sound", "NO");
            settingButtons[1].image.sprite = settingButtons[1].spriteOFF;
            sound.volume = 0f;
            sound_bool = false;
        }
    }

    public void VibroToggle()
    {
        if (settingButtons[0].image.sprite == settingButtons[0].spriteON)
        {
            PlayerPrefs.SetString("Vibro", "NO");
            settingButtons[0].image.sprite = settingButtons[0].spriteOFF;
            vibrate_bool = false;
        }
        else
        {
            PlayerPrefs.SetString("Vibro", "YES");
            settingButtons[0].image.sprite = settingButtons[0].spriteON;
            vibrate_bool = true;
        }
        SoundInterface(0);
    }
    public void SoundToggle()
    {
        if (settingButtons[1].image.sprite == settingButtons[1].spriteON)
        {
            PlayerPrefs.SetString("Sound", "NO");
            settingButtons[1].image.sprite = settingButtons[1].spriteOFF;
            sound.volume = 0f;
            sound_bool = false;
        }
        else
        {
            PlayerPrefs.SetString("Sound", "YES");
            settingButtons[1].image.sprite = settingButtons[1].spriteON;
            sound.volume = 100f;
            sound_bool = true;
        }
        SoundInterface(0);
    }
    public void MusicToggle()
    {
        if (settingButtons[2].image.sprite == settingButtons[2].spriteON)
        {
            PlayerPrefs.SetString("Music", "NO");
            settingButtons[2].image.sprite = settingButtons[2].spriteOFF;
            music.volume = 0f;
            music_bool = false;
        }
        else
        {
            PlayerPrefs.SetString("Music", "YES");
            settingButtons[2].image.sprite = settingButtons[2].spriteON;
            music.volume = 100f;
            music_bool = true;
        }
        SoundInterface(0);
    }

    public void SoundInterface(int soundIndex)
    {
        sound.PlayOneShot(soundInterfaceClips[soundIndex]);
    }

    public void OurGame()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=" + Application.companyName);
    }

    public void Exit()
    {
        SoundInterface(0);
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void CloseExitPanel()
    {
        SoundInterface(0);
        exitPanel.SetActive(false);
    }

}
