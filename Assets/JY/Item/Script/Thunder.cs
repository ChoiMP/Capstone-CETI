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
        // ���� ��ġ�� �������� �ݰ� ���� Collider ã��
        Collider[] colliders = Physics.OverlapSphere(player1.transform.position, 10f);

        // ã�� Collider���� ��ȸ�ϸ� ó��
        foreach (Collider collider in colliders)
        {
            // ã�� Collider�� ������ Ȯ��
            if (collider.CompareTag("Enemy"))
            {
                // ������ ������ �ֱ�
                collider.GetComponent<Monster>().Hit_the_Monster(30f, effect);
                Instantiate(effect, collider.transform.position, Quaternion.identity);
            }
        }
        yield return null;
    }
    public override IEnumerator Function(Player2 player2)
    {
        base.Function(player2);
        // ���� ��ġ�� �������� �ݰ� ���� Collider ã��
        Collider[] colliders = Physics.OverlapSphere(player2.transform.position, 10f);

        // ã�� Collider���� ��ȸ�ϸ� ó��
        foreach (Collider collider in colliders)
        {
            // ã�� Collider�� ������ Ȯ��
            if (collider.CompareTag("Enemy"))
            {
                // ������ ������ �ֱ�
                collider.GetComponent<Monster>().Hit_the_Monster(30f, effect);
                Instantiate(effect, collider.transform.position, Quaternion.identity);
            }
        }
        yield return null;
    }
}
