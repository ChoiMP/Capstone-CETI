using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_Summoner : Monster
{
    [SerializeField] GameObject [] Enemy;//��ȯ�� ������Ʈ
    [SerializeField] float Summoner_Time;//��ȯ�� �ð�
    [SerializeField] float Timer;//��ȯ�� �ð�
    public override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer>= Summoner_Time)
        {
            ani.SetBool("Jump", true);
            Summoner();
            Timer = 0;
        }
        else if(Timer >=1f)
        {
            ani.SetBool("Jump", false);
        }
    }

    void Summoner()
    {
        int num = Random.Range(0, Enemy.Length);

        float X = (Random.Range(-0.5f, 0.5f))+transform.position.x;
        float Y = (Random.Range(-0.5f, 0.5f))+transform.position.y;

        Instantiate(Enemy[num], new Vector2(X, Y), Quaternion.identity);
       
    }
}
