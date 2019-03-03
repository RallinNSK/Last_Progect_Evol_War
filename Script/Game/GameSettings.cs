using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour {
    [System.Serializable]
    public class SettingButton
    {
        public string name;
        public Image image;
        public Sprite spriteON;
        public Sprite spriteOFF;
    }


    [Header("Panel")]
    public GameObject settings;
    public GameObject backToMenu;
    [Header("Settings Buttons Sprite")]
    public SettingButton[] settingButtons; 
    [Header("Time Speed Game")]
    public Text timeText;
    public int timeScale = 1;

    [Header("Audio Source")]
    public AudioSource music;
    public AudioSource sound;
    public GameObject prefabSoundUnit;
    [Header("AudioClip")]
    public AudioClip[] backgroundMusicClips;
    public AudioClip[] soundInterfaceClips;
    public AudioClip[] soundUltimate;
    [Header("Params")]
    public bool vibrate_bool;
    public bool music_bool;
    public bool sound_bool;


    public static GameSettings instance;

    private void Awake()
    {
        instance = FindObjectOfType<GameSettings>();
        music.clip = backgroundMusicClips[(int)Random.Range(0, backgroundMusicClips.Length)];
        music.Play();

        SetupParam();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            BackToMenuPanelShow();
        }
    }

    private void SetupParam()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
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

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Time.timeScale = timeScale;
        }
        else
        {
            Time.timeScale = 0f;
        }
    }

    public void PanelSettings()
    {
        if (!settings.activeSelf)
            settings.SetActive(true);
        else
            settings.SetActive(false);
        SoundInterface(0);
    }

    public void VibroToggle()
    {
        if(settingButtons[0].image.sprite == settingButtons[0].spriteON)
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

    public void ChangeTime()
    {
        switch(timeScale)
        {
            case 0:
                Time.timeScale = 1;
                timeScale = 1;

                timeText.text = "x" + timeScale.ToString();
                break;
            case 1:
                Time.timeScale = 2;
                timeScale = 2;

                timeText.text = "x" + timeScale.ToString();
                break;
            case 2:
                Time.timeScale = 3;
                timeScale = 3;

                timeText.text = "x" + timeScale.ToString();
                break;
            case 3:
                Time.timeScale = 0;
                timeScale = 0;

                timeText.text = "x" + timeScale.ToString();
                break;
        }
        SoundInterface(0);
    }


    public void SoundUlt(int ageIndex)
    {
        sound.PlayOneShot(soundUltimate[ageIndex]);
    }

    public void SoundInterface(int soundIndex)
    {
        sound.PlayOneShot(soundInterfaceClips[soundIndex]);
    }

    public void SoundUnit(AudioClip Clip,Transform target)
    {
        GameObject SoucreUnit = Instantiate(prefabSoundUnit);
        SoucreUnit.transform.parent = target;
        AudioSource source = SoucreUnit.GetComponent<AudioSource>();
        source.volume = sound.volume;
        source.PlayOneShot(Clip);
    }

    public void OurGame()
    {
        SoundInterface(0);
    }

    public void BackToMenuPanelShow()
    {
        SoundInterface(0);
        backToMenu.SetActive(true);
    }

    public void BackToMenuPanelClose()
    {
        SoundInterface(0);
        backToMenu.SetActive(false);
    }

    public void BackToMenu()
    {
        SoundInterface(0);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }


    public void Restart()
    {
        SoundInterface(0);
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
