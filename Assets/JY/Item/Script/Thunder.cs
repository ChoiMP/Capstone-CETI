using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Thunder : Item
{
    public GameObject effect;
    public override IEnumerator Function(Player1 player1)
    {
        base.Function(player1);
        // 현재 위치를 기준으로 반경 안의 Collider 찾기
        Collider[] colliders = Physics.OverlapSphere(player1.transform.position, 10f);

        // 찾은 Collider들을 순회하며 처리
        foreach (Collider collider in colliders)
        {
            // 찾은 Collider가 적인지 확인
            if (collider.CompareTag("Enemy"))
            {
                // 적에게 데미지 주기
                collider.GetComponent<Monster>().Hit_the_Monster(30f, effect);
                Instantiate(effect, collider.transform.position, Quaternion.identity);
            }
        }
        yield return null;
    }
    public override IEnumerator Function(Player2 player2)
    {
        base.Function(player2);
        // 현재 위치를 기준으로 반경 안의 Collider 찾기
        Collider[] colliders = Physics.OverlapSphere(player2.transform.position, 10f);

        // 찾은 Collider들을 순회하며 처리
        foreach (Collider collider in colliders)
        {
            // 찾은 Collider가 적인지 확인
            if (collider.CompareTag("Enemy"))
            {
                // 적에게 데미지 주기
                collider.GetComponent<Monster>().Hit_the_Monster(30f, effect);
                Instantiate(effect, collider.transform.position, Quaternion.identity);
            }
        }
        yield return null;
    }
}
