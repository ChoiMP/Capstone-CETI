using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Beam : Item
{
    //���� �� �� �Ǵ� �����ȿ� ������ ���� �߻��Ѵ�. ���� ������ �������.
    public GameObject bullet;
    private Vector3 dir;
    public override IEnumerator Function(Player1 player1)
    {
        base.Function(player1);

        // ���� ��ġ�� �������� �ݰ� ���� Collider ã��
        Collider[] colliders = Physics.OverlapSphere(player1.transform.position, 20f);

        // ã�� Collider���� ��ȸ�ϸ� ó��
        foreach (Collider collider in colliders)
        {
            // ã�� Collider�� ������ Ȯ��
            if (collider.CompareTag("Enemy"))
            {
                // �� �������� ������ �߻�
                Vector3 dir = collider.transform.position - player1.transform.position;
                dir.Normalize(); // ������ ����ȭ�Ͽ� ���� ���ͷ� ����ϴ�.

                // �������� ������ �� ���� ���͸� ����Ͽ� Quaternion�� ����
                Quaternion rotation = Quaternion.LookRotation(dir);
                GameObject bullet = Instantiate(this.bullet, player1.transform.position, rotation);
                bullet.GetComponent<BeamBullet>().Init(2);
                //Debug.Log("Bullet ����");
            }
        }
        yield return null;
    }
    public override IEnumerator Function(Player2 player2)
    {
        base.Function(player2);
        // ���� ��ġ�� �������� �ݰ� ���� Collider ã��
        Collider[] colliders = Physics.OverlapSphere(player2.transform.position, 20f);

        // ã�� Collider���� ��ȸ�ϸ� ó��
        foreach (Collider collider in colliders)
        {
            // ã�� Collider�� ������ Ȯ��
            if (collider.CompareTag("Enemy"))
            {
                // �� �������� ������ �߻�
                Vector3 dir = collider.transform.position - player2.transform.position;
                dir.Normalize(); // ������ ����ȭ�Ͽ� ���� ���ͷ� ����ϴ�.

                // �������� ������ �� ���� ���͸� ����Ͽ� Quaternion�� ����
                Quaternion rotation = Quaternion.LookRotation(dir);
                GameObject bullet = Instantiate(this.bullet, player2.transform.position, rotation);
                bullet.GetComponent<BeamBullet>().Init(2);
                //Debug.Log("Bullet ����");
            }
        }
        yield return null;
    }
}
