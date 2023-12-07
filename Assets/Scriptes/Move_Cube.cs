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
            moveTimer = 3f; // 여기에서 moveTimer를 초기화
        }
    }


    void SetRandomMoveDirection()
    {
        float randomAngle = Random.Range(0f, 360f); // 랜덤한 각도 선택
        moveDirection = Quaternion.Euler(0f, 0f, randomAngle) * Vector3.right;
    }

    void MoveObject()
    {
        float movement = speed * Time.deltaTime;

        // 현재 위치에서 이동할 위치 계산
        Vector3 newPosition = transform.position + moveDirection * movement;

        // 벽에 닿았을 때, 다른 방향으로 이동하기 위한 조정
        if (!CheckBounds(newPosition))
        {
            SetRandomMoveDirection();
        }

        // 새로운 위치가 벽에 닿지 않으면 이동
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