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

        //ó�� ������ �ʿ� �ִ� ��� ���� ����Ʈ�� �־��ش�
        GameObject[] All_Enemy = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject e in All_Enemy)
        {
            Stage_Enemy.Add(e.GetComponent<Monster>());
            e.GetComponent<Monster>().Is_Invincibility = true;

        }
    }

    private void OnDestroy()//�ı� �� ���� ������ ��� ������Ʈ�� ���� ���� ��Ų��
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
