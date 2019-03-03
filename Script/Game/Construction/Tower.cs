using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    public enum Side
    {
        Tower, Enemy, Player
    }

    [Header("Game Data")]
    public Side side;

    [Header("Health")]
    public GameObject HealthBar;
    public float healthMax;
    public float health;

    [Header("Cannon")]
    public Cannon[] cannons;

    [Header("Point")]
    public List<Transform> points;

    private void Start()
    {
        ChengeSide();
        ResrtoreHealth();
    }

    public void ChengeSide()
    {
        foreach (Cannon cannon in cannons)
        {
            cannon.enemySide = side;
            cannon.gameObject.SetActive(true);
        }

        ResrtoreHealth();
    }

    public void TowerHealthBarUpdate()
    {
        HealthBar.transform.localScale = new Vector3(health / healthMax * 2f, 2f, 2f);
    }

    private void ResrtoreHealth()
    {
        health = healthMax;
        HealthBar.transform.localScale = new Vector3(health / healthMax * 2f, 2f, 2f);
    }

}
