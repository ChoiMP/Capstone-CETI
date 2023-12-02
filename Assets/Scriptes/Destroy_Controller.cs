using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Controller : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player1")
        {
            print("player1 체력 감소");
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Player2")
        {
            print("player2 체력 감소");
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Plant")
        {
            StartCoroutine("Destroy_Storn");
        }
        else if(collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
    
    IEnumerator Destroy_Storn()
    {
        yield return new WaitForSecondsRealtime(1f);
        Destroy(gameObject);
    }
}
