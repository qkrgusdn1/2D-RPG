using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public InventoryItemData[] items;
    public GameObject purchase_UI;
    public Image itemImage;
    public Text ItemNameText;
    public Text ItemCoinText;
    public Text ItemExplaneText;

    string selectedItemID;

    private Dictionary<string, InventoryItemData> itemDictionary;

    void Start()
    {
        itemDictionary = new Dictionary<string, InventoryItemData>();
        foreach(InventoryItemData item in items)
        {
            itemDictionary[item.itemID] = item;
        }
    }

    public void SelectItem(string itemID)
    {
        if(itemDictionary.TryGetValue(itemID, out InventoryItemData selectedItem))
        {
            purchase_UI.SetActive(true);
            itemImage.sprite = selectedItem.itemImage;
            ItemNameText.text = selectedItem.itemName;
            ItemCoinText.text = $"({selectedItem.itemPrice:N0} Point)";
            ItemExplaneText.text = selectedItem.itemDescription;

            selectedItemID = itemID;
        }
        else
        {
            Debug.LogError("Item ID not found: " + itemID);
        }
    }

    public void Purchase()
    {
        InventoryItemData selectedItem = itemDictionary[selectedItemID];
        if(GameManager.Instance.playerStat.coin >= selectedItem.itemPrice) 
        {
            if (BackPackManager.Instance.AddItem(selectedItem))
            {
                GameManager.Instance.playerStat.coin -= selectedItem.itemPrice;
                PopupMsgManager.Instance.ShowPopupMessage("구매 성공");
            }
            else
            {
                PopupMsgManager.Instance.ShowPopupMessage("BackPack에 빈 공간이 없습니다.");
            }
        }
        else
        {
            PopupMsgManager.Instance.ShowPopupMessage($"잔액이 부족합니다. 잔액 : {GameManager.Instance.playerStat.coin}");
        }


    }
}
