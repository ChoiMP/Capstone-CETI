using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class XPIncrease : Item
{
    public override IEnumerator Function(Player1 player1)
    {
        base.Function(player1);
        player1.current_XP++;
        yield return null;
    }
    public override IEnumerator Function(Player2 player2)
    {
        base.Function(player2);
        FindObjectOfType<Player1>().current_XP++;
        yield return null;
    }
}
