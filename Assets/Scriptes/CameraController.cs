using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ī�޶� ��ġ���� �־����� Ư�� ������Ʈ �Ⱥ���
        Camera camera = GetComponent<Camera>();
        float[] distance = new float[32];
        distance[10] = 5;
        camera.layerCullDistances = distance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
