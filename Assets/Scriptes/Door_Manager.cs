using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Manager : MonoBehaviour
{
    public static Door_Manager instance;
    public List<GameObject> door;
    public GameObject test;
    // Start is called before the first frame update
    public GameObject start_Plant;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        GameObject[] dor;
        dor = GameObject.FindGameObjectsWithTag("Door");

        for(int i = 0; i < dor.Length; i++)
        {
            door.Add(dor[i]);
        }
    }

    public void Off_Door()
    {
        //print("¹® ¿°");
        foreach (GameObject d in door)
        {
            d.SetActive(false);
        }
    }
    public void On_Door()
    {
        //print("¹® ´ÝÀ½");
        foreach (GameObject d in door)
        {
            d.SetActive(true);
        }
    }

    private void Update()
    {
        if (test != null)
        {
            if (Room_Controller.room.GetComponent<Spawn_Enemy>().rangeObject.transform.childCount == 0)
            {
                Room_Controller.room.GetComponent<Spawn_Enemy>().clear_Check = true;
                Off_Door();
            }
            else
            {
                Room_Controller.room.GetComponent<Spawn_Enemy>().clear_Check = false;
                On_Door();
            }

        }
        else if (start_Plant.GetComponent<Start_Plant_UI>().check == false)
        {
            if (start_Plant.GetComponent<Start_Plant_UI>().rangeObject.transform.childCount == 0)
            {
                start_Plant.GetComponent<Start_Plant_UI>().clear_Check_S = true;
                Off_Door();
            }
            else
            {
                start_Plant.GetComponent<Start_Plant_UI>().clear_Check_S = false;
                On_Door();
            }
        }
        else
        {
        }
    }

    public void Clear_Check()
    {

    }
}
