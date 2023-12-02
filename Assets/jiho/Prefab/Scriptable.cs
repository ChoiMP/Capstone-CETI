using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scriptable : MonoBehaviour
{
    public static Scriptable instance;
    public GameObject[] Player_List;

    private void Awake()
    {
        instance = this;
    }


}
