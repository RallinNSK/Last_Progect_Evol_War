using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivmentsController : MonoBehaviour {

    [Header("Achivment Parametrs")]
    public int seaOfSwords;
    public int cloudOfArrows;
    public int theDarkSky;
    public int theArmyInvincible;
    public int nucklerBomb;
    public int gunsmith;
    public int duelist;
    public int invader;
    public int insaneBattle;
    public int aggressiveCapture;
    public int captain;
    public int colonel;
    public int marshal;
    public int senior;
    public int feudal;
    public int monarch;
    public int theStrongestThug;
    public int theEmperorOfHeaven;
    public int theKingChosenByGod;
    public int thePresidentOfThePlanet;
    public int theMasterOfTheUniverse;
    public int theDeityOfTheAbyss;
    public int thePinnacleOfEvolution;
    public int aMessengerWithTidings;
    public int worldTreasures;
    [Header("Sprite&Text")]
    public Image achivSprite;
    public Text achivNameText;
    public Sprite[] sprites;
    [Header("Achivment Show")]
    public GameObject achivShow;

    public static AchivmentsController instance;

    public void Awake()
    {
        instance = FindObjectOfType<AchivmentsController>();
        SetupParametrs();
    }

    private void SetupParametrs()
    {
        seaOfSwords = PlayerPrefs.GetInt("Sea of Swords");
        cloudOfArrows = PlayerPrefs.GetInt("Cloud of arrows");
        theDarkSky = PlayerPrefs.GetInt("The Dark sky");
        theArmyInvincible = PlayerPrefs.GetInt("The army invincible");
        nucklerBomb = PlayerPrefs.GetInt("Nuclear bomb");
        gunsmith = PlayerPrefs.GetInt("Gunsmith");
        duelist = PlayerPrefs.GetInt("Duelist");
        invader = PlayerPrefs.GetInt("Invader");
        insaneBattle = PlayerPrefs.GetInt("Insane Battle");
        aggressiveCapture = PlayerPrefs.GetInt("Aggressive Capture");
        captain = PlayerPrefs.GetInt("Captain");
        colonel = PlayerPrefs.GetInt("Colonel");
        marshal = PlayerPrefs.GetInt("Marshal");
        senior = PlayerPrefs.GetInt("Senior");
        feudal = PlayerPrefs.GetInt("Feudal");
        monarch = PlayerPrefs.GetInt("Monarch");
        theStrongestThug = PlayerPrefs.GetInt("The Strongest Thug");
        theEmperorOfHeaven = PlayerPrefs.GetInt("The Emperor of Heaven");
        theKingChosenByGod = PlayerPrefs.GetInt("The King Chosen by God");
        thePresidentOfThePlanet = PlayerPrefs.GetInt("The President of the Planet");
        theMasterOfTheUniverse = PlayerPrefs.GetInt("The Master of the Universe");
        theDeityOfTheAbyss = PlayerPrefs.GetInt("The Deity of the Abyss");
        thePinnacleOfEvolution = PlayerPrefs.GetInt("The Pinnacle of Evolution");
        aMessengerWithTidings = PlayerPrefs.GetInt("A messenger with tidings");
        worldTreasures = PlayerPrefs.GetInt("World Treasures");
    }

    public void GainAchivmentForUnit(int unitIndex)
    {
        switch (unitIndex)
        {
            case 0:
            seaOfSwords++;
            PlayerPrefs.SetInt("Sea of Swords", seaOfSwords);

                if (seaOfSwords == 1500 && !PlayerPrefs.HasKey("Achivment_0"))
                {
                    PlayerPrefs.SetString("Achivment_0", "YES");
                    achivSprite.sprite = sprites[0];
                    achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Море мечей" : "Sea of swords";
                    StartCoroutine(ShowAchivUnlocked());
                }
                break;
            case 1:
                cloudOfArrows++;
                PlayerPrefs.SetInt("Cloud of arrows", cloudOfArrows);

                if (cloudOfArrows == 1000 && !PlayerPrefs.HasKey("Achivment_1"))
                {
                    PlayerPrefs.SetString("Achivment_1", "YES");
                    achivSprite.sprite = sprites[1];
                    achivNameText.text  = Application.systemLanguage == SystemLanguage.Russian ? "Туча стрел" : "Cloud of arrows";
                    StartCoroutine(ShowAchivUnlocked());
                }
                break;
            case 2:
                theDarkSky++;
                PlayerPrefs.SetInt("The Dark sky", theDarkSky);

                if (theDarkSky == 700 && !PlayerPrefs.HasKey("Achivment_2"))
                {
                    PlayerPrefs.SetString("Achivment_2", "YES");
                    achivSprite.sprite = sprites[2];
                    achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Чёрное небо" : "The Dark sky";
                    StartCoroutine(ShowAchivUnlocked());
                }
                break;
            case 3:
                theArmyInvincible++;
                PlayerPrefs.SetInt("The army invincible", theDarkSky);

                if (theArmyInvincible == 500 && !PlayerPrefs.HasKey("Achivment_3"))
                {
                    PlayerPrefs.SetString("Achivment_3", "YES");
                    achivSprite.sprite = sprites[3];
                    achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Армия непобедимых" : "The army invincible";
                    StartCoroutine(ShowAchivUnlocked());
                }
                break;
        }
    }

    public void NucklerBomb()
    {
        nucklerBomb++;
        PlayerPrefs.SetInt("Nuclear bomb", nucklerBomb);

        //использование ультимейта 1 раз(Ядерная бомба)
        if (nucklerBomb == 1 && !PlayerPrefs.HasKey("Achivment_4"))
        {
            PlayerPrefs.SetString("Achivment_4", "YES");
            achivSprite.sprite = sprites[4];
            achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Ядерная бомба" : "Nuclear bomb";
            StartCoroutine(ShowAchivUnlocked());
        }
        //использование ультимейта 30 раз(Геноцид)
        else if (nucklerBomb == 30 && !PlayerPrefs.HasKey("Achivment_5"))
        {
            PlayerPrefs.SetString("Achivment_5", "YES");
            achivSprite.sprite = sprites[5];
            achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Геноцид" : "Genocide";
            StartCoroutine(ShowAchivUnlocked());
        }
    }

    public void Firepower(bool check)
    {
        //Когда будут поставлены все 3 пушки на замке
        if (check && !PlayerPrefs.HasKey("Achivment_6"))
        {
            PlayerPrefs.SetString("Achivment_6", "YES");
            achivSprite.sprite = sprites[6];
            achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Огневая мощь" : "Firepower";
            StartCoroutine(ShowAchivUnlocked());
        }
    }

    public void Gunsmith()
    {
        gunsmith++;
        PlayerPrefs.SetInt("Gunsmith", gunsmith);

        if (gunsmith == 100 && !PlayerPrefs.HasKey("Achivment_7"))
        {
            PlayerPrefs.SetString("Achivment_7", "YES");
            achivSprite.sprite = sprites[7];
            achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Оружейник" : "Gunsmith";
            StartCoroutine(ShowAchivUnlocked());
        }
    }

    public void Duelist()
    {
        duelist++;
        PlayerPrefs.SetInt("Duelist", duelist);

        if (duelist == 1 && !PlayerPrefs.HasKey("Achivment_8"))
        {
            PlayerPrefs.SetString("Achivment_8", "YES");
            achivSprite.sprite = sprites[8];
            achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Дуэлянт" : "Duelist";
            StartCoroutine(ShowAchivUnlocked());
        }
    }

    public void Invader()
    {
        invader++;
        PlayerPrefs.SetInt("Invader", invader);

        if (invader == 1 && !PlayerPrefs.HasKey("Achivment_9"))
        {
            PlayerPrefs.SetString("Achivment_9", "YES");
            achivSprite.sprite = sprites[9];
            achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Захватчик" : "Invader";
            StartCoroutine(ShowAchivUnlocked());
        }
    }

    public void InsaneBattle()
    {
        insaneBattle++;
        PlayerPrefs.SetInt("Insane Battle", insaneBattle);

        if (insaneBattle == 1 && !PlayerPrefs.HasKey("Achivment_10"))
        {
            PlayerPrefs.SetString("Achivment_10", "YES");
            achivSprite.sprite = sprites[10];
            achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Безумная битва" : "Insane Battle";
            StartCoroutine(ShowAchivUnlocked());
        }
    }

    public void AggressiveCapture()
    {
        aggressiveCapture++;
        PlayerPrefs.SetInt("Aggressive Capture", aggressiveCapture);

        if (aggressiveCapture == 100 && !PlayerPrefs.HasKey("Achivment_11"))
        {
            PlayerPrefs.SetString("Achivment_11", "YES");
            achivSprite.sprite = sprites[11];
            achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Агрессивный захват" : "Aggressive capture";
            StartCoroutine(ShowAchivUnlocked());
        }
    }

    public void Captain()
    {
        captain++;
        PlayerPrefs.SetInt("Captain", captain);

        if (captain == 30 && !PlayerPrefs.HasKey("Achivment_12"))
        {
            PlayerPrefs.SetString("Achivment_12", "YES");
            achivSprite.sprite = sprites[12];
            achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Капитан" : "Captain";
            StartCoroutine(ShowAchivUnlocked());
        }
    }

    public void Colonel()
    {
        colonel++;
        PlayerPrefs.SetInt("Colonel", colonel);

        if (colonel == 20 && !PlayerPrefs.HasKey("Achivment_13"))
        {
            PlayerPrefs.SetString("Achivment_13", "YES");
            achivSprite.sprite = sprites[13];
            achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Полковник" : "Colonel";
            StartCoroutine(ShowAchivUnlocked());
        }
    }

    public void Marshal()
    {
        marshal++;
        PlayerPrefs.SetInt("Marshal", marshal);

        if (colonel == 10 && !PlayerPrefs.HasKey("Achivment_14"))
        {
            PlayerPrefs.SetString("Achivment_14", "YES");
            achivSprite.sprite = sprites[14];
            achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Маршал" : "Marshal";
            StartCoroutine(ShowAchivUnlocked());
        }
    }

    public void GainAchivmentForUpgradeCastle(int index)
    {
        switch (index)
        {
            case 0:
                senior++;
                PlayerPrefs.SetInt("Senior", senior);

                if (colonel == 30 && !PlayerPrefs.HasKey("Achivment_15"))
                {
                    PlayerPrefs.SetString("Achivment_15", "YES");
                    achivSprite.sprite = sprites[15];
                    achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Сеньор" : "Senior";
                    StartCoroutine(ShowAchivUnlocked());
                }
                break;
            case 1:
                feudal++;
                PlayerPrefs.SetInt("Feudal", feudal);

                if (colonel == 40 && !PlayerPrefs.HasKey("Achivment_16"))
                {
                    PlayerPrefs.SetString("Achivment_16", "YES");
                    achivSprite.sprite = sprites[16];
                    achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Феодал" : "Feudal";
                    StartCoroutine(ShowAchivUnlocked());
                }
                break;
            case 2:
                monarch++;
                PlayerPrefs.SetInt("Monarch", monarch);

                if (colonel == 50 && !PlayerPrefs.HasKey("Achivment_17"))
                {
                    PlayerPrefs.SetString("Achivment_17", "YES");
                    achivSprite.sprite = sprites[17];
                    achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Монарх" : "Monarch";
                    StartCoroutine(ShowAchivUnlocked());
                }
                break;
        }
    }
    public void GainAchivmentForEvolution(int ageIndex)
    {
        switch (ageIndex)
        {
                case 0:
                theStrongestThug++;
                PlayerPrefs.SetInt("The Strongest Thug", theStrongestThug);

                if (theStrongestThug == 60 && !PlayerPrefs.HasKey("Achivment_18"))
                {
                    PlayerPrefs.SetString("Achivment_18", "YES");
                    achivSprite.sprite = sprites[18];
                    achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Сильнейший громила" : "The Strongest Thug";
                    StartCoroutine(ShowAchivUnlocked());
                }
                break;
            case 1:
                theEmperorOfHeaven++;
                PlayerPrefs.SetInt("The Emperor of Heaven", theEmperorOfHeaven);

                if (theEmperorOfHeaven == 50 && !PlayerPrefs.HasKey("Achivment_19"))
                {
                    PlayerPrefs.SetString("Achivment_19", "YES");
                    achivSprite.sprite = sprites[19];
                    achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Император поднебесной" : "The Emperor of Heaven";
                    StartCoroutine(ShowAchivUnlocked());
                }
                break;
            case 2:
                theKingChosenByGod++;
                PlayerPrefs.SetInt("The King Chosen by God", theKingChosenByGod);

                if (theKingChosenByGod == 40 && !PlayerPrefs.HasKey("Achivment_20"))
                {
                    PlayerPrefs.SetString("Achivment_20", "YES");
                    achivSprite.sprite = sprites[20];
                    achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Король выбранный богом" : "The King chosen by God";
                    StartCoroutine(ShowAchivUnlocked());
                }
                break;
            case 3:
                thePresidentOfThePlanet++;
                PlayerPrefs.SetInt("The President of the Planet", thePresidentOfThePlanet);

                if (thePresidentOfThePlanet == 30 && !PlayerPrefs.HasKey("Achivment_21"))
                {
                    PlayerPrefs.SetString("Achivment_21", "YES");
                    achivSprite.sprite = sprites[21];
                    achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Президент планеты" : "The President of the planet";
                    StartCoroutine(ShowAchivUnlocked());
                }
                break;
            case 4:
                theMasterOfTheUniverse++;
                PlayerPrefs.SetInt("The Master of the Universe", theMasterOfTheUniverse);

                if (theMasterOfTheUniverse == 20 && !PlayerPrefs.HasKey("Achivment_22"))
                {
                    PlayerPrefs.SetString("Achivment_22", "YES");
                    achivSprite.sprite = sprites[22];
                    achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Повелитель вселенной" : "The Master of the Universe";
                    StartCoroutine(ShowAchivUnlocked());
                }
                break;
            case 5:
                theDeityOfTheAbyss++;
                PlayerPrefs.SetInt("The Deity of the Abyss", theDeityOfTheAbyss);

                if (theDeityOfTheAbyss == 10 && !PlayerPrefs.HasKey("Achivment_23"))
                {
                    PlayerPrefs.SetString("Achivment_23", "YES");
                    achivSprite.sprite = sprites[23];
                    achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Божество из бездны" : "The Deity of the Abyss";
                    StartCoroutine(ShowAchivUnlocked());
                }
                break;
        }
    }
    public void ThePinnacleOfEvolution()
    {
        thePinnacleOfEvolution++;
        PlayerPrefs.SetInt("The Pinnacle of Evolution", thePinnacleOfEvolution);

        if (thePinnacleOfEvolution == 15 && !PlayerPrefs.HasKey("Achivment_24"))
        {
            PlayerPrefs.SetString("Achivment_24", "YES");
            achivSprite.sprite = sprites[24];
            achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Вершина эволюции" : "The pinnacle of evolution";
            StartCoroutine(ShowAchivUnlocked());
        }
    }

    public void MessengerWithTidings()
    {
        aMessengerWithTidings++;
        PlayerPrefs.SetInt("A messenger with tidings", aMessengerWithTidings);

        if (aMessengerWithTidings == 3 && !PlayerPrefs.HasKey("Achivment_25"))
        {
            PlayerPrefs.SetString("Achivment_25", "YES");
            achivSprite.sprite = sprites[25];
            achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Гонец с вестями" : "A messenger with tidings";
            StartCoroutine(ShowAchivUnlocked());
        }
    }

    public void GainAchivmentsForMoney(int playerMoney)
    {
        if (playerMoney >= 5000 && !PlayerPrefs.HasKey("Achivment_26"))
        {
            PlayerPrefs.SetString("Achivment_26", "YES");
            achivSprite.sprite = sprites[26];
            achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Подать" : "Submit";
            StartCoroutine(ShowAchivUnlocked());
        }
        else if (playerMoney >= 10000 && !PlayerPrefs.HasKey("Achivment_27"))
        {
            PlayerPrefs.SetString("Achivment_27", "YES");
            achivSprite.sprite = sprites[27];
            achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Оброк" : "Servage";
            StartCoroutine(ShowAchivUnlocked());
        }
        else if (playerMoney >= 20000 && !PlayerPrefs.HasKey("Achivment_28"))
        {
            PlayerPrefs.SetString("Achivment_28", "YES");
            achivSprite.sprite = sprites[28];
            achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Великая дань" : "Great Tribute";
            StartCoroutine(ShowAchivUnlocked());
        }
    }

    public void WorldTreasures(int money)
    {
        worldTreasures += money;
        PlayerPrefs.SetInt("World Treasures", worldTreasures);

        if (worldTreasures >= 100000 && !PlayerPrefs.HasKey("Achivment_29"))
        {
            PlayerPrefs.SetString("Achivment_29", "YES");
            achivSprite.sprite = sprites[29];
            achivNameText.text = Application.systemLanguage == SystemLanguage.Russian ? "Мировые сокровища" : "World Treasures";
            StartCoroutine(ShowAchivUnlocked());
        }
    }

    public IEnumerator ShowAchivUnlocked()
    {
        achivShow.SetActive(true);
        yield return new WaitForSeconds(1f);
        achivShow.SetActive(false);
    }

}
