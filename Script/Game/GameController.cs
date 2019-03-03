using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum StartMoney
{
    player = 2000,
    enemy = 2000
}

public enum GameMode
{
    Standart = 0, CuptureTower = 1, Custom = 2 
}

public class GameController : MonoBehaviour {

    private LocationController location;

    private Image playerEvolutionBar;
    private GameObject playerButtonUlt;
    private Text playerTextMoneyValue;
    private Text playerTextLimitedUnit;

    private Image enemyrEvolutionBar;
    private Text enemyTextMoneyValue;
    private Text enemyTextLimitedUnit;

    private Image playerButtonImage;
    private int evolution = 0;

    public GameObject locationPrefab;
    public GameObject towerPrefab;

    [Header("<-------GUI Elements------->")]
    [Header("Evolution Bar")]
    public Image castle_1EvolutionBar;
    public Image castle_2EvolutionBar;
    [Header("Ult Button")]
    public GameObject castle_1Button;
    public GameObject castle_2Button;
    [Header("Info Text Money")]
    public Text info_1TextMoneyValue;
    public Text info_2TextMoneyValue;
    [Header("Info Text Limited Unit")]
    public Text info_1TextLimitedUnit;
    public Text info_2TextLimitedUnit;
    [Header("Info Panel")]
    public GameObject info_1;
    public GameObject info_2;
    [Header("Panel")]
    public GameObject losePanel;

    [Header("<-------Game Data------->")]
    public GameMode gameMode;

    [Header("Age")]
    public Unit.Age playerAge;
    public Unit.Age enemyAge;

    [Header("Castle LVL")]
    [Range(0, 3)]
    public int playerCasleLVL;
    [Range(0, 3)]
    public int enemyCasleLVL;

    [Header("Money")]
    public int playerMoney;
    public int enemyMoney;
    [Header("Evolution Points")]
    public float playerEvolutionPoints;
    public float enemyEvolutionPoints;
    [Header("Player Limitd Unit")]
    public int playerLimitedUnit;
    public int playerLimitedUnitMax;
    [Header("Enemy Limitd Unit")]
    public int enemyLimited;
    public int enemyLimitedMax;

    [Header("<-------Ult Sprite & Prefabs------->")]
    public Sprite castleSprite;
    public Sprite[] sprites;
    public GameObject[] ultPrefabs;

    public static GameController instance;
    [Header("Ult process")]
    public bool playerUlt;
    public bool enemyUlt;

    private void Awake()
    {
        instance = FindObjectOfType<GameController>();
        Instantiate(locationPrefab);
        SetupGameDate();
    }

    private void Start()
    {
        playerMoney = (int)StartMoney.player;
        enemyMoney = (int)StartMoney.enemy;

        InfoUpdate();
        EvolutionAge();

        StartCoroutine(AutoIncomeCourutine());
    }

    private void Update()
    {
        ActivButtonUlt();
        //Запуск Ультимейта для противника
        CheckEnemyUlt();
        //Эволюция Игрока
        if (playerEvolutionPoints >= PointForEvolution((int)playerAge)  && (int)playerAge < 5)
        {
            playerEvolutionPoints = 0;
            playerEvolutionBar.fillAmount = 0;

            playerAge++;

            EvolutionAge();
        }
        //Эволюция Противника
        if (enemyEvolutionPoints >= PointForEvolution((int)enemyAge) && (int)enemyAge < 5)
        {
            enemyEvolutionPoints = 0;
            enemyrEvolutionBar.fillAmount = 0;
            enemyAge++;
            enemyUlt = false;

            EnemyCastleUP();
            UnitsController.instance.UnitEvolution(true);
            CannonController.instance.ChangeCannon(true);
        }
    }

