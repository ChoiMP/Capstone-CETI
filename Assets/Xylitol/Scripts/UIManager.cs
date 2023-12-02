using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image player1HpBar;
    public Image player2HpBar;

    public Text ResurrectTextP1;
    public Image resChargeBarp1;

    public Text ResurrectTextP2;
    public Image resChargeBarp2;


    void Start()
    {
       
    }

    void Update()
    {
        Player1HpManage();
        Player2HpManage();
     /*   ResurrctUIOffp1();
        ResurrctUIOffp2();*/



    }

    void Player1HpManage()
    {
        Player1 player1 = GameObject.FindObjectOfType<Player1>();
        int hp = player1.player1_hp;
        float maxHp = player1.player1_maxHp;
        player1HpBar.fillAmount = hp / maxHp;
    }
    void Player2HpManage()
    {
        Player2 player2 = GameObject.FindObjectOfType<Player2>();
        int hp = player2.player2_hp;
        float maxHp = player2.player2_maxHp;
        player2HpBar.fillAmount = hp / maxHp;
    }

    public void ResurrctUIOnp1()
    {
        ResurrectTextP1.text = "부활시키기";
    }
    public void ResurrctUIOffp1()
    {
        ResurrectTextP1.text = "";
        resChargeBarp1.fillAmount = 0;

    }


    public void ResurrctUIOnp2()
    {
        ResurrectTextP2.text = "부활시키기";

    }
    public void ResurrctUIOffp2()
    {
        ResurrectTextP2.text = "";
        resChargeBarp2.fillAmount = 0;
    }
}
