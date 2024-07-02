using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPackManager : MonoBehaviour
{
    public static BackPackManager Instance;
    public GameObject backPack_UI;
    public Text coinText;

    public Image[] itemImages;
    InventoryItemData[] inventoryItemDatas;

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
}
