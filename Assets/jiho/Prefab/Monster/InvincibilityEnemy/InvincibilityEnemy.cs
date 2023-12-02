using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityEnemy : Monster
{
    [SerializeField] List<Monster> Stage_Enemy;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        //처음 생성시 맵에 있는 모든 적을 리스트로 넣어준다
        GameObject[] All_Enemy = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject e in All_Enemy)
        {
            Stage_Enemy.Add(e.GetComponent<Monster>());
            e.GetComponent<Monster>().Is_Invincibility = true;

        }
    }

    private void OnDestroy()//파괴 시 무적 상태인 모든 오브젝트를 무적 해제 시킨다
    {
        foreach (Monster e in Stage_Enemy)
        {
            e.Is_Invincibility = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
