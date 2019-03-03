using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ScreenSide
{
    Left = 0, Right =2
}

public class CustomeGameScript : MonoBehaviour {

    [Header("Enemy Settings")]
    public Difficult difficult;
    [Range(0,3)]
    public int enemyCastleLvL;
    public Unit.Age enemyAge;
    [Header("PlayerSettings")]
    public ScreenSide positionOnScreen;
    public Unit.Age playerAge;
    [Header("Tower")]
    public bool towerEnable;

    [Header("Text Game Object")]
    public Text difficultText;
    public Text enemyCastleLvlText;
    public Text enemyAgeText;
    public Text positionOnScreenText;
    public Text playerAgeText;
    public Text towerEnableText;

    private void Start()
    {
        DefaltSettings();
    }
    //Переключатель для Уровня сложности
    public void DifficultSwitchLeft()
    {
        switch (difficult)
        {
            case Difficult.Easy:
                difficult = Difficult.Hard;
                difficultText.text = Application.systemLanguage == SystemLanguage.Russian ? "СЛОЖНО" : "HARD";
                PlayerPrefs.SetInt("Dufficult", (int)difficult);
                break;
            case Difficult.Normal:
                difficult = Difficult.Easy;
                difficultText.text = Application.systemLanguage == SystemLanguage.Russian ? "ПРОСТО" : "SIMPLE";
                PlayerPrefs.SetInt("Dufficult", (int)difficult);
                break;
            case Difficult.Hard:
                difficult = Difficult.Normal;
                difficultText.text = Application.systemLanguage == SystemLanguage.Russian ? "НОРМАЛЬНО" : "NORMAL";
                PlayerPrefs.SetInt("Dufficult", (int)difficult);
                break;
        }
    }
    public void DifficultSwitchRight()
    {
        switch (difficult)
        {
            case Difficult.Easy:
                difficult = Difficult.Normal;
                difficultText.text = Application.systemLanguage == SystemLanguage.Russian ? "НОРМАЛЬНО" : "NORMAL";
                PlayerPrefs.SetInt("Dufficult", (int)difficult);
                break;
            case Difficult.Normal:
                difficult = Difficult.Hard;
                difficultText.text = Application.systemLanguage == SystemLanguage.Russian ? "СЛОЖНО" : "HARD";
                PlayerPrefs.SetInt("Dufficult", (int)difficult);
                break;
            case Difficult.Hard:
                difficult = Difficult.Easy;
                difficultText.text = Application.systemLanguage == SystemLanguage.Russian ? "ПРОСТО" : "SIMPLE";
                PlayerPrefs.SetInt("Dufficult", (int)difficult);
                break;
        }
    }
    //Переключатель для уровня крепости противника
    public void CastleLVLSwitchLeft()
    {
        switch (enemyCastleLvL)
        {
            case 0:
                enemyCastleLvL = 3;
                enemyCastleLvlText.text = enemyCastleLvL.ToString();
                PlayerPrefs.SetInt("EnemyCastleLVL", enemyCastleLvL);
                break;
            case 1:
                enemyCastleLvL = 0;
                enemyCastleLvlText.text = enemyCastleLvL.ToString();
                PlayerPrefs.SetInt("EnemyCastleLVL", enemyCastleLvL);
                break;
            case 2:
                enemyCastleLvL = 1;
                enemyCastleLvlText.text = enemyCastleLvL.ToString();
                PlayerPrefs.SetInt("EnemyCastleLVL", enemyCastleLvL);
                break;
            case 3:
                enemyCastleLvL = 2;
                enemyCastleLvlText.text = enemyCastleLvL.ToString();
                PlayerPrefs.SetInt("EnemyCastleLVL", enemyCastleLvL);
                break;
        }
    }
    public void CastleLVLSwitchRight()
    {
        switch (enemyCastleLvL)
        {
            case 0:
                enemyCastleLvL = 1;
                enemyCastleLvlText.text = enemyCastleLvL.ToString();
                PlayerPrefs.SetInt("EnemyCastleLVL", enemyCastleLvL);
                break;
            case 1:
                enemyCastleLvL = 2;
                enemyCastleLvlText.text = enemyCastleLvL.ToString();
                PlayerPrefs.SetInt("EnemyCastleLVL", enemyCastleLvL);
                break;
            case 2:
                enemyCastleLvL = 3;
                enemyCastleLvlText.text = enemyCastleLvL.ToString();
                PlayerPrefs.SetInt("EnemyCastleLVL", enemyCastleLvL);
                break;
            case 3:
                enemyCastleLvL = 0;
                enemyCastleLvlText.text = enemyCastleLvL.ToString();
                PlayerPrefs.SetInt("EnemyCastleLVL", enemyCastleLvL);
                break;
        }
    }
    //Переключатель Века для противника
    public void EnemyAgeSwitchLeft()
    {
        switch (enemyAge)
        {
            case Unit.Age.StoneAge:
                enemyAge = Unit.Age.AncientGod;
                enemyAgeText.text  = Application.systemLanguage == SystemLanguage.Russian ? "ДРЕВНИЕ БОГИ" : "ANCIENT GOD";
                PlayerPrefs.SetInt("EnemyAge", (int)enemyAge);
                break;
            case Unit.Age.China:
                enemyAge = Unit.Age.StoneAge;
                enemyAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "КАМЕННЫЙ ВЕК" : "STONE AGE";
                PlayerPrefs.SetInt("EnemyAge", (int)enemyAge);
                break;
            case Unit.Age.MiddeleAge:
                enemyAge = Unit.Age.China;
                enemyAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "КИТАЙ" : "CHINA";
                PlayerPrefs.SetInt("EnemyAge", (int)enemyAge);
                break;
            case Unit.Age.Nowadays:
                enemyAge = Unit.Age.MiddeleAge;
                enemyAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "СРЕДИНИЕ ВЕКА" : "MIDDLE AGE";
                PlayerPrefs.SetInt("EnemyAge", (int)enemyAge);
                break;
            case Unit.Age.Future:
                enemyAge = Unit.Age.Nowadays;
                enemyAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "НАШИ ДНИ" : "NOWADAYS";
                PlayerPrefs.SetInt("EnemyAge", (int)enemyAge);
                break;
            case Unit.Age.AncientGod:
                enemyAge = Unit.Age.Future;
                enemyAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "БУДУЩИЕ" : "FUTURE";
                PlayerPrefs.SetInt("EnemyAge", (int)enemyAge);
                break;
        }
    }
    public void EnemyAgeSwitchRight()
    {
        switch (enemyAge)
        {
            case Unit.Age.StoneAge:
                enemyAge = Unit.Age.China;
                enemyAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "КИТАЙ" : "CHINA";
                PlayerPrefs.SetInt("EnemyAge", (int)enemyAge);
                break;
            case Unit.Age.China:
                enemyAge = Unit.Age.MiddeleAge;
                enemyAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "СРЕДИНИЕ ВЕКА" : "MIDDLE AGE";
                PlayerPrefs.SetInt("EnemyAge", (int)enemyAge);
                break;
            case Unit.Age.MiddeleAge:
                enemyAge = Unit.Age.Nowadays;
                enemyAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "НАШИ ДНИ" : "NOWADAYS";
                PlayerPrefs.SetInt("EnemyAge", (int)enemyAge);
                break;
            case Unit.Age.Nowadays:
                enemyAge = Unit.Age.Future;
                enemyAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "БУДУЩИЕ" : "FUTURE";
                PlayerPrefs.SetInt("EnemyAge", (int)enemyAge);
                break;
            case Unit.Age.Future:
                enemyAge = Unit.Age.AncientGod;
                enemyAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "ДРЕВНИЕ БОГИ" : "ANCIENT GOD";
                PlayerPrefs.SetInt("EnemyAge", (int)enemyAge);
                break;
            case Unit.Age.AncientGod:
                enemyAge = Unit.Age.StoneAge;
                enemyAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "КАМЕННЫЙ ВЕК" : "STONE AGE";
                PlayerPrefs.SetInt("EnemyAge", (int)enemyAge);
                break;
        }
    }
    //Переключение позии игрока на Экране
    public void ScreenSideSwitch()
    {
        switch (positionOnScreen)
        {
            case ScreenSide.Left:
                positionOnScreen = ScreenSide.Right;
                positionOnScreenText.text = Application.systemLanguage == SystemLanguage.Russian ? "СПРАВА" : "RIGHT";
                PlayerPrefs.SetString("Side", "Right");
                break;
            case ScreenSide.Right:
                positionOnScreen = ScreenSide.Left;
                positionOnScreenText.text = Application.systemLanguage == SystemLanguage.Russian ? "СЛЕВА" : "LEFT";
                PlayerPrefs.SetString("Side", "Left");
                break;
        }
    }
    //Переключетель века игрока
    public void PlayerAgeSwitchLeft()
    {
        switch (playerAge)
        {
            case Unit.Age.StoneAge:
                playerAge = Unit.Age.AncientGod;
                playerAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "ДРЕВНИЕ БОГИ" : "ANCIENT GOD";
                PlayerPrefs.SetInt("PlayerAge", (int)playerAge);
                break;
            case Unit.Age.China:
                playerAge = Unit.Age.StoneAge;
                playerAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "КАМЕННЫЙ ВЕК" : "STONE AGE";
                PlayerPrefs.SetInt("PlayerAge", (int)playerAge);
                break;
            case Unit.Age.MiddeleAge:
                playerAge = Unit.Age.China;
                playerAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "КИТАЙ" : "CHINA";
                PlayerPrefs.SetInt("PlayerAge", (int)playerAge);
                break;
            case Unit.Age.Nowadays:
                playerAge = Unit.Age.MiddeleAge;
                playerAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "СРЕДИНИЕ ВЕКА" : "MIDDLE AGE";
                PlayerPrefs.SetInt("PlayerAge", (int)playerAge);
                break;
            case Unit.Age.Future:
                playerAge = Unit.Age.Nowadays;
                playerAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "НАШИ ДНИ" : "NOWADAYS";
                PlayerPrefs.SetInt("PlayerAge", (int)playerAge);
                break;
            case Unit.Age.AncientGod:
                playerAge = Unit.Age.Future;
                playerAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "БУДУЩИЕ" : "FUTURE";
                PlayerPrefs.SetInt("PlayerAge", (int)playerAge);
                break;
        }
    }
    public void PlayerAgeSwitchRight()
    {
        switch (playerAge)
        {
            case Unit.Age.StoneAge:
                playerAge = Unit.Age.China;
                playerAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "КИТАЙ" : "CHINA";
                PlayerPrefs.SetInt("PlayerAge", (int)playerAge);
                break;
            case Unit.Age.China:
                playerAge = Unit.Age.MiddeleAge;
                playerAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "СРЕДИНИЕ ВЕКА" : "MIDDLE AGE";
                PlayerPrefs.SetInt("PlayerAge", (int)playerAge);
                break;
            case Unit.Age.MiddeleAge:
                playerAge = Unit.Age.Nowadays;
                playerAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "НАШИ ДНИ" : "NOWADAYS";
                PlayerPrefs.SetInt("PlayerAge", (int)playerAge);
                break;
            case Unit.Age.Nowadays:
                playerAge = Unit.Age.Future;
                playerAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "БУДУЩИЕ" : "FUTURE";
                PlayerPrefs.SetInt("PlayerAge", (int)playerAge);
                break;
            case Unit.Age.Future:
                playerAge = Unit.Age.AncientGod;
                playerAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "ДРЕВНИЕ БОГИ" : "ANCIENT GOD";
                PlayerPrefs.SetInt("PlayerAge", (int)playerAge);
                break;
            case Unit.Age.AncientGod:
                playerAge = Unit.Age.StoneAge;
                playerAgeText.text = Application.systemLanguage == SystemLanguage.Russian ? "КАМЕННЫЙ ВЕК" : "STONE AGE";
                PlayerPrefs.SetInt("PlayerAge", (int)playerAge);
                break;
        }
    }
    //Переключатель Башни
    public void TowerSwitch()
    {
        switch (towerEnable)
        {
            case false:
                towerEnable = true;
                towerEnableText.text = Application.systemLanguage == SystemLanguage.Russian ? "ДА" : "YES";
                PlayerPrefs.SetString("EnableTower","YES");
                break;
            case true:
                towerEnable = false;
                towerEnableText.text = Application.systemLanguage == SystemLanguage.Russian ? "НЕТ" : "NO";
                PlayerPrefs.SetString("EnableTower", "NO");
                break;
        }
    }

    //Закрыть Панель
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    //Начать Игру
    public void PlayCustomeMode()
    {
        PlayerPrefs.SetInt("GameMode", 2);
        SceneManager.LoadScene(1);
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
}
