using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> items;

    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private List<Image> slots;
    int count = 0;
    private void OnValidate()
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);
            slots.Add(childTransform.GetComponentInChildren<Image>());
        }
    }

    void Awake()
    {
        
    }


    public void AddItem(Item _item)
    {
        if (items.Count < slots.Count)
        {
            items.Add(_item);
            slots[count].sprite = _item.itemImage;
            slots[count].GetComponent<ItemUI>().item = _item;
            count++;
        }
        else
        {
            Debug.Log("슬롯이 가득 차 있습니다.");
        }
    }
    /// <summary>
    /// UI창을 켜고끄는 기능
    /// </summary>
    /// <param name="isActive"></param>
    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}