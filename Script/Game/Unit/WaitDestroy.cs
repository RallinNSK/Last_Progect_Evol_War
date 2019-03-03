using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitDestroy : MonoBehaviour {

    public float timeToDestroy = 1f;

    private void Start()
    {
        StartCoroutine(Courutine());
    }


    IEnumerator Courutine()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
}
