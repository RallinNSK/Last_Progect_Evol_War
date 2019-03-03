using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientGodGroup : MonoBehaviour
{

    public GameObject[] group;

    private void OnEnable()
    {
        Disable();

        StartCoroutine(ActivetedGroupCourutine());
    }

    private void OnDisable()
    {
        Disable();
    }

    private void Disable()
    {
        foreach (GameObject element in group)
        {
            element.SetActive(false);
        }
    }

    IEnumerator ActivetedGroupCourutine()
    {
        foreach(GameObject element in group)
        {
            element.SetActive(true);
            yield return new WaitForSeconds(0.25f);
        }
    }


}
