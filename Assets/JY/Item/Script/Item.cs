using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itemName;
    public string itemIndex;
    public bool isActive;
    public Sprite itemImage;

    /// <summary>
    /// Player1老锭 角青
    /// </summary>
    /// <param name="player1"></param>
    public virtual IEnumerator Function(Player1 player1)
    {
        Debug.Log(player1.name + "use" + name);
        yield break;
    }
    /// <summary>
    /// Player2老锭 角青
    /// </summary>
    /// <param name="player2"></param>
    public virtual IEnumerator Function(Player2 player2)
    {
        Debug.Log(player2.name + "use" + name);
        yield break;
    }
    public IEnumerator timer(float time)
    {
        yield return new WaitForSeconds(time);
    }
}