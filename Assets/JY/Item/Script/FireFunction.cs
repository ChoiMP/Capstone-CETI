using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFunction : MonoBehaviour
{
    public GameObject effect;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Fire());
    }
    IEnumerator Fire()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            foreach (Collider collider in colliders)
            {
                // ã�� Collider�� ������ Ȯ��
                if (collider.CompareTag("Enemy"))
                {
                    // ������ ������ �ֱ�
                    collider.GetComponent<Monster>().Hit_the_Monster(10f, effect);
                    Instantiate(effect, collider.transform.position, Quaternion.identity);
                }
            }
        }
    }
}
