using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TempDMGIncrease : Item
{
    public override IEnumerator Function(Player1 player1)
    {
        base.Function(player1);
        // 데미지 관련 변수가 추가되면 작성
        FindObjectOfType<Turret1>().dmg *= 1.5f;
        yield return new WaitForSeconds(5);
        // 데미지 관련 변수가 추가되면 작성
        FindObjectOfType<Turret1>().dmg /= 1.5f;
    }
    public override IEnumerator Function(Player2 player2)
    {
        base.Function(player2);
        // 데미지 관련 변수가 추가되면 작성
        FindObjectOfType<Turret1>().dmg *= 1.5f;
        yield return new WaitForSeconds(5);
        // 데미지 관련 변수가 추가되면 작성
        FindObjectOfType<Turret1>().dmg /= 1.5f;
    }
}
