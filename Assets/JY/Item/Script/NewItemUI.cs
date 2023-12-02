using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NewItemUI : MonoBehaviour,  IPointerExitHandler
{
    public Text nameUI;
    public Text indexUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init(string name, string index)
    {
        nameUI.text = name;
        indexUI.text = index;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(this.gameObject);
    }
}
