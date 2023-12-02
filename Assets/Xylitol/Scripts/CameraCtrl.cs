using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    Vector3 targetPos;

    public float camSpeed = 10f;

    public bool is_KB_State = false;

    void Start()
    {

    }

    void Update()
    {
        if (!is_KB_State)
        {
            FollowCam();
        }
    }

    void FollowCam()
    {
        Vector3 p1Pos = player1.transform.position;
        Vector3 p2Pos = player2.transform.position;

        targetPos = (p1Pos + p2Pos) / 2;

        Vector3 camPos = targetPos + new Vector3(0, 8f, 0);
        transform.position = Vector3.Lerp(transform.position, camPos, Time.deltaTime * camSpeed);
    }

}
