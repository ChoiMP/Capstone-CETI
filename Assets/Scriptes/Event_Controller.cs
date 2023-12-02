using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Event_Controller : MonoBehaviour
{
    public TextMeshProUGUI game_Over;
    // Time_Attack()
    public TextMeshProUGUI time_UI;
    public bool time_Attack = false;
    public float time = 60;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time_Attack == true && Room_Controller.room == transform.gameObject && Spawn_Enemy.instance.spawn_Check == true)
        {
            Time_Attack();
        }
    }

    void Time_Attack() // 제한시간
    {
        time_UI.gameObject.SetActive(true);
        time -= Time.deltaTime;
        time_UI.text = Mathf.FloorToInt(time).ToString();
        if (time <= 0)
        {
            if (Room_Controller.room.GetComponent<Spawn_Enemy>().clear_Check == false)
            {
                game_Over.gameObject.SetActive(true);
                time_UI.text = "0";
                Time.timeScale = 0;   
                print("게임 오버");
            }
        }
        else
        {
            if (Room_Controller.room.GetComponent<Spawn_Enemy>().clear_Check == true)
            {
                time_Attack = false;
                time_UI.gameObject.SetActive(false);
            }
        }
    }
}
