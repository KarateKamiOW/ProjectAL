using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShopUI : MonoBehaviour
{
    [Header("Layout Settings")]
    [SerializeField] float itemSpace = .5f;
    float itemHeight;


    [Header("UI Elements")]
    [SerializeField] Transform shopMenu;
    [SerializeField] Transform itemsContainer;      //shopItemsContainer
    [SerializeField] GameObject itemPrefab;
    [Space(20f)]
    [SerializeField] InventoryObject npcInventory;


    [Header("Shop Events")]
    [SerializeField] GameObject shopUI;
    [SerializeField] Button openShopButton;
    [SerializeField] Button closeShopButton;
    [SerializeField] List<Button> buyItemButtons;

    private void Start()
    {
        AddShopEvents();

        GenerateShopItemsUI();
    }

    void AddShopEvents()
    {
        openShopButton.onClick.RemoveAllListeners();
        openShopButton.onClick.AddListener(OpenShop);

        closeShopButton.onClick.RemoveAllListeners();
        closeShopButton.onClick.AddListener(CloseShop);
    }

    void GenerateShopItemsUI() 
    {
        itemHeight = itemsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        Destroy(itemsContainer.GetChild(0).gameObject);
        itemsContainer.DetachChildren();

        for (int i = 0; i < npcInventory.container.Count; i++) 
        {
            InventorySlot invContainer = npcInventory.container[i];
            ShopPanelUI uiItem = Instantiate(itemPrefab, itemsContainer).GetComponent<ShopPanelUI>();

            uiItem.SetItemPosition(Vector2.down * i * (itemHeight + itemSpace));
            
            //Sets the item name in heirarchy
            //uiItem.gameObject.name = "Item" + i + "-" + invContainer.item.name;

            uiItem.SetItemName(invContainer.item.name);
            uiItem.SetItemImage(invContainer.item.ItemSprite);
            uiItem.SetItemPrice(invContainer.item.ShopKeeperSellPrice);

            itemsContainer.GetComponent<RectTransform>().sizeDelta = Vector2.up * (itemHeight + itemSpace) * npcInventory.container.Count;

            uiItem.OnItemPurchase(i, OnItemPurchase);
            //buyItemButtons[i].onClick.RemoveAllListeners();
            //buyItemButtons[i].onClick.AddListener(CloseShop);

        }
    }

    void OpenShop() 
    {
        shopUI.SetActive(true);
    }

    void CloseShop() 
    {
        shopUI.SetActive(false);
    }

    void OnItemPurchase(int index) 
    {
        InventorySlot invItem = npcInventory.container[index];
        if (CoinsAndScore.instance.coins >= invItem.item.ShopKeeperSellPrice)
        {
            CoinsAndScore.instance.ChangeCoinAmount(-invItem.item.ShopKeeperSellPrice);
            AlienBase.instance.inventory.AddItem(invItem.item, 1);
            Debug.Log(CoinsAndScore.instance.coins);
        }
        else 
        {
            Debug.Log("Not Enough Coins!");
        }
    }
}
