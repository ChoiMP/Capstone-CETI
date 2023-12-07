using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Controller : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player1 robot")
        {
            Player1.instance.player1_hp -= (int)0.1f;
            print("player1 체력 감소하고 현재 남은 체력: " + Player1.instance.player1_hp);
            Destroy(gameObject);
        }
        else if(collision.gameObject.name == "Player2 robot")
        {
            Player2.instance.player2_hp -= (int)0.1f;
            print("player2 체력 감소하고 현재 남은 체력: " + Player2.instance.player2_hp);
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Plant")
        {
            Destroy(gameObject);
        }
    }

}
