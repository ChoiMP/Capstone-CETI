using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storn_Spawner : MonoBehaviour
{
    public GameObject storn;

    public GameObject rangeObject;
    public GameObject ob;
    [SerializeField] BoxCollider boxCollider;
    public float time = 0.1f;

    public GameObject Ston_Danger_View;
    // Start is called before the first frame update
    
    void Start()
    {
        boxCollider = rangeObject.GetComponent<BoxCollider>();
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        // �ݶ��̴��� ����� �������� bound.size ���
        float range_X = boxCollider.bounds.size.x;
        float range_Z = boxCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion;
        respawnPosition += new Vector3(0, 1, 0);
        return respawnPosition;
    }
    // Update is called once per frame
    void Update()
    {
        if(Room_Controller.room == ob && ob.GetComponent<Spawn_Enemy>().spawn_Check == false)
        {
            Ston_Danger_View.SetActive(true);
            StartCoroutine("Destroy_View");
        }
        else
        {
            Ston_Danger_View.SetActive(false);
        }
        if (ob.GetComponent<Spawn_Enemy>().spawn_Check == true)
        {
            
            if (time <= 0)
            {
                print("��ȯ");
                GameObject instantCapsul = Instantiate(storn, Return_RandomPosition(), Quaternion.identity);
                instantCapsul.transform.parent = rangeObject.transform;
                time = 1f;
            }
            else
            {
                time -= Time.deltaTime;
            }
        }
    }

    IEnumerator Destroy_View()
    {
        yield return new WaitForSecondsRealtime(3f);
        Ston_Danger_View.SetActive(false);
    }
}
