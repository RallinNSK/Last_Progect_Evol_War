using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BaseStat
{
    [Tooltip("Min damage(Минимальный урон)")]
    public float damageMinBase;
    [Tooltip("Max damage(Маскимальный урон)")]
    public float damageMaxBase;
    [Tooltip("Min defence(Минимальная защита)")]
    public float defenceMinBase;
    [Tooltip("Max defence(Максимальная защита)")]
    public float defenceMaxBase;
    [Tooltip("Max value heath point unit(Максимальное значение здоровья юнита)")]
    public float healthMaxBase;
    [Tooltip("Range distance attack(Максимальная дистанция для аттаки)")]
    public float distanceAttackBase;
    [Tooltip("Valuation unit")]
    public int valuationBase;
}
public enum UnitType
{
    Footman, Rifleman, AirUnit, SuperUnit
}
public enum SideUnit
{
    Player=0, Enemy=1
}

public class Unit : MonoBehaviour {

    public enum Age
    {
        StoneAge = 0, China = 1, MiddeleAge = 2, Nowadays = 3, Future = 4, AncientGod = 5
    }

    [Header("Animator")]
    public Animator animator;

    [Header("HeathBar")]
    public GameObject heathBar;
    public Sprite hpPlayer;
    public Sprite hpEnemy;

    [Header("Damage show")]
    public GameObject damageShow;
    public Color playerUnitColor;
    public Color enemyUnitColor;

    [Header("Unit Type")]
    public UnitType unitType;
    public Age age;

    [Header("Sound Clip")]
    public AudioClip[] attacksClip;
    public AudioClip[] deathClip;

    [Header("Base Unit Param")]
    public BaseStat baseStat;

    [Header("Unit Params")]
    public float damageMin;
    public float damageMax;
    public float defenceMin;
    public float defenceMax;
    public float heathMax;
    public float health;
    public float distanceAttack;
    public float attackSpeed;
    public float moveSpeed;
    public int valuation;

    public SideUnit side;
    public bool fly;
    public bool canAttackFly;

    public Line line; // Просто данные о линии на которой находится Юнит

    public float timeTillHit = 1.0f; 
    float tempheathMax; // Трешь переменная для обработки текущего количесва ХП при апгрейде;

    [Header("Target")]
    public Transform target;
    public float distanceForTarget;
    [Header("Projectile")]
    public Transform spawnProjectailPoint;
    public GameObject[] projectalePefab;

    private float tempAttackSpeed;

    private void Start()
    {
        SetupParam();

        ChangeAge();
        HealtBarUpdate();
    }


