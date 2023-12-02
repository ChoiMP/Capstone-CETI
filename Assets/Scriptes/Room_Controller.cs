using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Controller : MonoBehaviour
{
    public GameObject ceiling_Object; // 방 천장 오브젝트 끄고 켜줄거임
    public static GameObject room;
    public bool player1_In; // 플레이어 1이 방에 입장했는지 확인해주는 변수
    public bool player2_In; // 플레이어 2이 방에 입장했는지 확인해주는 변수
    public GameObject ston_Rain;
    public GameObject start_Plant;

    private void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        print("DDDDDDDDDDDAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        if(other.gameObject.name == "Player1 robot")
        {
            player1_In = true;
        }
        if (other.gameObject.name == "Player2 robot")
        {
            player2_In = true;
        }
        /*if (other.gameObject.tag == "Player1")
        {
            player1_In = true;
        }
        if (other.gameObject.tag == "Player2")
        {
            player2_In = true;
        }*/
        if (player1_In == true && player2_In == true && transform.GetComponent<Spawn_Enemy>().clear_Check == false)
        {// 플레이어가 모두 입장하고 스테이지를 클리어 하지 않았다면
            if (transform.GetComponent<Spawn_Enemy>().spawn_Check != true) // 스테이지에 적을 생성하지 않았다면
            {
                if (ston_Rain.gameObject != null)
                {
                    ston_Rain.gameObject.SetActive(true);
                }
                room = transform.gameObject;
                Door_Manager.instance.test = room;

                print(room.name);
                ceiling_Object.gameObject.SetActive(false);
                Door_Manager.instance.On_Door();
                transform.GetComponent<Spawn_Enemy>().Create_Enemy(); // 적 생성
            }
        }
        else
        {
            ceiling_Object.gameObject.SetActive(false);
            if (ston_Rain != null)
            {
                ston_Rain.gameObject.SetActive(false);
            }

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player1 robot")
        {
            player1_In = false;
        }
        if (other.gameObject.name == "Player2 robot")
        {
            player2_In = false;
        }
        if (player1_In == false && player2_In == false)
        {
            ceiling_Object.gameObject.SetActive(true);
        }
    }

   /* private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player1" && other.tag != "Player2")
        {
            ceiling_Object.gameObject.SetActive(true);
        }
    }*/
}
