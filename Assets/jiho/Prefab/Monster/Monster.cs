using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Monster : MonoBehaviour
{

    public NavMeshAgent agent;
    public Animator ani;

    [Header("찾을 오브젝트의 레이어")]
    [SerializeField] protected LayerMask Find_ObjectLayer;
    [Header("공격할 오브젝트")]
    public GameObject Player;


    [Header("기본 능력치")]
    public bool Is_Invincibility = false;//무적인가
    [SerializeField] float Hp;
    public float Speed;
    [SerializeField] float Attack_Power;
    [SerializeField] List<GameObject> Drop_Item;//떨어트릴 아이템 모아둔 리스트
    [SerializeField, Header("아이템 박스 할당")] GameObject itemBox;    //  아이템 박스 오브젝트
    [SerializeField] protected bool CanAttack;//공격을 할 수 있는가

    [SerializeField] GameObject Attack_Effect=null;

    private void Awake()
    {
    
        agent = this.GetComponent<NavMeshAgent>();
        ani = this.GetComponent<Animator>();
        agent.speed = Speed;

    }

    public virtual void Start() 
    {
        int num = Random.Range(0, 2);
        Player = Scriptable.instance.Player_List[num];
    }

    public void Move_To_Player()//플레이어를 따라가는 함수
    {
        if(Player.GetComponent<Player1>() && Player.GetComponent<Player1>().isDied)
        {
            Player = Scriptable.instance.Player_List[1];
        }
        else if(Player.GetComponent<Player2>() && Player.GetComponent<Player2>().isDied)
        {
            Player = Scriptable.instance.Player_List[0];
        }


        agent.SetDestination(Player.transform.position);
    }

    public void Attack_To_Player()//플레이어를 공격하는 함수
    {
        if(Player.GetComponent<Player1>()&& Player.GetComponent<Player1>().isDied==false)
        {
            Player.GetComponent<Player1>().player1_hp -= (int)Attack_Power;
        }
        else if(Player.GetComponent<Player2>() && Player.GetComponent<Player2>().isDied == false )
        {
            Player.GetComponent<Player2>().player2_hp -= (int)Attack_Power;
        }
        //플레이어가 데미지를 받는 부분
        print(Player.name + "을 공격 하였습니다");
    }

    public void Hit_the_Monster(float Player_AttackPower, GameObject Effect)//피격 받는 함수
    {                           //플레이어 공격의 데미지 부분

        if (Is_Invincibility == false)
        {
            Hp -= Player_AttackPower;

            if (Attack_Effect == null)
            {
                GameObject E = Instantiate(Effect);
                Attack_Effect = E;
                E.transform.parent = gameObject.transform;
                E.transform.localPosition = new Vector3(0, 0, 0);
                // 이펙트 삭제 함수 추가
                E.AddComponent<EffectDestroy>();
            }
            else
            {
                Attack_Effect.gameObject.SetActive(true);
            }


            if (Hp <= 0)//체력이 적으면 죽는다
            {
                //죽는 애니메이션
                //애니메이션 이벤트로 Destroy_To_Ani() 실행해서 파괴한다
                //  int ItemNum = Random.Range(0, Drop_Item.Count);//떨어트릴 아이템을 랜덤으로 정해준다
                //  Instantiate(Drop_Item[ItemNum]);
                Instantiate(itemBox, this.transform.position + Vector3.up, Quaternion.identity);
                Destroy(gameObject);

            }

            //피격 애니메이션 실행
        }

    }

    public void Destroy_To_Ani()//애니메이션으로 적 삭제
    {   //죽기전에 랜덤한 아이템을 소환한다
        //  int ItemNum = Random.Range(0, Drop_Item.Count);//떨어트릴 아이템을 랜덤으로 정해준다
        //  Instantiate(Drop_Item[ItemNum]);
        Instantiate(itemBox, this.transform.position + Vector3.up, Quaternion.identity);
        Destroy(this);
    }

    private void Ani()
    {
    }

}