    private void Update()
    {
        FindClosedEnemy();

        if (distanceForTarget < distanceAttack)
        {
            animator.SetBool("Attack", true);
            tempAttackSpeed -= Time.deltaTime;
            if (tempAttackSpeed <= 0)
            {
                ProjectileThrow();

                tempAttackSpeed = attackSpeed;
            }
        }
        else
        {
            animator.SetBool("Attack", false);
            if (canAttackFly)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime); 
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            }
        }

        //Смерть Юнита
        if (health <= 0)
        {
            if (side == SideUnit.Enemy)
            {
                GameController.instance.enemyLimited--;
                GameController.instance.enemyEvolutionPoints += 80;

                GameController.instance.playerEvolutionPoints += !GameController.instance.playerUlt ? 150 : 0;
                GameController.instance.playerMoney += valuation;
                AchivmentsController.instance.WorldTreasures(valuation);

                UnitsController.instance.EnemyUnits.Remove(this);
            }
            else if(side == SideUnit.Player)
            {
                GameController.instance.playerLimitedUnit--;
                GameController.instance.playerEvolutionPoints += 80;

                GameController.instance.enemyEvolutionPoints += !GameController.instance.enemyUlt ? 150 : 0;
                GameController.instance.enemyMoney += valuation;

                UnitsController.instance.PlayerUnits.Remove(this);
            }
            GameSettings.instance.SoundUnit(deathClip[Random.Range(0, 1)], gameObject.transform);
            GameController.instance.InfoUpdate();
            UnitsController.instance.UnitRemoveLine(this);
            Destroy(gameObject);
        }
    }

    private void ProjectileThrow()
    {
        float xdistance;
        xdistance = target.position.x - spawnProjectailPoint.position.x;

        float ydistance;
        ydistance = (target.position.y + 0.1f) - spawnProjectailPoint.position.y;
        
        float throwAngle;
        throwAngle = Mathf.Atan((ydistance + 4.905f * (timeTillHit * timeTillHit)) / xdistance);
        
        float totalVelo = xdistance / (Mathf.Cos(throwAngle) * timeTillHit);
        
        float xVelo, yVelo;

        xVelo = totalVelo * Mathf.Cos(throwAngle);
        yVelo = totalVelo * Mathf.Sin(throwAngle);
        
        GameObject bulletInstance = Instantiate(projectalePefab[(int)age], spawnProjectailPoint.position, Quaternion.identity);
        bulletInstance.transform.parent = gameObject.transform;
        bulletInstance.GetComponent<SpriteRenderer>().flipX = side == SideUnit.Enemy ? true : false; 

        Rigidbody2D rigid;

        Projectile projectileScrit;
        rigid = bulletInstance.GetComponent<Rigidbody2D>();
        projectileScrit = bulletInstance.GetComponent<Projectile>();

        projectileScrit.target = target;
        projectileScrit.damageProjectile = Random.Range(damageMin, damageMax);
        rigid.velocity = new Vector2(xVelo, yVelo);

        GameSettings.instance.SoundUnit(attacksClip[(int)age], gameObject.transform);
    }

    private void SetupParam()
    {
        health = Starthealth(side == SideUnit.Enemy);
        tempAttackSpeed = attackSpeed;

        tempheathMax = baseStat.healthMaxBase * (UnitsController.instance.multipliersPlayer[(int)unitType].healthMultiplier + (float)age / 10);

        if (side == SideUnit.Enemy)
        {
            heathBar.GetComponent<SpriteRenderer>().sprite = hpEnemy;
            transform.localScale = PlayerPrefs.GetString("Side") == "Left" ? new Vector3(-1f,1f,1f) : new Vector3(1f, 1f, 1f);
            UnitsController.instance.EnemyUnits.Add(this);
        }
        else
        {
            heathBar.GetComponent<SpriteRenderer>().sprite = hpPlayer;
            transform.localScale = PlayerPrefs.GetString("Side") == "Left" ? new Vector3(1f, 1f, 1f) : new Vector3(-1f, 1f, 1f);
            UnitsController.instance.PlayerUnits.Add(this);
        }
        transform.localScale = unitType == UnitType.AirUnit ? new Vector3(transform.localScale.x * 0.65f, transform.localScale.y * 0.65f, transform.localScale.z * 0.65f) : new Vector3(transform.localScale.x * 1f, transform.localScale.y * 1f, transform.localScale.z * 1f);

        UnitsController.instance.UnitAddLine(this);
        StartCoroutine(HideHpBarCourutite());
    }

    public void ChangeAge()
    {
        switch (age)
        {
            case Age.StoneAge:
                animator.SetTrigger("Evolution");
                animator.SetTrigger("Stone_Age");
                break;
            case Age.China:
                animator.SetTrigger("Evolution");
                animator.SetTrigger("China");
                break;
            case Age.MiddeleAge:
                animator.SetTrigger("Evolution");
                animator.SetTrigger("Middle_Age");
                break;
            case Age.Nowadays:
                animator.SetTrigger("Evolution");
                animator.SetTrigger("Nowadays");
                break;
            case Age.Future:
                animator.SetTrigger("Evolution");
                animator.SetTrigger("Future");
                break;
            case Age.AncientGod:
                animator.SetTrigger("Evolution");
                animator.SetTrigger("Ancient_God");
                break;
        }

        MultiplirStats(side == SideUnit.Enemy);
    }

    public void MultiplirStats(bool side)
    {
        switch (side)
        {
            case false:
                damageMin = baseStat.damageMinBase * (UnitsController.instance.multipliersPlayer[(int)unitType].attackMultiplier + (float)age / 10);
                damageMax = baseStat.damageMaxBase * (UnitsController.instance.multipliersPlayer[(int)unitType].attackMultiplier + (float)age / 10);
                defenceMin = baseStat.defenceMinBase * (UnitsController.instance.multipliersPlayer[(int)unitType].defenceMultiplier + (float)age / 10);
                defenceMax = baseStat.defenceMaxBase * (UnitsController.instance.multipliersPlayer[(int)unitType].defenceMultiplier + (float)age / 10);
                heathMax = baseStat.healthMaxBase * (UnitsController.instance.multipliersPlayer[(int)unitType].healthMultiplier + (float)age / 10);

                health = (health / tempheathMax) * heathMax;
                tempheathMax = heathMax;

                distanceAttack = baseStat.distanceAttackBase * (UnitsController.instance.multipliersPlayer[(int)unitType].rangeMultiplier + (float)age / 10);
                attackSpeed = (UnitsController.instance.multipliersPlayer[(int)unitType].attackSpeedMultiplier); // скорость атки не зависит от эпохи

                valuation = (int)(baseStat.valuationBase * UnitsController.instance.incomeMultiplayerEnemy); // ценность юнита не зависит от эпохи

                break;
            case true: //Для юнитов противника
                damageMin = baseStat.damageMinBase * (UnitsController.instance.multipliersEnemy[(int)unitType].attackMultiplier + (float)age / 10);
                damageMax = baseStat.damageMaxBase * (UnitsController.instance.multipliersEnemy[(int)unitType].attackMultiplier + (float)age / 10);
                defenceMin = baseStat.defenceMinBase * (UnitsController.instance.multipliersEnemy[(int)unitType].defenceMultiplier + (float)age / 10);
                defenceMax = baseStat.defenceMaxBase * (UnitsController.instance.multipliersEnemy[(int)unitType].defenceMultiplier + (float)age / 10);
                heathMax = baseStat.healthMaxBase * (UnitsController.instance.multipliersEnemy[(int)unitType].healthMultiplier + (float)age / 10);

                health = (health / tempheathMax) * heathMax;
                tempheathMax = heathMax;

                distanceAttack = baseStat.distanceAttackBase * (UnitsController.instance.multipliersEnemy[(int)unitType].rangeMultiplier + (float)age / 10);
                attackSpeed = (UnitsController.instance.multipliersEnemy[(int)unitType].attackSpeedMultiplier); // скорость атки не зависит от эпохи

                valuation = (int)(baseStat.valuationBase * UnitsController.instance.incomeMultiplayerPlayer); // ценность юнита не зависит от эпохи
                break;
        }
        HealtBarUpdate();
    }

    public void HealtBarUpdate()
    {
        heathBar.transform.localScale = new Vector3(health / heathMax * 1.5f, 1.5f, 1.5f);
    }

    void FindClosedEnemy()
    {
        if (target == null)
            target = side == SideUnit.Enemy ? line.playerStartPoint : line.enemyStartPoint;

        distanceForTarget = Mathf.Infinity;
        List<Unit> allTarget;

        if (side == SideUnit.Enemy)
            allTarget = UnitsController.instance.PlayerUnits;
        else
            allTarget = UnitsController.instance.EnemyUnits;

        //Если есть башня
        if (line.tower != null && line.tower.side.ToString() != side.ToString())
        {           
            foreach (Transform point in line.tower.points)
            {
                float tempdist = Mathf.Abs(transform.position.x - point.transform.position.x);
                if (tempdist < distanceForTarget)
                {
                    distanceForTarget = tempdist;
                    target = point.transform;
                }
            }
        }
        // Если есть противники
        foreach (Unit at in allTarget)
        {
            if (canAttackFly || canAttackFly == at.fly)
            {
                float tempdist = Mathf.Abs(transform.position.x - at.transform.position.x);
                if (tempdist < distanceForTarget)
                {
                    distanceForTarget = tempdist;
                    target = at.transform;
                }
            }
        }
        //Если нет противников
        if (allTarget.Count == 0 && target == null)
        {
            if (side == SideUnit.Enemy)
            {
                target = line.playerStartPoint;
            }
            else
            {
                target = line.enemyStartPoint;
            }
        }
        distanceForTarget = Mathf.Abs(transform.position.x - target.position.x);
    }

    IEnumerator HideHpBarCourutite()
    {
        heathBar.SetActive(false);
        yield return new WaitForSeconds(0.33f);
        heathBar.SetActive(true);
    }

    float Starthealth(bool side)
    {
        float startheath = 0; 
        switch (side)
        {
            case true:
                startheath = baseStat.healthMaxBase * (UnitsController.instance.multipliersEnemy[(int)unitType].healthMultiplier + (float)age / 10);
                break;
            case false:
                startheath = baseStat.healthMaxBase * (UnitsController.instance.multipliersPlayer[(int)unitType].healthMultiplier + (float)age / 10);
                break;
        }
        return startheath;
    }

}