    private void SetupGameDate()
    {
        location = FindObjectOfType<LocationController>();

        gameMode = (GameMode)PlayerPrefs.GetInt("GameMode");

        if (gameMode == GameMode.CuptureTower)
            Instantiate(towerPrefab);
        else if (gameMode == GameMode.Custom)
        {
            enemyAge = (Unit.Age)PlayerPrefs.GetInt("EnemyAge");
            playerAge = (Unit.Age)PlayerPrefs.GetInt("PlayerAge");
            enemyCasleLVL = PlayerPrefs.GetInt("EnemyCastleLVL");
            if (PlayerPrefs.GetString("EnableTower") == "YES")
                Instantiate(towerPrefab);
            EnemyCastleUP();
        }

        playerEvolutionBar = PlayerPrefs.GetString("Side") == "Left" ? castle_1EvolutionBar : castle_2EvolutionBar;
        playerButtonUlt = PlayerPrefs.GetString("Side") == "Left" ? castle_1Button : castle_2Button;
        playerTextMoneyValue = PlayerPrefs.GetString("Side") == "Left" ? info_1TextMoneyValue : info_2TextMoneyValue;
        playerTextLimitedUnit = PlayerPrefs.GetString("Side") == "Left" ? info_1TextLimitedUnit : info_2TextLimitedUnit;

        enemyrEvolutionBar = PlayerPrefs.GetString("Side") == "Left" ? castle_2EvolutionBar : castle_1EvolutionBar;
        enemyTextMoneyValue = PlayerPrefs.GetString("Side") == "Left" ? info_2TextMoneyValue : info_1TextMoneyValue;
        enemyTextLimitedUnit = PlayerPrefs.GetString("Side") == "Left" ? info_2TextLimitedUnit : info_1TextLimitedUnit;

        bool state = PlayerPrefs.GetString("Side") == "Left" ? true : false;

        info_1.SetActive(state);
        info_2.SetActive(!state);

        playerButtonImage = playerButtonUlt.transform.Find("Castle_Image").GetComponent<Image>();
        if (PlayerPrefs.GetString("Side") == "Left")
        {
            GameObject.Find("EvolutionBar_2").SetActive(false);
            castle_2Button.SetActive(false);
        }
        else
        {
            GameObject.Find("EvolutionBar_1").SetActive(false);
            castle_1Button.SetActive(false);
        }
    }

    private void ActivButtonUlt()
    {
        //Активация Ультимейта!!! Смерть нашим врагам, во имя высшего блага!
        if (playerEvolutionBar.fillAmount >= 0.65f)
        {
            playerEvolutionBar.GetComponent<Animator>().SetBool("Fade", true);

            playerButtonUlt.GetComponent<Button>().interactable = true;
            playerButtonImage.sprite = sprites[(int)playerAge];
        }
        else
        {
            playerEvolutionBar.GetComponent<Animator>().SetBool("Fade", false);

            playerButtonUlt.GetComponent<Button>().interactable = false;
            playerButtonImage.sprite = castleSprite;
        }
    }

    private void EvolutionAge()
    {
        location.Change((int)playerAge);
        location.CastleUP((int)playerAge, playerCasleLVL);

        UnitsController.instance.UnitEvolution(false);
        CannonController.instance.ChangeCannon(false);
        AchivmentsController.instance.GainAchivmentForEvolution((int)playerAge);

        evolution++;
        if(evolution == 6) //проверка на то, пройдены ли все этапы эволюции
            AchivmentsController.instance.ThePinnacleOfEvolution();
    }

    private void CheckEnemyUlt()
    {
        if (enemyrEvolutionBar.fillAmount >= 0.65f && enemyMoney <= 1000 && UnitsController.instance.PlayerUnits.Count >= 10 && !enemyUlt)
        {
            enemyUlt = true;
            UnitsController.instance.UltimateResolve(false);
            GameSettings.instance.SoundUlt((int)enemyAge);
            StartCoroutine(EnemyUltCourutine());
            InfoUpdate();
        }
    }

    private float PointForEvolution(int ageIndex)
    {
        float[] pointForEvolution = { 7000f, 8000f, 9000f, 10000f, 11000f, 12000f };
        return pointForEvolution[ageIndex];
    }

