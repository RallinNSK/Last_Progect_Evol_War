using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CannonBaseStat
{
    public string lvlName;
    [Tooltip("Min damage(Минимальный урон)")]
    public float damageMinBase;
    [Tooltip("Max damage(Маскимальный урон)")]
    public float damageMaxBase;
    [Tooltip("Range Attack")]
    public float rangeAttackBase;
    [Tooltip("Attack Speed")]
    public float attackSpeedBase;
}

public class Cannon : MonoBehaviour {

    [Header("Throwing Parametrs")]
    public Transform target;
    public Transform throwPoint;
    public GameObject projectile;
    public float timeTillHit = 1f;

    [Header("Base Stat")]
    public CannonBaseStat[] baseStat;

    [Header("Cannon leavel")]
    public int leavel;

    [Header("Cannon Stats")]
    public float damageMin;
    public float damageMax;
    public float rangeAttack;
    public float attackSpeed;

    public Tower.Side enemySide;

    public AudioClip clip;
    private Animator animator;
    private float tempAttackSpeed;
    private float distanceForTarget;

    public List<Unit> allTarget;

    private void Start()
    {
        animator = GetComponent<Animator>();
        SetupParam();
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion qt = Quaternion.AngleAxis(angle, Vector3.forward);
            float DressingDelta = Time.deltaTime * 200f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, qt , DressingDelta);

            GetComponent<SpriteRenderer>().flipY = vectorToTarget.x >= 0 ? false : true;
        }
    }

    private void LateUpdate()
    {
        FindClocedEnemy();
        if (distanceForTarget <= rangeAttack)
        {
            tempAttackSpeed -= Time.deltaTime;
            if (tempAttackSpeed <= 0)
            {
                animator.SetTrigger("Attack");
                tempAttackSpeed = attackSpeed;
                ProjectileThrow();
            }
        }
    }

    private void FindClocedEnemy()
    { 
        distanceForTarget = Mathf.Infinity;
        allTarget = new List<Unit>();

        switch (enemySide)
        {
            case Tower.Side.Tower:
                allTarget.AddRange(UnitsController.instance.PlayerUnits);
                allTarget.AddRange(UnitsController.instance.EnemyUnits);
                break;
            case Tower.Side.Enemy:
                allTarget = UnitsController.instance.PlayerUnits;
                break;
            case Tower.Side.Player:
                allTarget = UnitsController.instance.EnemyUnits;
                break;
        }

        // Если есть противники
        if (allTarget.Count > 0)
        {
            foreach (Unit at in allTarget)
            {
                float tempdist = Mathf.Abs(transform.position.x - at.transform.position.x);
                if (tempdist < distanceForTarget)
                {
                    distanceForTarget = tempdist;
                    target = at.transform;
                }
            }
        }
        else if (allTarget.Count == 0)
            target = null;
    }

    public void SetupParam()
    {
        damageMin = enemySide == Tower.Side.Player ? baseStat[leavel].damageMinBase * UnitsController.instance.multipliersPlayer[4].attackMultiplier: baseStat[leavel].damageMinBase;
        damageMax = enemySide == Tower.Side.Player ? baseStat[leavel].damageMaxBase * UnitsController.instance.multipliersPlayer[4].attackMultiplier : baseStat[leavel].damageMaxBase;
        rangeAttack = enemySide == Tower.Side.Player ? baseStat[leavel].rangeAttackBase * UnitsController.instance.multipliersPlayer[4].rangeMultiplier : baseStat[leavel].rangeAttackBase;
        attackSpeed = enemySide == Tower.Side.Player ? baseStat[leavel].attackSpeedBase * UnitsController.instance.multipliersPlayer[4].attackSpeedMultiplier : baseStat[leavel].attackSpeedBase;
    }

    void ProjectileThrow()
    {
        float xdistance;
        xdistance = target.position.x - throwPoint.position.x;

        float ydistance;
        ydistance = (target.position.y+0.8f) - throwPoint.position.y;

        float throwAngle; 
        throwAngle = Mathf.Atan((ydistance + 4.905f * (timeTillHit * timeTillHit)) / xdistance);

        float totalVelo = xdistance / (Mathf.Cos(throwAngle) * timeTillHit);

        float xVelo, yVelo;
        xVelo = totalVelo * Mathf.Cos(throwAngle);
        yVelo = totalVelo * Mathf.Sin(throwAngle);

        GameObject bulletInstance = Instantiate(projectile, throwPoint.position, gameObject.transform.rotation) as GameObject;
        bulletInstance.transform.parent = gameObject.transform;

        Rigidbody2D rigid;
        Projectile projectileScrit;

        rigid = bulletInstance.GetComponent<Rigidbody2D>();
        projectileScrit = bulletInstance.GetComponent<Projectile>();

        projectileScrit.target = target;
        projectileScrit.damageProjectile = UnityEngine.Random.Range(damageMin, damageMax);
        rigid.velocity = new Vector2(xVelo, yVelo);

        GameSettings.instance.SoundUnit(clip,gameObject.transform);
    }
} 
