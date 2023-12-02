using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zippermouth_Bullet : MonoBehaviour
{
    [SerializeField] GameObject p1;
    [SerializeField] GameObject p2;
    public int Damege;
    // Start is called before the first frame update
    void Start()
    {
        p1 = GameObject.Find("Player1 robot");
        p2 = GameObject.Find("Player2 robot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //print(other.name);
        if(other.gameObject==p1)
        {
            if(other.GetComponent<Player1>())
            {
                other.GetComponent<Player1>().player1_hp -= Damege;
            }
          
            Destroy(gameObject);
        }
        else if (other.gameObject == p2)
        {
            if (other.GetComponent<Player2>())
            {
                other.GetComponent<Player2>().player2_hp -= Damege;
            }

            Destroy(gameObject);
        }
        else if(other.transform.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
     
    }
}
