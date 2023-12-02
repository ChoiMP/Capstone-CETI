using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookCAM : MonoBehaviour
{
    public Camera cam;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        transform.rotation = cam.transform.rotation;    
        //transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }
}
