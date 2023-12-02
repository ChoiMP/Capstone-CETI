using System.Collections;
using UnityEngine;

public class BommSlime : Monster
{
    [SerializeField] float Rush_Speed;
    [SerializeField] GameObject Dir_Display_Obj;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();  
        StartCoroutine("SetDir");
    }

    // Update is called once per frame
    void Update()
    {
        Dir_Display_Obj.transform.LookAt(Player.transform.position);
    }

    IEnumerator SetDir()
    {
        float Timer=0;
        yield return null;
        Vector3 Dir = (Player.transform.position - transform.position).normalized;
        while (true)
        {
            yield return null;
            Dir_Display_Obj.SetActive(true);
            transform.LookAt(Player.transform.position);
            Timer += Time.deltaTime;

            if(Timer>3)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                Timer = 0;
                break;
            }

        }
        
        while (true)//돌진 시작
        {
            Dir_Display_Obj.SetActive(false);

            yield return null;
            Dir_Display_Obj.transform.LookAt(Dir);
            Timer += Time.deltaTime;
            transform.Translate(Vector3.forward * Rush_Speed * Time.deltaTime);

            if (Timer > 1)//돌진 할 시간
            {
                StartCoroutine("SetDir");
                break;
            }
            // print("코루틴 끝");
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("EnemyAttackOBJ"))
        {
            Attack_To_Player();
            GetComponent<Rigidbody>().isKinematic = true;
        }
    
    }
}
