using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficult // Уровень сложности
{
    Easy = 0, Normal = 1, Hard = 2
}
public enum State_Behaviour// Текущее состояние бота или его поведение!
{
    BuyUpgrade,
    Training,
    UpgradeCastle,
    ByuSellCannon  
}

[System.Serializable]
public class BotStackTrainUnit // Очередь тренеровки Юнита(анологично классу тренеровки юнитов для игрока - ButtonTrainUnit в TrainUnit)
{
    public string name;

    public int trainingCost;
    public int trainStack;
    public float trainTime;
    public float tempTime;

    public bool block;
    public bool training;
}


public class BotController : MonoBehaviour
{
    private LocationController location;

    public Difficult difficultBot;

    [Header("Unity Prefab")]
    public GameObject[] UnitPrefab;

    [Header("Stack Train Unit")]
    public BotStackTrainUnit[] botStackTrainUnits;

    [Header("Unit Upgrades")]
    public UnitUpgradePanel[] upgradePanel;

    [Header("Cannon Buy Panels")]
    public CannonUpgradePanel[] cannonPanel;

    [Header("State_Behaviour")]
    public State_Behaviour state;

    private readonly float delay=1f;

    private float tempTime;
    // Продумать хорошую механику бота
    /* Нужна проработка покупки апгрейдов и пушек, нужна проработка заказа юнитов, нужна проработка апгрейда замка */


    private void Awake()
    {
        float trainingMultiplayer = 0f;

        difficultBot = (Difficult)PlayerPrefs.GetInt("Dufficult");

        switch (difficultBot)
        {
            case Difficult.Easy:
                trainingMultiplayer = 1f;
                break;
            case Difficult.Normal:
                trainingMultiplayer = 0.75f;
                break;
            case Difficult.Hard:
                trainingMultiplayer = 0.5f;
                botStackTrainUnits[3].trainingCost = 300; // стоимость суперюнита на сложнов уровне
                break;
        }

        foreach (BotStackTrainUnit botStack in botStackTrainUnits)
        {
            botStack.trainTime *= trainingMultiplayer;
        }
    }

    private void Start()
    {
        location = FindObjectOfType<LocationController>();
        tempTime = delay;
    }

    private void LateUpdate()
    {
        switch (state) // Переключатель поведения бота
        {
            case State_Behaviour.Training:
                Traning();
                break;
            case State_Behaviour.BuyUpgrade:
                BuyUpgradeUnit();
                break;
            case State_Behaviour.ByuSellCannon:
                BuySellCannon();
                break;
            case State_Behaviour.UpgradeCastle:
                UpgradeCastle();
                break;
        }
    }

    private void Traning()
    {
        //tempTime -= Time.deltaTime;
        if (GameController.instance.playerLimitedUnit+1 > GameController.instance.enemyLimited)
        {
            int rand = Random.Range(0, botStackTrainUnits.Length);
            if (botStackTrainUnits[rand].block && GameController.instance.enemyMoney >= botStackTrainUnits[rand].trainingCost)
            {
                GameController.instance.enemyMoney -= botStackTrainUnits[rand].trainingCost;
                GameController.instance.InfoUpdate();
                botStackTrainUnits[rand].block = false;
            }
            else if (!botStackTrainUnits[rand].block && GameController.instance.enemyMoney >= botStackTrainUnits[rand].trainingCost && GameController.instance.enemyLimitedMax > GameController.instance.enemyLimited)
            {
                botStackTrainUnits[rand].trainStack++;

                GameController.instance.enemyMoney -= botStackTrainUnits[rand].trainingCost;
                GameController.instance.enemyLimited++;
                GameController.instance.InfoUpdate();

                if (!botStackTrainUnits[rand].training)
                {
                    StartCoroutine(BotTrainUinCourutine(rand));
                    botStackTrainUnits[rand].training = true;
                }
            }
            //tempTime = delay;
            //state = State_Behaviour.UpgradeCastle;
        }
        else
        {
            state = State_Behaviour.UpgradeCastle;
        }
    }

    private void BuyUpgradeUnit()
    {
        tempTime -= Time.deltaTime;
        if (tempTime <= 0f)
        {
            //Параметры необходимые для рандомного апгрейда
            int randPanel = Random.Range(0, upgradePanel.Length-1);
            int randUpgrade = Random.Range(0, upgradePanel[randPanel].upgrades.Length-1);

            //Попытка задать приоритет для апгрейдов, костыльная задача
            if (UnitsController.instance.incomeMultiplayerEnemy < 2f)
            {
                upgradePanel[4].EnemyUpgrade(0);
            }

            else
            {
                upgradePanel[randPanel].EnemyUpgrade(randUpgrade);
            }
            tempTime = delay;

            state = State_Behaviour.Training;
        }
    }

    private void BuySellCannon()
    {
        tempTime -= Time.deltaTime;
        if (tempTime <= 0f)
        {
            int enemyCasleLvl = GameController.instance.enemyCasleLVL;

            if (enemyCasleLvl > 0)
            {
                int randFloor = Random.Range(0, enemyCasleLvl);

                cannonPanel[randFloor].BuyCannonEnemy();
            }

            state = State_Behaviour.Training;
        }
    }

    private void UpgradeCastle()
    {
        tempTime -= Time.deltaTime;
        if (tempTime <= 0f)
        {
            int enemyCasleLvl = GameController.instance.enemyCasleLVL;

            if (GameController.instance.enemyCasleLVL < 3 && GameController.instance.enemyMoney >= CostCastle(enemyCasleLvl) )
            {
                GameController.instance.enemyMoney -= CostCastle(enemyCasleLvl);
                GameController.instance.enemyCasleLVL++;
                GameController.instance.EnemyCastleUP();
            }
            tempTime = delay;
            state = State_Behaviour.ByuSellCannon;
        }
    }

    private int  CostCastle(int castleLVL)
    {
        int[] cost = { 1500, 3200, 3500 }; // цена за апгрейд замка(записал чтобы лишний раз не оборащаться к панелькам как при покупках апгрейда или покупках пушек)
        return cost[castleLVL];
    }

    IEnumerator BotTrainUinCourutine(int indexUnit)
    {
        while (botStackTrainUnits[indexUnit].trainStack > 0)
        {
            yield return new WaitForSeconds(botStackTrainUnits[indexUnit].trainTime);
            botStackTrainUnits[indexUnit].trainStack--;

            GameObject unit;
            Transform spawnPoint = UnitsController.instance.SpawnUnit(true);

            if (indexUnit != 2)
                unit = Instantiate(UnitPrefab[indexUnit], spawnPoint.position, Quaternion.identity);
            else
                unit = Instantiate(UnitPrefab[indexUnit], spawnPoint.position + Vector3.up * 5, Quaternion.identity);

            unit.GetComponent<Unit>().age = GameController.instance.enemyAge;
            unit.GetComponent<Unit>().side = SideUnit.Enemy;
            unit.transform.parent = spawnPoint;

            unit.name = botStackTrainUnits[indexUnit].name;
        }
        botStackTrainUnits[indexUnit].training = false;
    }
}
