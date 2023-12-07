using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spawn_Enemy : MonoBehaviour
{
    public static Spawn_Enemy instance;
    public bool clear_Check = false; // ��� ���͸� ��ġ�������� üũ���ִ� ����
    public bool spawn_Check = false; // ���Ͱ� �����Ǿ����� üũ���ִ� ����
    public bool boss_Check = false; // ���� ���͸� �������� ���� üũ���ִ� ����
    public bool spawn_Miri_Check = false; // ���Ͱ� ������ �������� üũ���ִ� ����

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

    [SerializeField]  BoxCollider boxCollider;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rangeObject = this.gameObject;
        boxCollider = rangeObject.GetComponent<BoxCollider>();

    }

    private void Update()
    {
        if (test == true)
        {
            count_View.gameObject.SetActive(true);
            count_View_Int -= Time.deltaTime;
            int count = (int)count_View_Int;
            count_View.text = count.ToString();
        }
        else if(test == false && spawn_Check == true)
        {
            count_View.gameObject.SetActive(false);
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
        test = true;
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        spawn_Miri_Check = true;
        yield return new WaitForSecondsRealtime(3f);
        test = false;
        spawn_Check = true;
        for (int i = 0; i <= long_Enemy_Count; i++) // ���� �� ��ŭ ���Ÿ� �� ����
        {
            GameObject instantCapsul = Instantiate(long_Enemy[0], Return_RandomPosition(), Quaternion.identity);
            instantCapsul.transform.parent = Room_Controller.room.transform;
            //instantCapsul.transform.parent = rangeObject.transform;
        }
        for (int i = 0; i <= short_Enemy_Count; i++) /// ���� �� ��ŭ �ٰŸ� �� ����
        {
            int count = Random.Range(1, short_Enemy.Length);

            GameObject instantCapsul = Instantiate(short_Enemy[count], Return_RandomPosition(), Quaternion.identity);
            instantCapsul.transform.parent = Room_Controller.room.transform;
            //instantCapsul.transform.parent = rangeObject.transform;
        }

        if (boss_Check == true) // ���� ���͸� �����ϴ� ���̸� ����
        {
            GameObject instantCapsul = Instantiate(boss_Enemy, Return_RandomPosition(), Quaternion.identity);
            instantCapsul.transform.parent = Room_Controller.room.transform;
            //instantCapsul.transform.parent = rangeObject.transform;
        }
    }
}
