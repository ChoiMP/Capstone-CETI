using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour,IPointerEnterHandler 
{
    public Item item;
    public GameObject itemUI;
    GameObject newItemUI;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        //newItemUI = Instantiate(itemUI, Input.mousePosition, Quaternion.identity, canvas.transform);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null) return;
        // Debug.Log(item.itemName + "설명출력");
        newItemUI = Instantiate(itemUI, Input.mousePosition, Quaternion.identity, canvas.transform);
        newItemUI.GetComponent<NewItemUI>().Init(item.itemName,item.itemIndex);
    }

    
}
