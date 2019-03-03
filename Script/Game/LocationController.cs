using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Age
{
    public string name;
    public Sprite backgroundSprite;
    public Sprite layer_1Sprite;
    public Sprite layer_2Sprite;
    public Sprite layer_3Sprite;
    public Sprite layer_4Sprite;
    public Sprite[] castleLVL;
}

public class LocationController : MonoBehaviour {
    [Header("Castle")]
    private GameObject playerCastle;
    private GameObject enemyCastle;

    [Header("Tower")]
    private GameObject tower;

    [Header("Age Sprite")]
    public Age[] ages;

    [Header("Location")]
    public SpriteRenderer backgound;
    public SpriteRenderer layer_1;
    public SpriteRenderer layer_2;
    public SpriteRenderer layer_3;
    public SpriteRenderer layer_4;

    [Header("Dicoration")]
    public GameObject[] dicorations;

    [Header("Point SpawnCastle")]
    public Transform[] pointsSpawnCastle1;
    public Transform[] pointsSpawnCastle2;

    private void Awake()
    {
        playerCastle = (PlayerPrefs.GetString("Side") == "Left") ? GameObject.Find("Castle_1") : GameObject.Find("Castle_2");
        enemyCastle = (PlayerPrefs.GetString("Side") == "Left") ? GameObject.Find("Castle_2") : GameObject.Find("Castle_1");

        enemyCastle.GetComponent<Building>().enemySide = true;
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            UnitsController.instance.lines[i].playerStartPoint = (PlayerPrefs.GetString("Side") == "Left") ? pointsSpawnCastle1[i] : pointsSpawnCastle2[i];
            UnitsController.instance.lines[i].enemyStartPoint = (PlayerPrefs.GetString("Side") == "Left") ? pointsSpawnCastle2[i] : pointsSpawnCastle1[i];
        }
    }

    public void Change(int agesIndex)
    {
        //Изменение основных слоев локации
        backgound.sprite = ages[agesIndex].backgroundSprite;

        layer_1.sprite = ages[agesIndex].layer_1Sprite;
        layer_2.sprite = ages[agesIndex].layer_2Sprite;
        layer_3.sprite = ages[agesIndex].layer_3Sprite;
        layer_4.sprite = ages[agesIndex].layer_4Sprite;

        //Переключениее дикорации в соответствии с веком
        foreach(GameObject dicoration in dicorations)
        {
            dicoration.SetActive(false);
        }

        dicorations[agesIndex].SetActive(true);
    }

    public void CastleUP(int agesIndex, int castleLVL)
    {
        playerCastle.GetComponent<SpriteRenderer>().sprite = ages[agesIndex].castleLVL[castleLVL];
    }

    public void EnemyCastleUP(int agesIndex, int castleLVL)
    {
        enemyCastle.GetComponent<SpriteRenderer>().sprite = ages[agesIndex].castleLVL[castleLVL];
    }
}
