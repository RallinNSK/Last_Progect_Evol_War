using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDicoration : MonoBehaviour {

    public Transform point_1;
    public Transform point_2;

    bool derectiom = false;

    private void Update()
    {
        if(!derectiom)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            transform.position = Vector3.MoveTowards(transform.position, point_2.position, Time.deltaTime * 2.5f);

            if(Vector3.Distance(transform.position,point_2.position) <= 0.5f)
                derectiom = !derectiom;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            transform.position = Vector3.MoveTowards(transform.position, point_1.position, Time.deltaTime * 2.5f);

            if (Vector3.Distance(transform.position, point_1.position) <= 0.5f)
                derectiom = !derectiom;
        }

    }
}
