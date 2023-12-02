using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortAttackMonster : Monster
{
    
    [SerializeField] GameObject CanHitThisPlayer;//해당 플레이어를 공격할수있음

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
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Attack_To_Player();
            CanHitThisPlayer = other.gameObject;
            agent.speed = 0;
            ani.SetBool("Walk Forward", false);
            ani.SetBool("Stab Attack", true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            CanHitThisPlayer = null;
            agent.speed = Speed;

            ani.SetBool("Stab Attack", false);
            ani.SetBool("Walk Forward", true);
        }
    }



}
