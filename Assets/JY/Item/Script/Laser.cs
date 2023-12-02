using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Laser : Item
{
    //�ٶ󺸴� �������� �������� �߻��Ѵ�. �������� ���̳� ���� 5�� �浹�ϸ� �������.
    public GameObject bullet;
    public override IEnumerator Function(Player1 player1)
    {
        base.Function(player1);
        // �ٶ󺸴� �������� ������ �߻�
        GameObject bullet = Instantiate(this.bullet, player1.transform.position,player1.transform.rotation);
        bullet.GetComponent<BeamBullet>().Init(5);
        yield return null;
    }
    public override IEnumerator Function(Player2 player2)
    {
        base.Function(player2);
        // �ٶ󺸴� �������� ������ �߻�
        GameObject bullet = Instantiate(this.bullet, player2.transform.position, player2.transform.rotation);
        bullet.GetComponent<BeamBullet>().Init(5);
        yield return null;
    }
}
