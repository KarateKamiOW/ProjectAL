using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ShopPanelUI : MonoBehaviour
{
    [SerializeField] Color itemNotSelectedColor;
    [SerializeField] Color itemSelectedColor;

    [Space(20f)]
    [SerializeField] Image itemImage;
    [SerializeField] Text itemName;
    [SerializeField] Text itemPrice;
    [SerializeField] Button purchaseButton;

    [Space(20f)]
    [SerializeField] Button itemButton;
    [SerializeField] Image itemPanel;
    [SerializeField] Outline itemOutline;

    //-------------------------------------------------------------------------------------------

    public void SetItemPosition(Vector2 pos) 
    {
        GetComponent<RectTransform>().anchoredPosition += pos;
    }

    public void SetItemImage(Sprite sprite) 
    {
        itemImage.sprite = sprite;
    }

    public void SetItemName(string name) 
    {
        itemName.text = name;
    }

    public void SetItemPrice(int price) 
    {
        itemPrice.text = price.ToString(); 
    }

    public void ItemOutOfStock() 
    {
        purchaseButton.gameObject.SetActive(false);
    }

    public void SetItemAsPurchased() 
    {
        purchaseButton.gameObject.SetActive(false);
        itemButton.interactable = true;

        itemPanel.color = itemNotSelectedColor;
    }

    public void SelectItem() 
    {
        itemOutline.enabled = true;
        itemPanel.color = itemSelectedColor;
        itemButton.interactable = false;
    }
    public void DeSelectItem()
    {
        itemOutline.enabled = false;
        itemPanel.color = itemNotSelectedColor;
        itemButton.interactable = true;
    }

    public void OnItemPurchase(int itemIndex, UnityAction<int> action) 
    {
        purchaseButton.onClick.RemoveAllListeners();
        purchaseButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }

    public void OnItemSelect(int itemIndex, UnityAction<int> action) 
    {
        itemButton.interactable = true;
        purchaseButton.onClick.RemoveAllListeners();
        itemButton.onClick.AddListener(() => action.Invoke(itemIndex));

    }
}