    public void Ult()
    {
        UnitsController.instance.UltimateResolve(true);
        GameSettings.instance.SoundUlt((int)playerAge);
        AchivmentsController.instance.NucklerBomb();
        StartCoroutine(PlayerUltCourutine());
        InfoUpdate();
    }

    public void Win()
    {
        GameSettings.instance.SoundInterface(5);
        CheckWinAchivment();
        StartCoroutine(WinCourutine());
    }

    IEnumerator WinCourutine()
    {
        PlayerPrefs.SetString("Win", "YES!");
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(0);
    }

    private void CheckWinAchivment()
    {
        AchivmentsController.instance.AggressiveCapture();
        switch (gameMode)
        {
            case GameMode.Standart:
                AchivmentsController.instance.Duelist();
                break;
            case GameMode.CuptureTower:
                AchivmentsController.instance.Invader();
                break;
            case GameMode.Custom:
                AchivmentsController.instance.InsaneBattle();
                break;
        }

        if (gameMode != GameMode.Custom)
        {
            Difficult difficult = FindObjectOfType<BotController>().difficultBot;
            switch (difficult)
            {
                case Difficult.Easy:
                    AchivmentsController.instance.Captain();
                    break;
                case Difficult.Normal:
                    AchivmentsController.instance.Colonel();
                    break;
                case Difficult.Hard:
                    AchivmentsController.instance.Marshal();
                    break;
            }
        }
    }

    public void Lose()
    {
        Time.timeScale = 0;
        losePanel.SetActive(true);
        GameSettings.instance.SoundInterface(4);
    }

    public void InfoUpdate()
    {
        playerTextMoneyValue.text = playerMoney.ToString();
        enemyTextMoneyValue.text = enemyMoney.ToString();

        playerTextLimitedUnit.text = playerLimitedUnit.ToString() + "/" + playerLimitedUnitMax.ToString();
        enemyTextLimitedUnit.text = enemyLimited.ToString() + "/" + enemyLimitedMax.ToString();

        playerEvolutionBar.fillAmount = playerEvolutionPoints / PointForEvolution((int)playerAge);
        enemyrEvolutionBar.fillAmount = enemyEvolutionPoints / PointForEvolution((int)enemyAge);

        AchivmentsController.instance.GainAchivmentsForMoney(playerMoney);
    }

    public void CastleUpgrade(int cost, int lvl)
    {
        playerCasleLVL = lvl;
        playerMoney -= cost;
        location.CastleUP((int)playerAge, playerCasleLVL);
        InfoUpdate();
    }

    public void EnemyCastleUP()
    {
        location.EnemyCastleUP((int)enemyAge, enemyCasleLVL);
    }

    IEnumerator PlayerUltCourutine()
    {
        Transform camPos = Camera.main.transform;
        playerEvolutionPoints = PointForEvolution((int)playerAge) * 0.35f;
        playerUlt = true;
        GameObject Ult = Instantiate(ultPrefabs[(int)playerAge], new Vector3(camPos.position.x, ultPrefabs[(int)playerAge].transform.position.y, 0), Quaternion.identity);
        Ult.SetActive(true);
        yield return new WaitForSeconds(5f);
        Destroy(Ult);
        playerUlt = false;
    }

    IEnumerator EnemyUltCourutine()
    {
        Transform camPos = Camera.main.transform;
        enemyEvolutionPoints = PointForEvolution((int)enemyAge) * 0.35f;
        GameObject Ult = Instantiate(ultPrefabs[(int)enemyAge], new Vector3(camPos.position.x, ultPrefabs[(int)enemyAge].transform.position.y, 0), Quaternion.identity);
        Ult.SetActive(true);
        yield return new WaitForSeconds(5f);
        Destroy(Ult);
    }

    IEnumerator AutoIncomeCourutine()
    {
        while (true)
        {
            playerMoney++;
            enemyMoney = enemyMoney + 6;

            playerTextMoneyValue.text = playerMoney.ToString();
            enemyTextMoneyValue.text = enemyMoney.ToString();

            AchivmentsController.instance.WorldTreasures(1);
            yield return new WaitForSeconds(1f);
        }
    }
}
