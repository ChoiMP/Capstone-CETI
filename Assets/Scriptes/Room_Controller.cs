using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Controller : MonoBehaviour
{
    public GameObject ceiling_Object; // �� õ�� ������Ʈ ���� ���ٰ���
    public static GameObject room;
    public bool player1_In; // �÷��̾� 1�� �濡 �����ߴ��� Ȯ�����ִ� ����
    public bool player2_In; // �÷��̾� 2�� �濡 �����ߴ��� Ȯ�����ִ� ����
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
        {// �÷��̾ ��� �����ϰ� ���������� Ŭ���� ���� �ʾҴٸ�
            if (transform.GetComponent<Spawn_Enemy>().spawn_Check != true) // ���������� ���� �������� �ʾҴٸ�
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
                transform.GetComponent<Spawn_Enemy>().Create_Enemy(); // �� ����
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
