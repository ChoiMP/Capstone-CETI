using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HpIncrease : Item
{
    public override IEnumerator Function(Player1 player1)
    {
        base.Function(player1);
        player1.player1_hp += 10;
        yield return null;
    }
    public override IEnumerator Function(Player2 player2)
    {
        base.Function(player2);
        player2.player2_hp += 10;
        yield return null;
    }
}
