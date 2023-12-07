using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_Plant_UI : MonoBehaviour
{
    public GameObject canvas;

    public static Spawn_Enemy instance;
    public bool clear_Check_S = false; // 모든 몬스터를 해치웠을때를 체크해주는 변수
    public bool spawn_Check = false; // 몬스터가 생성되었는지 체크해주는 변수
    public bool boss_Check = false; // 보스 몬스터를 생성할지 말지 체크해주는 변수

    public int max_Enemy = 12; // 적 최대 생성가능한 수

    public int long_Enemy_Count; // 원거리
    public int short_Enemy_Count; // 근거리
    public GameObject[] long_Enemy;  // 원거리 적
    public GameObject[] short_Enemy; // 근거리 적
    public GameObject boss_Enemy; // 보스

    public GameObject rangeObject;

    public Text count_View; // 몬스터 생성 까지 남은 시간 표시
    public float count_View_Int; // 몬스터 생성 까지 남은 시간 계산
    public bool test;

    [SerializeField] BoxCollider boxCollider;

    public GameObject ceiling_Object; // 방 천장 오브젝트 끄고 켜줄거임
    public GameObject room;
    public bool player1_In; // 플레이어 1이 방에 입장했는지 확인해주는 변수
    public bool player2_In; // 플레이어 2이 방에 입장했는지 확인해주는 변수
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
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = boxCollider.bounds.size.x;
        float range_Z = boxCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion;
        respawnPosition += new Vector3(0, 1, 0);
        return respawnPosition;
    }

    public void Create_Enemy() // 적 생성 함수
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
        for (int i = 0; i <= long_Enemy_Count; i++) // 랜덤 수 만큼 원거리 적 생성
        {
            GameObject instantCapsul = Instantiate(long_Enemy[0], Return_RandomPosition(), Quaternion.identity);
            instantCapsul.transform.parent = room.transform;
            //instantCapsul.transform.parent = rangeObject.transform;
        }
        for (int i = 0; i <= short_Enemy_Count; i++) /// 랜덤 수 만큼 근거리 적 생성
        {
            int count = Random.Range(0, short_Enemy.Length);

            GameObject instantCapsul = Instantiate(short_Enemy[count], Return_RandomPosition(), Quaternion.identity);
            instantCapsul.transform.parent = rangeObject.transform;
        }

        if (boss_Check == true) // 보스 몬스터를 생성하는 방이면 생성
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
        {// 플레이어가 모두 입장하고 스테이지를 클리어 하지 않았다면
            if (transform.GetComponent<Spawn_Enemy>().spawn_Check != true) // 스테이지에 적을 생성하지 않았다면
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
