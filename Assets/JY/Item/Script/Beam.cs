using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Beam : Item
{
    //현재 방 안 또는 범위안에 적에게 빔을 발사한다. 벽에 닿으면 사라진다.
    public GameObject bullet;
    private Vector3 dir;
    public override IEnumerator Function(Player1 player1)
    {
        base.Function(player1);

        // 현재 위치를 기준으로 반경 안의 Collider 찾기
        Collider[] colliders = Physics.OverlapSphere(player1.transform.position, 20f);

        // 찾은 Collider들을 순회하며 처리
        foreach (Collider collider in colliders)
        {
            // 찾은 Collider가 적인지 확인
            if (collider.CompareTag("Enemy"))
            {
                // 적 방향으로 레이저 발사
                Vector3 dir = collider.transform.position - player1.transform.position;
                dir.Normalize(); // 방향을 정규화하여 단위 벡터로 만듭니다.

                // 레이저를 생성할 때 방향 벡터를 사용하여 Quaternion을 생성
                Quaternion rotation = Quaternion.LookRotation(dir);
                GameObject bullet = Instantiate(this.bullet, player1.transform.position, rotation);
                bullet.GetComponent<BeamBullet>().Init(2);
                //Debug.Log("Bullet 생성");
            }
        }
        yield return null;
    }
    public override IEnumerator Function(Player2 player2)
    {
        base.Function(player2);
        // 현재 위치를 기준으로 반경 안의 Collider 찾기
        Collider[] colliders = Physics.OverlapSphere(player2.transform.position, 20f);

        // 찾은 Collider들을 순회하며 처리
        foreach (Collider collider in colliders)
        {
            // 찾은 Collider가 적인지 확인
            if (collider.CompareTag("Enemy"))
            {
                // 적 방향으로 레이저 발사
                Vector3 dir = collider.transform.position - player2.transform.position;
                dir.Normalize(); // 방향을 정규화하여 단위 벡터로 만듭니다.

                // 레이저를 생성할 때 방향 벡터를 사용하여 Quaternion을 생성
                Quaternion rotation = Quaternion.LookRotation(dir);
                GameObject bullet = Instantiate(this.bullet, player2.transform.position, rotation);
                bullet.GetComponent<BeamBullet>().Init(2);
                //Debug.Log("Bullet 생성");
            }
        }
        yield return null;
    }
}
