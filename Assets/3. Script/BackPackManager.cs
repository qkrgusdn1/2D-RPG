using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackPackManager : MonoBehaviour
{
    public static BackPackManager Instance;
    public GameObject backPack_UI;
    public Text coinText;

    public Image[] itemImages;
    InventoryItemData[] inventoryItemDatas;

    int defItemUsingCount;
    int speedItemUsingCount = 0;
    int powerItemUsingCount = 0;
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        inventoryItemDatas = new InventoryItemData[itemImages.Length];
    }

    void Update()
    {
        BackPackUIOn();
        coinText.text = $"Coin: {GameManager.Instance.coin:N0}";
    }
    void BackPackUIOn()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            backPack_UI.SetActive(!backPack_UI.activeSelf);
        }
    }

    IEnumerator DefItem()
    {
        defItemUsingCount++;
        GameManager.Instance.playerDef *= 2;
        GameManager.Instance.Character.GetComponent<SpriteRenderer>().color = Color.blue;
        Debug.Log("1. PlayerDef : " + GameManager.Instance.playerDef);
        yield return new WaitForSeconds(10f);

        defItemUsingCount--;
        GameManager.Instance.playerDef /= 2;
        if (defItemUsingCount == 0)
        {
            GameManager.Instance.Character.GetComponent<SpriteRenderer>().color = Color.white;
        }
        Debug.Log("2. PlayerDef : " + GameManager.Instance.playerDef);
    }

    public bool AddItem(InventoryItemData item)
    {
        for(int i = 0; i < itemImages.Length; i++)
        {
            if (itemImages[i].sprite == null)
            {
                itemImages[i].sprite = item.itemImage;
                inventoryItemDatas[i] = item;
                return true;
            }
        }
        return false;
    }
    public void ItemUse()
    {
        int siblingIndex = EventSystem.current.currentSelectedGameObject.transform.parent.GetSiblingIndex();
        InventoryItemData inventoryItem = inventoryItemDatas[siblingIndex];
        if (inventoryItem == null) return;

        if (inventoryItem.itemID == "HP")
        {
            GameManager.Instance.playerHp += 10f;
            GameManager.Instance.playerHp = Mathf.Min(GameManager.Instance.playerHp, 100f);
            PopupMsgManager.Instance.ShowPopupMessage("체력이 10 회복 되었습니다.");
        }
        else if (inventoryItem.itemID == "MP")
        {
            GameManager.Instance.playerMp += 10f;
            GameManager.Instance.playerMp = Mathf.Min(GameManager.Instance.playerHp, 100f);
            PopupMsgManager.Instance.ShowPopupMessage("마나가 10 회복 되었습니다.");
        }
        else if (inventoryItem.itemID == "HP_Power")
        {
            GameManager.Instance.playerHp = 100f;
            PopupMsgManager.Instance.ShowPopupMessage("체력 전체가 회복 되었습니다.");
        }
        else if (inventoryItem.itemID == "MP_Power")
        {
            GameManager.Instance.playerMp = 100f;
            PopupMsgManager.Instance.ShowPopupMessage("마나 전체가 회복 되었습니다.");
        }
        else if(inventoryItem.itemID == "Def")
        {
            StartCoroutine(DefItem());
        }
        else if (inventoryItem.itemID == "Speed")
        {
            StartCoroutine(SpeedItem());
        }
        else if (inventoryItem.itemID == "Power")
        {
            StartCoroutine(PowerItem());
        }
        else if (inventoryItem.itemID == "Super")
        {
           
        }
        else
        {
            Debug.LogError($"존재하지 않는 itemID[{inventoryItem.itemID}]");
            return;
        }

        inventoryItemDatas[siblingIndex] = null;
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = null;
    }

    IEnumerator SpeedItem()
    {
        speedItemUsingCount++;
        GameManager.Instance.Character.speedUp = true;
        GameManager.Instance.Character.speed += 2f;
        GameManager.Instance.Character.GetComponent<SpriteRenderer>().color = Color.red;
        Debug.Log("1. Speed : " + GameManager.Instance.Character.speed);
        yield return new WaitForSeconds(10f);
        speedItemUsingCount--;
        GameManager.Instance.Character.speedUp = false;
        GameManager.Instance.Character.speed -= 2f;
        if (speedItemUsingCount == 0)
        {
            GameManager.Instance.Character.GetComponent<SpriteRenderer>().color = Color.white;
        }
        Debug.Log("2. Speed : " + GameManager.Instance.Character.speed);
    }

    IEnumerator PowerItem()
    {
        powerItemUsingCount++;
        GameManager.Instance.CharacterAttack.attackDamage += 2f;
        GameManager.Instance.Character.GetComponent<SpriteRenderer>().color = Color.green;
        Debug.Log("1. Character Power AttackDamage : " + GameManager.Instance.CharacterAttack.attackDamage);
        yield return new WaitForSeconds(10f);
        powerItemUsingCount--;
        GameManager.Instance.CharacterAttack.attackDamage -= 2f;
        if (powerItemUsingCount == 0)
        {
            GameManager.Instance.Character.GetComponent<SpriteRenderer>().color = Color.white;
        }
        Debug.Log("2. Character Power AttackDamage : " + GameManager.Instance.CharacterAttack.attackDamage);
    }
}
