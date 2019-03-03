using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Multiplier
{
    public string name;
    public float attackMultiplier = 1.0f;
    public float rangeMultiplier = 1.0f;
    public float attackSpeedMultiplier = 1.0f;
    public float defenceMultiplier = 1.0f;
    public float healthMultiplier = 1.0f;
}
[System.Serializable]
public class Line
{
    public string name;
    public Transform playerStartPoint;
    public Transform enemyStartPoint;
    public List<Unit> playerUnits;
    public List<Unit> enemyUnits;
    public Tower tower;
}

public class UnitsController : MonoBehaviour {

    public static UnitsController instance;

    [Tooltip("Multipllier stats player")]
    [Header("Main Upgrades")]
    public Multiplier[] multipliersPlayer;
    public Multiplier[] multipliersEnemy;

    public float incomeMultiplayerPlayer = 1.0f;
    public float incomeMultiplayerEnemy = 1.0f;


    [Header("Lines")]
    public Line[] lines;
    [Header("Player Units")]
    public List<Unit> PlayerUnits;
    [Header("Enemy Units")]
    public List<Unit> EnemyUnits;

    private void Start()
    {
        instance = FindObjectOfType<UnitsController>();
        if (FindObjectOfType<Tower>() != null)
        {
            foreach (Line line in lines)
            {
                line.tower = FindObjectOfType<Tower>();
            }
        }
    }

    public void UpdateStatsUnit(bool enemy)
    {
        foreach (Unit unit in !enemy ? PlayerUnits : EnemyUnits)
        {
            unit.MultiplirStats(enemy);
        }
    }

    public void UnitEvolution(bool enemy)
    {
        foreach (Unit unit in !enemy ? PlayerUnits : EnemyUnits)
        {
            unit.age++;
            unit.ChangeAge();
        }
    }

    public void UnitAddLine(Unit unit)
    {
        // Флаги для костылей
        bool addTruePlayer = true;
        bool addTrueEnemy = true;

        foreach (Line line in lines)
        {
            float minPlayer = Mathf.Min(lines[0].playerUnits.Count, lines[1].playerUnits.Count, lines[2].playerUnits.Count);
            float minEnemy = Mathf.Min(lines[0].enemyUnits.Count, lines[1].enemyUnits.Count, lines[2].enemyUnits.Count);

            if (line.playerUnits.Count <= minPlayer && unit.side == SideUnit.Player && addTruePlayer)
            {
                unit.line = line;
                line.playerUnits.Add(unit);
                addTruePlayer = false;
            }
            else if (line.enemyUnits.Count <= minEnemy && unit.side == SideUnit.Enemy && addTrueEnemy)
            {
                unit.line = line;
                line.enemyUnits.Add(unit);
                addTrueEnemy = false;
            }
        }
    }

    public void UnitChangeLine(Unit unit)
    {
        float distance = Mathf.Infinity;
        foreach (Unit ut in (unit.side == SideUnit.Player ? PlayerUnits : EnemyUnits))
        {
            if (unit.side == SideUnit.Player ? PlayerUnits.Count > 0 : EnemyUnits.Count > 0)
            {
                float tempdist = Mathf.Abs(unit.transform.position.x - ut.transform.position.x);
                if (tempdist < distance)
                {
                    unit.line = ut.line;
                }
            }
        }
    }

    public void UnitRemoveLine(Unit unit)
    {   
        foreach(Line line in lines)
        {
            if(unit.line == line && unit.side == SideUnit.Player)
            {
                line.playerUnits.Remove(unit);
                unit.line = null;
            }

            else if (unit.line == line && unit.side == SideUnit.Enemy)
            {
                line.enemyUnits.Remove(unit);
                unit.line = null;
            }
        }
    }

    public void UltimateResolve(bool enemy)
    {
        foreach(Unit unit in !enemy ? PlayerUnits : EnemyUnits)
        {
            unit.health = 0;
        }
    }

    public Transform SpawnUnit(bool enemy)
    {
        Transform point = null;

        // Флаги для костылей
        bool addTruePlayer = true;
        bool addTrueEnemy = true;

        foreach (Line line in lines)
        {
            float minPlayer = Mathf.Min(lines[0].playerUnits.Count, lines[1].playerUnits.Count, lines[2].playerUnits.Count);
            float minEnemy = Mathf.Min(lines[0].enemyUnits.Count, lines[1].enemyUnits.Count, lines[2].enemyUnits.Count);

            if (line.playerUnits.Count <= minPlayer && !enemy && addTruePlayer)
            {
                point = line.playerStartPoint;
                addTruePlayer = false;
            }

            else if (line.enemyUnits.Count <= minEnemy && enemy && addTrueEnemy)
            {
                point = line.enemyStartPoint;
                addTrueEnemy = false;
            }
        }
        return point;
    }
}
