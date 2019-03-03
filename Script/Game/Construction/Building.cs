using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Building : MonoBehaviour {

    [Header("Game Date")]
    public bool enemySide;

    [Header("Health")]
    public float healthMax;
    public float health;
    public bool objectDestroy;

    private Image castleHealthBar;

    private void Start()
    {
        health = healthMax;
        if (PlayerPrefs.GetString("Side") == "Left")
            castleHealthBar = !enemySide ? GameObject.Find("CasleBar_1").transform.Find("Image").GetComponent<Image>() : GameObject.Find("CasleBar_2").transform.Find("Image").GetComponent<Image>();
        else
            castleHealthBar = !enemySide ? GameObject.Find("CasleBar_2").transform.Find("Image").GetComponent<Image>() : GameObject.Find("CasleBar_1").transform.Find("Image").GetComponent<Image>();
    }
    private void Update()
    {
        if (castleHealthBar.fillAmount <= 0 && !enemySide && !objectDestroy)
        {
            GameController.instance.Lose();
            objectDestroy = !objectDestroy;
        }
        else if (castleHealthBar.fillAmount <= 0 && enemySide && !objectDestroy)
        {
            GameController.instance.Win();
            objectDestroy = !objectDestroy;
        }
    }

    public void CastleBarUpdate()
    {
        castleHealthBar.fillAmount = health / healthMax;
    }
}
