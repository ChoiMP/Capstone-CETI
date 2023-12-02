using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffectManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        Invoke("Disappear", 1);
    }

    void Disappear()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
      
    }
}
