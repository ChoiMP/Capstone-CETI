using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Monster : MonoBehaviour
{

    public NavMeshAgent agent;
    public Animator ani;

    [Header("ã�� ������Ʈ�� ���̾�")]
    [SerializeField] protected LayerMask Find_ObjectLayer;
    [Header("������ ������Ʈ")]
    public GameObject Player;


    [Header("�⺻ �ɷ�ġ")]
    public bool Is_Invincibility = false;//�����ΰ�
    [SerializeField] float Hp;
    public float Speed;
    [SerializeField] float Attack_Power;
    [SerializeField] List<GameObject> Drop_Item;//����Ʈ�� ������ ��Ƶ� ����Ʈ
    [SerializeField, Header("������ �ڽ� �Ҵ�")] GameObject itemBox;    //  ������ �ڽ� ������Ʈ
    [SerializeField] protected bool CanAttack;//������ �� �� �ִ°�

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

    public void Move_To_Player()//�÷��̾ ���󰡴� �Լ�
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

    public void Attack_To_Player()//�÷��̾ �����ϴ� �Լ�
    {
        if(Player.GetComponent<Player1>()&& Player.GetComponent<Player1>().isDied==false)
        {
            Player.GetComponent<Player1>().player1_hp -= (int)Attack_Power;
        }
        else if(Player.GetComponent<Player2>() && Player.GetComponent<Player2>().isDied == false )
        {
            Player.GetComponent<Player2>().player2_hp -= (int)Attack_Power;
        }
        //�÷��̾ �������� �޴� �κ�
        print(Player.name + "�� ���� �Ͽ����ϴ�");
    }

    public void Hit_the_Monster(float Player_AttackPower, GameObject Effect)//�ǰ� �޴� �Լ�
    {                           //�÷��̾� ������ ������ �κ�

        if (Is_Invincibility == false)
        {
            Hp -= Player_AttackPower;

            if (Attack_Effect == null)
            {
                GameObject E = Instantiate(Effect);
                Attack_Effect = E;
                E.transform.parent = gameObject.transform;
                E.transform.localPosition = new Vector3(0, 0, 0);
                // ����Ʈ ���� �Լ� �߰�
                E.AddComponent<EffectDestroy>();
            }
            else
            {
                Attack_Effect.gameObject.SetActive(true);
            }


            if (Hp <= 0)//ü���� ������ �״´�
            {
                //�״� �ִϸ��̼�
                //�ִϸ��̼� �̺�Ʈ�� Destroy_To_Ani() �����ؼ� �ı��Ѵ�
                //  int ItemNum = Random.Range(0, Drop_Item.Count);//����Ʈ�� �������� �������� �����ش�
                //  Instantiate(Drop_Item[ItemNum]);
                Instantiate(itemBox, this.transform.position + Vector3.up, Quaternion.identity);
                Destroy(gameObject);

            }

            //�ǰ� �ִϸ��̼� ����
        }

    }

    public void Destroy_To_Ani()//�ִϸ��̼����� �� ����
    {   //�ױ����� ������ �������� ��ȯ�Ѵ�
        //  int ItemNum = Random.Range(0, Drop_Item.Count);//����Ʈ�� �������� �������� �����ش�
        //  Instantiate(Drop_Item[ItemNum]);
        Instantiate(itemBox, this.transform.position + Vector3.up, Quaternion.identity);
        Destroy(this);
    }

    private void Ani()
    {
    }

}
