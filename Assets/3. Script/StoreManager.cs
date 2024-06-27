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
        }
        else
        {
            Debug.LogError("Item ID not found: " + itemID);
        }
    }
}
