using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStemAnim : MonoBehaviour {

    [Header("Steps Elements")]
    public Transform spawnPoints;
    public Transform[] points;
    [Header("Spear&Arrow")]
    public Transform[] spawnArrowPoint;
    public Transform[] targetArrow;
    [Header("Prefab")]
    public GameObject[] Steps;
    public GameObject[] spearAndArrow;
    [Header("Update time")]
    public float updateSteps;
    public float updateArrow;
    private float tempTimeSteps;
    private float tempTimeArrow;

    private void Start()
    {
        tempTimeSteps = 0;
        tempTimeArrow = 0;
    }


    private void Update()
    {
        tempTimeSteps -= Time.deltaTime;
        if (tempTimeSteps <= 0)
        {
            GameObject Step = Instantiate(Steps[Random.Range(0, Steps.Length)], spawnPoints.position, Quaternion.identity);
            Step.GetComponent<StepAnim>().points = points;

            tempTimeSteps = updateSteps;
        }

        tempTimeArrow -=Time.deltaTime;
        if(tempTimeArrow <= 0)
        {
            GameObject Arrow = Instantiate(spearAndArrow[Random.Range(0, spearAndArrow.Length)], spawnArrowPoint[Random.Range(0, spawnArrowPoint.Length)].position, Quaternion.identity);

            Arrow.GetComponent<Arrow>().target = targetArrow[Random.Range(0, targetArrow.Length)];
            tempTimeArrow = updateArrow;
        }
    }
}
