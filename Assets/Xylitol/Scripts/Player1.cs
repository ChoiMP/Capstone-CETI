using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class Player1 : MonoBehaviour
{
    Rigidbody rigid;

    float hAxis;
    float vAxis;

    Vector3 moveDir;
    public float moveSpeed = 4f;
    public float rotSpeed = 10f;   
    
    Animator anim;

    public int current_XP = 0;

    public int player1_hp = 3;
    public float player1_maxHp = 3f;

    private bool resDown;
    public static float resurrectDis = 4f; //부활 가능 거리
    public float resurrectChargeTime = 3f;
    public float chargeTime;
    public bool canRes1 = false;

    private Vector3 knockBackDir;
    private Vector3 knockBackPos;
    private Vector3 knockBackPosUp;
    public float knockBackForce = 20f;
    public float knockBackForceUp = 10f;

    public bool isDied = false;

    // -----------맵 담당자가 수정한 부분-------------
    public static Player1 instance;

    private void Awake()
    {
        instance = this;
    }
    //-----------------------------------------------
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

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
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        resDown = Input.GetKey(KeyCode.F);
    }

    void Move()
    {
        moveDir = new Vector3(hAxis, 0, vAxis).normalized;

        if (player1_hp > 0)
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
        Vector3 player2Pos = GameObject.Find("Player2 robot").transform.position;
        float distance = Vector3.Distance(transform.position, player2Pos);

        UIManager uiManager = GameObject.Find("GameManager").GetComponent<UIManager>();

        if (resurrectDis > distance)
        {
            Player2 player2 = GameObject.FindObjectOfType<Player2>();
            if (player2.player2_hp <= 0)
            {        
                uiManager.ResurrctUIOnp2();

                //Debug.Log(uiManager.ResurrectTextP2.text);

                if (resDown)
                {
                    chargeTime += Time.deltaTime;
                    uiManager.resChargeBarp2.fillAmount = chargeTime / resurrectChargeTime;
                    if (chargeTime >= resurrectChargeTime)
                    {
                        player2.Resurrection_p2();
                        player2.isDied = false;
                        chargeTime = 0;
                    }
                }
                else
                {
                    chargeTime = 0;
                    uiManager.resChargeBarp2.fillAmount = 0;
                }
            }
            else
            {
                uiManager.ResurrctUIOffp2();
            }
        }
        else
        {
            uiManager.ResurrctUIOffp2();
        }
    }

    public void Resurrection_p1()
    {
        anim.SetTrigger("doOpen");
        rigid.constraints = RigidbodyConstraints.FreezePositionY |
            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        player1_hp = (int)player1_maxHp/2;

        rigid.isKinematic = false;
        UIManager uiManager = GameObject.Find("GameManager").GetComponent<UIManager>();
        uiManager.ResurrctUIOffp2();
       
    }

    IEnumerator KnockBack()
    {
        yield return new WaitForSeconds(0.05f);
        knockBackPos = transform.position + knockBackDir * knockBackForce;
        knockBackPosUp = transform.position + transform.up * knockBackForceUp;

        rigid.AddForce(knockBackPos + knockBackPosUp, ForceMode.Impulse);

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PowerUp")
        {
            Destroy(collision.gameObject);
            current_XP++;
            //Debug.Log("획득");
            //Debug.Log(current_XP);

        }
        if (!isDied)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                player1_hp--;
                //Debug.Log(player1_hp);


                if (player1_hp <= 0)
                {
                    PlayerDie();
                }
                //knockBackDir = -(collision.transform.position - transform.position);
                //StartCoroutine(KnockBack());
            }
        }
       
    }


}