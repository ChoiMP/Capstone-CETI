using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret2 : MonoBehaviour
{
    public GameObject turret1;

    void Start()
    {

    }

    void Update()
    {
        LookTarget();
    }

    void LookTarget()
    {
        Vector3 targetPos = turret1.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(targetPos);
    }
}
