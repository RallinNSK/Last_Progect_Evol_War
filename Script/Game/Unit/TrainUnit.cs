using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ButtonTrainUnit
{
    public string name;
    public Image timeCounter;
    public Text trainStackText;
    public bool block;

    public int trainingCost;
    public int trainStack;
    public float trainTime;

    public bool training;
}

public class TrainUnit : MonoBehaviour
{
    [Header("Training Button")]
    public ButtonTrainUnit[] trainUnits;

    [Header("Prefab Unit")]
    public GameObject[] units;

    [Header("Location")]
    private LocationController location;

    [Header("Unlock Panel")]
    public GameObject unlockSuperUnit;

    private void Start()
    {
        location = FindObjectOfType<LocationController>();
    }

    public void Training(int num)
    {
        if (trainUnits[num].block)
        {
            GameSettings.instance.SoundInterface(0);
            unlockSuperUnit.SetActive(true);
        }
        else if (GameController.instance.playerLimitedUnit < GameController.instance.playerLimitedUnitMax && GameController.instance.playerMoney >= trainUnits[num].trainingCost)
        {
            trainUnits[num].trainStack++;
            trainUnits[num].trainStackText.text = "x" + trainUnits[num].trainStack.ToString();
            GameController.instance.playerMoney -= trainUnits[num].trainingCost;
            GameController.instance.playerLimitedUnit++;
            // Проверка на достижение ачивки!
            AchivmentsController.instance.GainAchivmentForUnit(num);
            GameSettings.instance.SoundInterface(2);
            GameController.instance.InfoUpdate();

            if (!trainUnits[num].training)
            {
                StartCoroutine(Courutine(num));
                trainUnits[num].training = true;
            }
        }
        else
        {
            GameSettings.instance.SoundInterface(0);
        }
    }

    public void UnclockForCoin()
    {
        if(GameController.instance.playerMoney >= 400)
        {
            GameController.instance.playerMoney -= 400;
            trainUnits[3].block = false;
            unlockSuperUnit.SetActive(false);
        }
        GameSettings.instance.SoundInterface(0);
    }

    public void UnlockForReward()
    {
        //Вставка просмотра рекламы за просмотр видосика
        trainUnits[3].block = false;
        Destroy(unlockSuperUnit);
        AchivmentsController.instance.MessengerWithTidings(); //Проверка Ачивки

        GameSettings.instance.SoundInterface(0);
    }

    public void CloseUnlockPanel()
    {
        unlockSuperUnit.SetActive(false);
        GameSettings.instance.SoundInterface(0);
    }

    IEnumerator Courutine(int num)
    {
        while (trainUnits[num].trainStack > 0)
        {
            if (trainUnits[num].timeCounter.fillAmount < 1)
            {
                yield return new WaitForSeconds(0.1f);
                trainUnits[num].timeCounter.fillAmount += (1 / trainUnits[num].trainTime)/10;
            }
            else
            {
                trainUnits[num].trainStack--;
                trainUnits[num].trainStackText.text = "x" + trainUnits[num].trainStack.ToString();
                trainUnits[num].timeCounter.fillAmount = 0;


                GameObject unit;
                Transform spawnPoint = UnitsController.instance.SpawnUnit(false);

                if (num !=2)
                    unit = Instantiate(units[num], spawnPoint.position, Quaternion.identity);
                else
                    unit = Instantiate(units[num], spawnPoint.position + Vector3.up*5, Quaternion.identity);

                unit.GetComponent<Unit>().age = GameController.instance.playerAge;
                unit.GetComponent<Unit>().side = SideUnit.Player;
                unit.transform.parent = spawnPoint;

                unit.name = trainUnits[num].name;
            }
        }
        trainUnits[num].training = false;
    }
}
