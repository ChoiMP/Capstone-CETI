using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Cube : MonoBehaviour
{
    public float speed = 1f;
    public GameObject rangeObject;
    [SerializeField] BoxCollider boxCollider;

    public float moveTimer = 3f;
    public float directionTimer = 3f;

    private Vector3 moveDirection;

    private void Start()
    {
        boxCollider = rangeObject.GetComponent<BoxCollider>();
        SetRandomMoveDirection();


    }

    // Update is called once per frame
    void Update()
    {
        directionTimer -= Time.deltaTime;

        if (directionTimer <= 0)
        {
            SetRandomMoveDirection();
            directionTimer = 3f;
        }

        moveTimer -= Time.deltaTime;

        if (moveTimer > 0)
        {
            MoveObject();
        }
        else
        {
            moveTimer = 3f; // ���⿡�� moveTimer�� �ʱ�ȭ
        }
    }


    void SetRandomMoveDirection()
    {
        float randomAngle = Random.Range(0f, 360f); // ������ ���� ����
        moveDirection = Quaternion.Euler(0f, 0f, randomAngle) * Vector3.right;
    }

    void MoveObject()
    {
        float movement = speed * Time.deltaTime;

        // ���� ��ġ���� �̵��� ��ġ ���
        Vector3 newPosition = transform.position + moveDirection * movement;

        // ���� ����� ��, �ٸ� �������� �̵��ϱ� ���� ����
        if (!CheckBounds(newPosition))
        {
            SetRandomMoveDirection();
        }

        // ���ο� ��ġ�� ���� ���� ������ �̵�
        if (CheckBounds(newPosition))
        {
            transform.Translate(moveDirection * movement);
        }
    }

    bool CheckBounds(Vector3 newPosition)
    {
        return boxCollider.bounds.Contains(newPosition);
    }
}