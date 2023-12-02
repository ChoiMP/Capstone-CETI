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

                // ������ ���ҽ�ŵ�ϴ�.
                currentAlpha -= 0.001f; // ���ϴ� �ӵ��� ������ �� �ֽ��ϴ�.

                // ������ 0���� ������ 0���� ����
                if (currentAlpha < 0)
                {
                    destroy();
                }

                // ��Ƽ������ ���� ���� ������Ʈ
                pS.startColor = new Color(currentColor.r, currentColor.g, currentColor.b, currentAlpha);
            }
        }
    }

    void destroy()
    {
        print("��ƼŬ �����ϱ�");
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //������Ʈ ���ÿ���  layeró���������
        if(collision.gameObject.CompareTag("Player"))
        {
            transform.GetChild(0).transform.parent = null;
            parents.Attack_To_Player();
            Destroy(gameObject);
        }
       
    }
}
