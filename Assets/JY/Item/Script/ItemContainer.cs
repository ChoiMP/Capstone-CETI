using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ItemContainer : MonoBehaviour
{
    Player1 player1;
    Player2 player2;
    /// <summary> player1이면 1, player2면 2 </summary>
    [Range(1,2),Header(" player1이면 1, player2면 2")]
    public int PlayerInt;
    public Inventory inventory;
    public List<Item> passiveItem;
    public Item activeItem;
    public Image activeItemImage;
    public Text activeItemName;
    public Text activeItemIndex;
    private Sprite activeUIDefault;
    public GameObject itemBubble;
    public KeyCode itemInput1;
    public KeyCode itemInput2;
    private bool ispaused;
    public ItemContainer[] activeItems;

    public AudioSource audioSource;

    public GameObject itemEffect;
    public AudioClip itemSound;

    private void Awake()
    {
        player1 = FindAnyObjectByType<Player1>();
        player2 = FindAnyObjectByType<Player2>();
        
        activeItems = FindObjectsOfType<ItemContainer>();
        audioSource =GetComponent<AudioSource>();
        audioSource.clip = itemSound;
    }

    // Start is called before the first frame update
    private void Start()
    {
        //playerController = GetComponent<Player1>();
        activeItem = null;
        activeUIDefault = activeItemImage.sprite;
        activeItemName .text =null;
        activeItemIndex.text =null;

    }



    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        // 아이템 사용 입력시, activeItem확인하여 기능실행
        if (activeItem != null && (Input.GetKeyDown(itemInput1) || Input.GetKeyDown(itemInput2)))
        {
            if (Input.GetKeyDown(itemInput1))
            {
                UseItem(1);
            }
            else if (Input.GetKeyDown(itemInput2))
            {
                UseItem(2);
            }
            
        }
        if (!ispaused&&Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            ispaused = true;
            inventory.gameObject.SetActive(ispaused);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1f;
            ispaused = false;
            inventory.gameObject.SetActive(ispaused);
        }
    }
    /// <summary> 아이템 획득 </summary>
    public void AddItem(Item item)
    {
        // 아이템 획득 사운드
        audioSource.Play();
        // 아이템 획득 이펙트
        GameObject effect = Instantiate(itemEffect,transform.position,itemEffect.transform.rotation);
        effect.AddComponent<EffectDestroy>();
        // 사용아이템인지 아닌지 확인
        if (item.isActive)
        {

            // 사용아이템일시 activeItem에 할당, 기존 아이템이 있으면 기존 아이템 드랍
            // 액티브 아이템을 가지고 있는지 확인
            if (activeItems[0].activeItem != null|| activeItems[1].activeItem != null)
            {
                // 가지고 있으면 아이템 버블에 기존 아이템을 담아서 배출
                Instantiate(itemBubble,transform.position + Vector3.up * 2, Quaternion.identity).GetComponent<ItemBubble>().SetItem(activeItem);

            }
            // 각 아이템 컨테이너에 아이템을 넣고 이미지를 바꿈
            foreach (ItemContainer container in activeItems)
            {
                container.activeItem = item;
                activeItemImage.sprite = item.itemImage;
                activeItemName.text =  item.itemName;
                activeItemIndex.text = item.itemIndex;
            }

        }
        // 능력치아이템일시 passiveItem 리스트에 추가, UseItem도 실행
        else
        {
            inventory.AddItem(item);
            passiveItem.Add(item);
            UseItem(item);
        }
    }
    /// <summary> 패시브아이템 사용 </summary>
    public void UseItem(Item item)
    {
        switch (PlayerInt)
        {
            case 0:
                break;
            case 1:
                StartCoroutine(item.Function(player1));
                break;
            case 2:
                StartCoroutine(item.Function(player2));
                break;
            default:
                break;
        }
    }
    /// <summary>액티브 아이템 사용</summary>
    /// <param name="playerInt">플레이어 번호(1,2)</param>
    public void UseItem(int playerInt)
    {
        switch (playerInt)
        {
            case 0:
                return;
            case 1:
                if (player1.player1_hp > 0)
                {
                    StartCoroutine( activeItem.Function(player1));
                    break;
                }
                else return;
            case 2:
                if (player2.player2_hp > 0)
                {
                    StartCoroutine(activeItem.Function(player2));
                    break;
                }
                return;
            default:
                return;
        }
        foreach (ItemContainer container in activeItems)
        {
            container.activeItem = null;
            activeItemImage.sprite = activeUIDefault;
            activeItemName.text = null;
            activeItemIndex.text = null;
        }
    }
}