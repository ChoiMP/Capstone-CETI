using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_Plant_UI : MonoBehaviour
{
    public GameObject canvas;

    public static Spawn_Enemy instance;
    public bool clear_Check_S = false; // ��� ���͸� ��ġ�������� üũ���ִ� ����
    public bool spawn_Check = false; // ���Ͱ� �����Ǿ����� üũ���ִ� ����
    public bool boss_Check = false; // ���� ���͸� �������� ���� üũ���ִ� ����

    public int max_Enemy = 12; // �� �ִ� ���������� ��

    public int long_Enemy_Count; // ���Ÿ�
    public int short_Enemy_Count; // �ٰŸ�
    public GameObject[] long_Enemy;  // ���Ÿ� ��
    public GameObject[] short_Enemy; // �ٰŸ� ��
    public GameObject boss_Enemy; // ����

    public GameObject rangeObject;

    public Text count_View; // ���� ���� ���� ���� �ð� ǥ��
    public float count_View_Int; // ���� ���� ���� ���� �ð� ���
    public bool test;

    [SerializeField] BoxCollider boxCollider;

    public GameObject ceiling_Object; // �� õ�� ������Ʈ ���� ���ٰ���
    public GameObject room;
    public bool player1_In; // �÷��̾� 1�� �濡 �����ߴ��� Ȯ�����ִ� ����
    public bool player2_In; // �÷��̾� 2�� �濡 �����ߴ��� Ȯ�����ִ� ����
                            // Start is called before the first frame update

    public bool check = true;
    private void Start()
    {
        clear_Check_S = false;
        room = this.gameObject;
        ceiling_Object.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (count_View_Int <= 0)
        {
            count_View_Int = 0;
            if(check == true)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    count_View.gameObject.SetActive(false);
                    canvas.SetActive(false);
                    Create_Enemy();
                    check = false;
                }
            }
        }
        else
        {
            int count = (int)count_View_Int;
            canvas.SetActive(true);
            count_View.gameObject.SetActive(true);
            count_View_Int -= Time.deltaTime;
            count_View.text = count.ToString();
        }
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        // �ݶ��̴��� ����� �������� bound.size ���
        float range_X = boxCollider.bounds.size.x;
        float range_Z = boxCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion;
        respawnPosition += new Vector3(0, 1, 0);
        return respawnPosition;
    }

    public void Create_Enemy() // �� ���� �Լ�
    {
        long_Enemy_Count = Random.Range(1, 13);
        short_Enemy_Count = Random.Range(1, 13);

        if (long_Enemy_Count + short_Enemy_Count > max_Enemy)
        {
            while (true)
            {
                if (long_Enemy_Count + short_Enemy_Count <= max_Enemy)
                {
                    break;
                }
                else
                {
                    long_Enemy_Count = Random.Range(1, 13);
                    short_Enemy_Count = Random.Range(3, 13);
                }
            }
        }
        Spawn();
    }

    void Spawn()
    {
        spawn_Check = true;
        for (int i = 0; i <= long_Enemy_Count; i++) // ���� �� ��ŭ ���Ÿ� �� ����
        {
            GameObject instantCapsul = Instantiate(long_Enemy[0], Return_RandomPosition(), Quaternion.identity);
            instantCapsul.transform.parent = room.transform;
            //instantCapsul.transform.parent = rangeObject.transform;
        }
        for (int i = 0; i <= short_Enemy_Count; i++) /// ���� �� ��ŭ �ٰŸ� �� ����
        {
            int count = Random.Range(0, short_Enemy.Length);

            GameObject instantCapsul = Instantiate(short_Enemy[count], Return_RandomPosition(), Quaternion.identity);
            instantCapsul.transform.parent = rangeObject.transform;
        }

        if (boss_Check == true) // ���� ���͸� �����ϴ� ���̸� ����
        {
            GameObject instantCapsul = Instantiate(boss_Enemy, Return_RandomPosition(), Quaternion.identity);
            instantCapsul.transform.parent = room.transform;
            //instantCapsul.transform.parent = rangeObject.transform;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player1 robot")
        {
            player1_In = true;
        }
        if (other.gameObject.name == "Player2 robot")
        {
            player2_In = true;
        }
        if (player1_In == true && player2_In == true && transform.GetComponent<Spawn_Enemy>().clear_Check == false)
        {// �÷��̾ ��� �����ϰ� ���������� Ŭ���� ���� �ʾҴٸ�
            if (transform.GetComponent<Spawn_Enemy>().spawn_Check != true) // ���������� ���� �������� �ʾҴٸ�
            {
                room = this.gameObject;

                ceiling_Object.gameObject.SetActive(false);
                Room_Controller.room = room;
                Door_Manager.instance.On_Door();
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player1 robot")
        {
            player1_In = true;
        }
        if (other.gameObject.name == "Player2 robot")
        {
            player2_In = true;
        }
        if (player1_In == false && player2_In == false)
        {
            ceiling_Object.gameObject.SetActive(true);
        }
    }

}
