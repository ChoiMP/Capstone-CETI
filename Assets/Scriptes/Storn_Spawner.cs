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
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = rangeObject.GetComponent<BoxCollider>();
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
        respawnPosition += new Vector3(0, 1, 0);
        return respawnPosition;
    }
    // Update is called once per frame
    void Update()
    {
        if(ob.GetComponent<Spawn_Enemy>().spawn_Check == true)
        {
            if (time <= 0)
            {
                print("소환");
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
}
