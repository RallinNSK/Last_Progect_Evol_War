using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCamer : MonoBehaviour {

    [System.Serializable]
    public class AspectRatio
    {
        public string aspectRatioName;
        public float outerLeftPosition;
        public float outerRightPosition;
    }


    [Header("Header parametrs")]
    public float dragSpeed;
    private Vector3 dragOrigin;

    public bool cameraDragging = true;

    private float outerLeft;
    private float outerRight;

    [Header("Camera parametrs")]
    public AspectRatio[] aspectRatios;

    private void Awake()
    {
        SetupPostition();
    }

    private void SetupPostition()
    {
        if (Camera.main.aspect >= 1.77f) // 16:9
        {
            outerLeft = aspectRatios[0].outerLeftPosition;
            outerRight = aspectRatios[0].outerRightPosition;
        }
        else if (Camera.main.aspect >= 1.66f) // 5:3
        {
            outerLeft = aspectRatios[3].outerLeftPosition;
            outerRight = aspectRatios[3].outerRightPosition;
        }
        else if (Camera.main.aspect >= 1.59f) // 16:10
        {
            outerLeft = aspectRatios[1].outerLeftPosition;
            outerRight = aspectRatios[1].outerRightPosition;
        }
        else if (Camera.main.aspect >= 1.49f) // 3:2
        {
            outerLeft = aspectRatios[4].outerLeftPosition;
            outerRight = aspectRatios[4].outerRightPosition;
        }
        else if (Camera.main.aspect >= 1.33f) // 4:3
        {
            outerLeft = aspectRatios[2].outerLeftPosition;
            outerRight = aspectRatios[2].outerRightPosition;
        }

        transform.position = PlayerPrefs.GetString("Side") == "Left" ? new Vector3(outerLeft, 0f, -10.0f) : new Vector3(outerRight, 0f, -10.0f);
    }

    void Update()
    {
        DragCamera();
        CastleHit();
    }

    //Требуется для открытия панели улучшений пушек
    private static void CastleHit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

            if (hit && hit.collider.GetComponent<Building>() != null && !hit.collider.GetComponent<Building>().enemySide && GameController.instance.playerCasleLVL > 0)
            {
                FindObjectOfType<Panels>().CannonUpgradePanel(true);
            }
        }
    }

    private void DragCamera()
    {
        Vector2 mousePosition = Input.mousePosition;

        if (cameraDragging)
        {

            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = Input.mousePosition;
                return;
            }

            if (!Input.GetMouseButton(0)) return;

            Vector3 pos = Camera.main.ScreenToViewportPoint(dragOrigin - Input.mousePosition);
            Vector3 move = new Vector3(pos.x * dragSpeed, 0, 0);

            if (move.x > 0f)
            {
                if (transform.position.x < outerRight)
                {
                    transform.Translate(move, Space.World);
                }
            }
            else
            {
                if (transform.position.x > outerLeft)
                {
                    transform.Translate(move, Space.World);
                }
            }
        }

        if (transform.position.x < outerLeft)
            transform.position = new Vector3(outerLeft, transform.position.y, transform.position.z);
        else if (transform.position.x > outerRight)
            transform.position = new Vector3(outerRight, transform.position.y, transform.position.z);
    }

}
