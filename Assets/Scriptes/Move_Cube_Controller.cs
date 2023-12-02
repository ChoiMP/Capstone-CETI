using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Cube_Controller : MonoBehaviour
{
    public GameObject check_Ob;
    public GameObject[] move_cube;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(check_Ob != null)
        {
            if (check_Ob.GetComponent<Spawn_Enemy>().clear_Check == true)
            {
                foreach (GameObject a in move_cube)
                {
                    a.SetActive(false);
                }
            }
            else
            {
                foreach (GameObject a in move_cube)
                {
                    a.SetActive(true);
                }
            }
        }
        
    }
}
