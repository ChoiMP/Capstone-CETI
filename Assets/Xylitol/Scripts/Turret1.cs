using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret1 : MonoBehaviour
{
    public GameObject turret2;
    Vector3 targetPos;

    public Transform createPos;
    public Transform endPos;

    float distance;
    float maxDistance = 12f;

    public GameObject[] lightning_normal;
    public GameObject lightning_powerup;

    public GameObject attackEffect;

    public GameObject[] effects;

    public bool onObstacle; // 장애물 판단

    public int level2_XP = 3;
    public int level3_XP = 5;
    public int level4_XP = 10;

    bool level1;
    bool level2 = false;
    bool level3 = false;
    bool level4 = false;

    // 추가 
    public float dmg=10f;

    void Start()
    {
        level1 = true;
    }

    void Update()
    {
        LookTarget();
        PlayerAliveCheck();
        LevelManage();
    }

    void LookTarget()
    {
        targetPos = turret2.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(targetPos);
    }


    void Lightning_hit_Manage() // 공격 판정
    {
        Vector3 turret2Pos = turret2.transform.position;
        Vector3 targetDir = turret2Pos - transform.position;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, targetDir, out hit))
        {
            if (hit.collider.tag == "Wall")
            {
                onObstacle = true;
                // Debug.Log("벽");
            }
            else
            {
                onObstacle = false;
                if (effects.Length >= 1)
                {
                    effects[0].SetActive(false);
                }
            }
        }
        RaycastHit[] hitEnemy;
        hitEnemy = Physics.RaycastAll(transform.position, targetDir, distance);
        for (int i = 0; i < hitEnemy.Length; i++)
        {
            if (hitEnemy[i].transform.tag == "Enemy")
            {
                if (hitEnemy[i].transform.tag != "Player")
                {
                    hitEnemy[i].transform.GetComponent<Monster>().Hit_the_Monster(dmg, attackEffect);
                    print(hitEnemy[i].transform.name);
                }
            }
            Debug.Log(hitEnemy[i].transform.name);
        }
        Debug.DrawRay(transform.position, targetDir, Color.green);
    }

    void PlayerAliveCheck()
    {
        Player1 p1 = GameObject.FindObjectOfType<Player1>();
        Player2 p2 = GameObject.FindObjectOfType<Player2>();
        if (p1.player1_hp > 0 && p2.player2_hp > 0)
        {
            DistanceCheck();
        }
        else
        {
            for (int i = 0; i < lightning_normal.Length; i++)
            {
                lightning_normal[i].SetActive(false);
            }
            lightning_powerup.SetActive(false);

        }
    } 

    void DistanceCheck() //거리 따라 라이트닝 on/off
    {
        Vector3 player2Pos = GameObject.Find("Player2 robot").transform.position;
        distance = Vector3.Distance(transform.position, player2Pos);
        if (distance <= maxDistance)
        {

            CreateLightning();
            Lightning_hit_Manage();
        }
        else
        {
            for (int i = 0; i < lightning_normal.Length; i++)
            {
                lightning_normal[i].SetActive(false);
            }
            lightning_powerup.SetActive(false);
        }
    }

    void LevelManage()   // 레벨 관리
    {
        Player1 player1 = GameObject.Find("Player1 robot").GetComponent<Player1>();
        if (player1.current_XP >= level2_XP
             && player1.current_XP < level3_XP)
        {
            level1 = false;
            level2 = true;
        }
        else if (player1.current_XP >= level3_XP
             && player1.current_XP < level4_XP)
        {
            level1 = false; level2 = false;
            level3 = true;
        }
        else if (player1.current_XP >= level4_XP)
        {
            level1 = false; level2 = false; level3 = false;
            level4 = true;
        }
    }

    void CreateLightning() // 레벨 별 라인트닝 관리 
    {
        if (level1)
        {
            if (!onObstacle)
                lightning_normal[0].SetActive(true);
            else
                lightning_normal[0].SetActive(false);
            maxDistance = 10f;
        }
        if (level2)
        {
            if (!onObstacle)
            {
                lightning_normal[0].SetActive(true);
                lightning_normal[1].SetActive(true);
            }
            else
            {
                lightning_normal[0].SetActive(false);
                lightning_normal[1].SetActive(false);
            }
            maxDistance = 15f;
        }
        else if (level3)
        {
            if (!onObstacle)
            {
                for (int i = 0; i < lightning_normal.Length; i++)
                {
                    lightning_normal[i].SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < lightning_normal.Length; i++)
                {
                    lightning_normal[i].SetActive(false);
                }
            }
            maxDistance = 20f;
        }
        else if (level4)
        {
            if (!onObstacle)
            {
                for (int i = 0; i < lightning_normal.Length; i++)
                {
                    lightning_normal[i].SetActive(false);
                }
                lightning_powerup.SetActive(true);
            }
            else
                lightning_powerup.SetActive(false);
            maxDistance = 20f;
        }

    }
}
       