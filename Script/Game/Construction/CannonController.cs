using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class CannonPosition
{
    public string age;
    public Transform[] point;
}
[System.Serializable]
public class ListCannon
{
    public string floorName;
    public List<GameObject> cannon;
}


public class CannonController : MonoBehaviour {

    [Header("Cannon Prefabs")]
    public GameObject[] cannonPrefab;
    [Header("Cannon Position")]
    public CannonPosition[] cannonCastle_1Positions;
    public CannonPosition[] cannonCastle_2Positions;
    [Header("List Cannon")]
    public ListCannon[] playerCannon;
    public ListCannon[] enemyCannon;

    private CannonPosition[] playerPositions;
    private CannonPosition[] enemyPositions;


    public static CannonController instance;

    private void Awake()
    {
        instance = FindObjectOfType<CannonController>();
    }

    private void Start()
    {
        playerPositions = PlayerPrefs.GetString("Side") == "Left" ? cannonCastle_1Positions : cannonCastle_2Positions;
        enemyPositions = PlayerPrefs.GetString("Side") == "Left" ? cannonCastle_2Positions : cannonCastle_1Positions;
    }

    public void SetupPlayerCannon(int cannonlvl, int lvl) // lvl - этаж;
    {
        Unit.Age age = GameController.instance.playerAge;
        Transform Point = playerPositions[(int)age].point[lvl];
        GameObject CannonObject = Instantiate(cannonPrefab[(int)age], Point.position, Quaternion.identity);
        CannonObject.transform.parent = Point;
        playerCannon[lvl].cannon.Add(CannonObject);
        CannonObject.GetComponent<Cannon>().leavel = cannonlvl;
        CannonObject.GetComponent<Cannon>().enemySide = Tower.Side.Player;
    }
    
    public void RemovePlayerCannon(int lvl)
    {
        Destroy(playerCannon[lvl].cannon.First());
        playerCannon[lvl].cannon.Clear();
    }

    public void SetupEnemyCannon(int cannonlvl, int lvl) // lvl - этаж;
    {
        Unit.Age age = GameController.instance.enemyAge;
        Transform Point = enemyPositions[(int)age].point[lvl];
        GameObject CannonObject = Instantiate(cannonPrefab[(int)age], Point.position, Quaternion.identity);
        CannonObject.transform.parent = Point;
        enemyCannon[lvl].cannon.Add(CannonObject);
        CannonObject.GetComponent<Cannon>().leavel = cannonlvl;
        CannonObject.GetComponent<Cannon>().enemySide = Tower.Side.Enemy;
    }

    public void RemoveEnemyCannon(int lvl)
    {
        Destroy(enemyCannon[lvl].cannon.First());
        enemyCannon[lvl].cannon.Clear();
    }

    public void PlayerCannonUpdate()
    {
        foreach(ListCannon listcannon in playerCannon)
        {
            if(listcannon.cannon.Count>0)
            {
                listcannon.cannon.First().GetComponent<Cannon>().SetupParam();
            }
        }
    }

    public void ChangeCannon(bool enemySide)
    {
        int lvl = 0;
        switch (enemySide)
        {
            case false:
                foreach(ListCannon listCannon in playerCannon)
                {
                    if(listCannon.cannon.Count != 0)
                    {
                        int cannonlvl = listCannon.cannon.First().GetComponent<Cannon>().leavel;
                        RemovePlayerCannon(lvl);
                        SetupPlayerCannon(cannonlvl, lvl);
                    }
                    lvl++;
                }
                break;

            case true:
                foreach (ListCannon listCannon in enemyCannon)
                {
                    if (listCannon.cannon.Count != 0)
                    {
                        int cannonlvl = listCannon.cannon.First().GetComponent<Cannon>().leavel;
                        RemoveEnemyCannon(lvl);
                        SetupEnemyCannon(cannonlvl, lvl);
                    }
                    lvl++;
                }
                break;
        }
    }

    public void CheckFirepowerAchiv()
    {
        bool check = false;
        foreach(ListCannon list in playerCannon)
        {
            if (list.cannon.Count > 0)
                check = true;
            else
                check = false;
        }

        AchivmentsController.instance.Firepower(check);
    }
}
