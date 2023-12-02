using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

[CreateAssetMenu]
public class TempMSIncrease : Item
{
    public override IEnumerator Function(Player1 player1)
    {
        base.Function(player1);
        player1.moveSpeed *= 1.5f;
        yield return new WaitForSeconds(5);
        player1.moveSpeed /= 1.5f;
    }
    public override IEnumerator Function(Player2 player2)
    {
        base.Function(player2); 
        player2.moveSpeed *= 1.5f;
        yield return new WaitForSeconds(5);
        player2.moveSpeed /= 1.5f;
    }
}
