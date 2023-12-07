using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    Rigidbody rigid;

    float hAxis;
    float vAxis;

    Vector3 moveDir;
    public float moveSpeed = 4f;
    public float rotSpeed = 10f;

    Animator anim;

    public int player2_hp = 3;
    public float player2_maxHp = 3f;

    private bool resDown;
    public static float resurrectDis = 4f; //부활 가능 거리
    public float resurrectChargeTime = 3f;
    public float chargeTime;

    private Vector3 knockBackDir;
    private Vector3 knockBackPos;
    private Vector3 knockBackPosUp;
    public float knockBackForce = 20f;
    public float knockBackForceUp = 10f;

    public bool isDied = false;

    // -----------맵 담당자가 수정한 부분-------------
    public static Player2 instance;

    private void Awake()
    {
        instance = this;
    }
    //-----------------------------------------------
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim= GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();

    }

    void Update()
    {
        InputManager();
        if (!isDied)
        {
            Resurrect();

        }
    }
   

    void InputManager()
    {
        hAxis = Input.GetAxisRaw("Horizontal2");
        vAxis = Input.GetAxisRaw("Vertical2");
        resDown = Input.GetKey(KeyCode.RightShift);
    }

    void Move()
    {
        moveDir = new Vector3(hAxis, 0, vAxis).normalized;

        if (player2_hp > 0)
        {
            if (!(hAxis == 0 && vAxis == 0))
            {
                transform.position += moveDir * moveSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Lerp(transform.rotation,
                    Quaternion.LookRotation(moveDir), Time.deltaTime * rotSpeed);
            }
            if (moveDir.x != 0 || moveDir.z != 0)
            {
                anim.SetBool("isWalk", true);
            }
            else
            {
                anim.SetBool("isWalk", false);
            }
        }
    }

    void Rotate()
    {
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PowerUp")
        {
            Destroy(collision.gameObject);
            Player1 player = GameObject.Find("Player1 robot").GetComponent<Player1>();
            player.current_XP++;
            Debug.Log("획득");
            Debug.Log(player.current_XP);
        }
        if (!isDied)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                player2_hp--;
                Debug.Log(player2_hp);

                if (player2_hp <= 0)
                {
                    PlayerDie();
                }

                //knockBackDir = -(collision.transform.position - transform.position);
                //StartCoroutine(KnockBack());
            }
        }
       
    }

    void PlayerDie()
    {
        anim.SetTrigger("doClose");
        anim.SetBool("isWalk", false);

        rigid.constraints = RigidbodyConstraints.FreezePosition;
        rigid.constraints = RigidbodyConstraints.FreezeRotation;

        rigid.isKinematic = true;
        isDied = true;
    }

    void Resurrect() //부활
    {
        Vector3 player1Pos = GameObject.Find("Player1 robot").transform.position;
        float distance = Vector3.Distance(transform.position, player1Pos);

        UIManager uiManager = GameObject.FindObjectOfType<UIManager>();

        if (resurrectDis > distance)
        {
            Player1 player1 = GameObject.FindObjectOfType<Player1>();
            if (player1.player1_hp <= 0)
            {
                uiManager.ResurrctUIOnp1();
                if (resDown)
                {
                    chargeTime += Time.deltaTime;
                    uiManager.resChargeBarp1.fillAmount = chargeTime / resurrectChargeTime;
                    if (chargeTime >= resurrectChargeTime)
                    {
                        player1.Resurrection_p1();
                        player1.isDied=false;
                        chargeTime = 0;
                    }
                }
                else
                {
                    chargeTime = 0;
                    uiManager.resChargeBarp1.fillAmount = 0;
                }
            }
            else
            {
                uiManager.ResurrctUIOffp1();
            }
        }
        else
        {
            uiManager.ResurrctUIOffp1();
        }
    }

    public void Resurrection_p2()
    {
        anim.SetTrigger("doOpen");
        rigid.constraints = RigidbodyConstraints.FreezePositionY |
           RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        player2_hp = (int)player2_maxHp/2;

        rigid.isKinematic = false;
        UIManager uiManager = GameObject.Find("GameManager").GetComponent<UIManager>();
        uiManager.ResurrctUIOffp1();
    }

    IEnumerator KnockBack()
    {
        yield return new WaitForSeconds(0.05f);
        knockBackPos = transform.position + knockBackDir * knockBackForce;
        knockBackPosUp = transform.position + transform.up * knockBackForceUp;

        rigid.AddForce(knockBackPos + knockBackPosUp, ForceMode.Impulse);
    }

}
