using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Laser : Item
{
    //바라보는 방향으로 레이저를 발사한다. 레이저는 적이나 벽에 5번 충돌하면 사라진다.
    public GameObject bullet;
    public override IEnumerator Function(Player1 player1)
    {
        base.Function(player1);
        // 바라보는 방향으로 레이저 발사
        GameObject bullet = Instantiate(this.bullet, player1.transform.position,player1.transform.rotation);
        bullet.GetComponent<BeamBullet>().Init(5);
        yield return null;
    }
    public override IEnumerator Function(Player2 player2)
    {
        base.Function(player2);
        // 바라보는 방향으로 레이저 발사
        GameObject bullet = Instantiate(this.bullet, player2.transform.position, player2.transform.rotation);
        bullet.GetComponent<BeamBullet>().Init(5);
        yield return null;
    }
}
