using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour {

    public Text text;
    public float timer;

    private float time = 1;

    private void Awake()
    {
        Time.timeScale = 1;
        StartCoroutine(Destr(timer));
    }

    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            text.text += ".";
            time = 1;
        }
    }

    IEnumerator Destr(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

}
