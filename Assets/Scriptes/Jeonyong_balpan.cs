using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jeonyong_balpan : MonoBehaviour
{
    public GameObject[] balpan;

    public GameObject rangeObject;

    GameObject instantCapsul;
    //public GameObject ob;
    [SerializeField] BoxCollider boxCollider;
    public float time = 5f;

    public GameObject View;
    // Start is called before the first frame update
    void Start()
    {
        //boxCollider = rangeObject.GetComponent<BoxCollider>();
    }
    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = boxCollider.bounds.size.x;
        float range_Z = boxCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion;
        respawnPosition += new Vector3(0, -0.7f, 0);
        return respawnPosition;
    }

    private void Update()
    {
        if (Room_Controller.room == rangeObject && rangeObject.GetComponent<Spawn_Enemy>().spawn_Check == false)
        {
            View.SetActive(true);
        }
        else
        {
            View.SetActive(false);
        }

        if (rangeObject.GetComponent<Spawn_Enemy>().clear_Check == false && rangeObject.GetComponent<Spawn_Enemy>().spawn_Check == true)
        {
            if (time <= 0)
            {
                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
                    Destroy(gameObject.transform.GetChild(i).gameObject);
                }

                for (int i = 0; i < balpan.Length; i++)
                {
                    instantCapsul = Instantiate(balpan[i], Return_RandomPosition(), Quaternion.identity);
                    instantCapsul.transform.parent = gameObject.transform;
                }
                time = 5f;
            }
            else
            {
                time -= Time.deltaTime;
            }
        }
        else
        {
            for(int i = 0; i < gameObject.transform.childCount; i++)
            {
                Destroy(gameObject.transform.GetChild(i).gameObject);
            }
        }
    }
}
