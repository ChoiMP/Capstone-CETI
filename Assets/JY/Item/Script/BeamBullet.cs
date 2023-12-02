using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBullet : MonoBehaviour
{
    /// <summary>�ñ谡�� Ƚ��</summary>
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
        // �Ѿ��� z�������� �̵�
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �÷��̾�� ����
        if (collision.transform.CompareTag("Player"))
        {
            return;
        }
        // ���̸� ������ �ְ� ����
        else if (collision.transform.CompareTag("Enemy"))
        {
            Instantiate(effect, transform.position, transform.rotation);
            audioSource.Play();
            collision.gameObject.GetComponent<Monster>().Hit_the_Monster(10f, effect);
            Destroy(gameObject);
        }
        // ���̸� ī��Ʈ ����, �ñ� (�Ի簢, �ݻ簢 ��� �ʿ�?)
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Instantiate(effect, transform.position, transform.rotation);
            audioSource.Play();
            // ���� ��� ���͸� ������
            Vector3 wallNormal = collision.contacts[0].normal;

            // �Ѿ��� ���� ����
            Vector3 bulletDirection = transform.forward;

            // �Ի簢�� ���
            float incidenceAngle = Vector3.Angle(-bulletDirection, wallNormal);

            // �ݻ簢�� ���
            float reflectionAngle = 180f - incidenceAngle;

            // y ȸ������ �����Ͽ� �ݻ簢���� ���� ����
            transform.Rotate(Vector3.up, reflectionAngle);

            // ���� �ε������Ƿ� ī��Ʈ ����
            count--;
        }

        // ī��Ʈ�� 0���ϸ� ����
        if (count <= 0)
        {
            Destroy(gameObject);
        }
    }
}