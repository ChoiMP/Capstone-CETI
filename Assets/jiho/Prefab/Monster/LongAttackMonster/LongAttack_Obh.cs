using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongAttack_Obh : MonoBehaviour
{
    public Monster parents;
    [SerializeField] ParticleSystem pS;
    [SerializeField] bool AttackObj;
    [SerializeField] float Speed;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(DestroyThis());
    }

    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);


        if(AttackObj==true)
        {
            if(transform.parent==null)
            {
                Color currentColor = pS.startColor;
                float currentAlpha = currentColor.a;

                // 투명도를 감소시킵니다.
                currentAlpha -= 0.001f; // 원하는 속도로 조절할 수 있습니다.

                // 투명도가 0보다 작으면 0으로 설정
                if (currentAlpha < 0)
                {
                    destroy();
                }

                // 머티리얼의 알파 값을 업데이트
                pS.startColor = new Color(currentColor.r, currentColor.g, currentColor.b, currentAlpha);
            }
        }
    }

    void destroy()
    {
        print("파티클 삭제하기");
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //프로젝트 세팅에서  layer처리해줘야함
        if(collision.gameObject.CompareTag("Player"))
        {
            transform.GetChild(0).transform.parent = null;
            parents.Attack_To_Player();
            Destroy(gameObject);
        }
       
    }
}
