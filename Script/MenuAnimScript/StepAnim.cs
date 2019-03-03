using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepAnim : MonoBehaviour {


    private Animator animator;

    public Transform[] points;
    public float moveSpeed = 0.8f;

    public bool useFall;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(MoveCourutine());
    }

    IEnumerator MoveCourutine()
    {
        int rand = Random.Range(8, points.Length - 1);
        for (int i = 0; i < points.Length; i++)
        {
            float time = 0f;
            Vector3 start = transform.position;
            Vector3 nextpos;
            if (useFall && i == rand)
            {
                nextpos = new Vector3(transform.position.x + 0, transform.position.y - 10, transform.position.z);
                moveSpeed = 2.5f;
                animator.SetTrigger("Fall");
            }
            else
                nextpos = points[i].position;
            while (time < 1f)
            {
                time += Time.deltaTime / moveSpeed;
                transform.position = Vector3.Lerp(start, nextpos, time);
                yield return null;
            }
            if (useFall && i == rand)
                Destroy(gameObject);
            yield return null;
        }
        GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.clear, 0.3f);
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.clear, 0.6f);
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.clear, 0.9f);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
