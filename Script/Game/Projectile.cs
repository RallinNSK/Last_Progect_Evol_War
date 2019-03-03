using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour {

    private Animator animator;
    public float damageProjectile;
    public Transform target;


    private void Start()
    {
        if(GetComponent<Animator>() != null)
            animator = GetComponent<Animator>();
    }

    private void Update()
    {
        RotateForTaget();

        if (target != null && Mathf.Abs(transform.position.x - target.position.x) <= 0.55f && Mathf.Abs(transform.position.x - target.position.x) > 0.15f)
        {
            if (animator != null)
                animator.SetTrigger("Explosion");
        }
        else if (target != null && Mathf.Abs(transform.position.x - target.position.x) < 0.15f)
        {
            DameShow();
            Destroy(gameObject);
        }

        if (target == null)
        {
            Destroy(gameObject);
        }
    }

    private void RotateForTaget()
    {
        if (target != null)
        {
            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion qt = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, qt, Time.deltaTime *35f);
        }
    }

    private void DameShow()
    {
        if (target.GetComponent<Unit>() != null)
        {
            Unit unit = target.GetComponent<Unit>();
            float damage = damageProjectile - Random.Range(unit.defenceMin, unit.defenceMax);
            
            //Округление урона
            if (damage < 0)
                damage = 0;

            unit.health -= damage;

            int intDamage = (int)damage;
            GameObject DamageShow = Instantiate(unit.damageShow, target.position + Vector3.up * 10f, Quaternion.identity);

            DamageShow.transform.parent = target;
            DamageShow.GetComponentInChildren<Text>().text = "-" + intDamage.ToString();
            DamageShow.GetComponentInChildren<Text>().color = unit.side == SideUnit.Enemy ? unit.enemyUnitColor : unit.playerUnitColor;
            unit.HealtBarUpdate();
            DamageShow.SetActive(true);
        }
        else if(target.GetComponentInParent<Building>() != null)
        {
            Building building = target.GetComponentInParent<Building>();
            float damage = damageProjectile;
            building.health -= damage;
            building.CastleBarUpdate();

            if(GameSettings.instance.vibrate_bool && !building.enemySide)
            {
                Handheld.Vibrate();
            }
        }
        else if(target.GetComponentInParent<Tower>() != null)
        {
            Tower tower = target.GetComponentInParent<Tower>();
            float damage = damageProjectile;
            tower.health -= damage;
            tower.TowerHealthBarUpdate();

            if(tower.health <= 0 && GetComponentInParent<Unit>().side == SideUnit.Player)
            {
                tower.side = Tower.Side.Player;
                tower.ChengeSide();
            }
            else if(tower.health <= 0 && GetComponentInParent<Unit>().side == SideUnit.Enemy)
            {
                tower.side = Tower.Side.Enemy;
                tower.ChengeSide();
            }
        }
    }
}
