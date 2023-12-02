using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    /// <summary>
    /// ������ ������ ����
    /// </summary>
    public GameObject itemBubble;
    /// <summary>
    /// �� ������ ����(��ũ��Ʈ)
    /// </summary>
    public List<Item> ItemList;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameObject _itemBubble =  Instantiate(itemBubble, this.transform.position,Quaternion.identity);
            Item randomItem = ItemList[Random.Range(0, ItemList.Count)];
            _itemBubble.GetComponent<ItemBubble>().SetItem(randomItem);
            Destroy(gameObject);
        }
    }
}
