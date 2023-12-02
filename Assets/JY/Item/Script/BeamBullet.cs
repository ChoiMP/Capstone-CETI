using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBullet : MonoBehaviour
{
    /// <summary>팅김가능 횟수</summary>
    public int count;
    public float speed;
    private AudioSource audioSource;
    public GameObject effect;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Init(int count, float speed=30f)
    {
        this.count = count;
    }

    private void Update()
    {
        // 총알을 z방향으로 이동
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어면 무시
        if (collision.transform.CompareTag("Player"))
        {
            return;
        }
        // 적이면 데미지 주고 삭제
        else if (collision.transform.CompareTag("Enemy"))
        {
            Instantiate(effect, transform.position, transform.rotation);
            audioSource.Play();
            collision.gameObject.GetComponent<Monster>().Hit_the_Monster(10f, effect);
            Destroy(gameObject);
        }
        // 벽이면 카운트 감소, 팅김 (입사각, 반사각 계산 필요?)
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Instantiate(effect, transform.position, transform.rotation);
            audioSource.Play();
            // 벽의 노멀 벡터를 가져옴
            Vector3 wallNormal = collision.contacts[0].normal;

            // 총알의 현재 방향
            Vector3 bulletDirection = transform.forward;

            // 입사각을 계산
            float incidenceAngle = Vector3.Angle(-bulletDirection, wallNormal);

            // 반사각을 계산
            float reflectionAngle = 180f - incidenceAngle;

            // y 회전값을 조절하여 반사각으로 방향 변경
            transform.Rotate(Vector3.up, reflectionAngle);

            // 벽에 부딪혔으므로 카운트 감소
            count--;
        }

        // 카운트가 0이하면 삭제
        if (count <= 0)
        {
            Destroy(gameObject);
        }
    }
}