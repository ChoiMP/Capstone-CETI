using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MSIncrease : Item
{
    public override IEnumerator Function(Player1 player1)
    {
        base.Function(player1);
        player1.moveSpeed *= 1.1f;
        yield return null;
    }
    public override IEnumerator Function(Player2 player2)
    {
        base.Function(player2);
        player2.moveSpeed *= 1.1f;
        yield return null;
    }
}
