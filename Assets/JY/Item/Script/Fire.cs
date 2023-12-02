using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Fire : Item
{
    public GameObject effect;
    public GameObject fire;
    public override IEnumerator Function(Player1 player1)
    {
        GameObject fireObject =  Instantiate(fire, player1.transform.position, Quaternion.identity,player1.transform);
        for (int i = 0; i < 10; i++)
        {
            FindAndAttack(player1.transform);
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(fireObject);

    }
    public override IEnumerator Function(Player2 player2)
    {
        base.Function(player2);
        GameObject fireObject = Instantiate(fire, player2.transform.position, Quaternion.identity, player2.transform);
        for (int i = 0; i < 10; i++)
        {
            FindAndAttack(player2.transform);
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(fireObject);
    }

    private void FindAndAttack(Transform origin)
    {
        // 현재 위치를 기준으로 반경 안의 Collider 찾기
        Collider[] colliders = Physics.OverlapSphere(origin.transform.position, 5f);

        // 찾은 Collider들을 순회하며 처리
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // 적에게 데미지 주기
                collider.GetComponent<Monster>().Hit_the_Monster(10f, effect);
            }
        }
    }
}
