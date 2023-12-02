using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongAttackMonster : Monster
{
    [SerializeField] GameObject CanHitThisPlayer;//해당 플레이어를 공격할수있음
    [SerializeField] GameObject Attack_obj;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    
    ani.SetBool("Walk Forward", true);
    }

    // Update is called once per frame
    void Update()
    {
        Move_To_Player();
        if(CanHitThisPlayer!=null)
        {
            transform.LookAt(CanHitThisPlayer.transform);//플레이어 쳐다보기

        }

    }


    public void Attack_ani()
    {
        if(CanHitThisPlayer!=null)
        {
            GameObject a = Instantiate(Attack_obj, transform.position, Quaternion.identity);
            a.GetComponent<LongAttack_Obh>().parents = this;
            a.transform.LookAt(CanHitThisPlayer.transform);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            CanHitThisPlayer = other.gameObject;
            agent.speed = 0;
            ani.SetBool("Walk Forward", false);
            ani.SetBool("Attack", true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            CanHitThisPlayer = null;
            agent.speed = Speed;

            ani.SetBool("Attack", false);
            ani.SetBool("Walk Forward", true);
        }
    }
}
