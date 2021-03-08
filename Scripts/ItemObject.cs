using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Default,
    Consumable,
    Weapon,
    Misc
}
public abstract class ItemObject : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] Sprite itemSprite;
    [SerializeField] GameObject prefab;
    [SerializeField] ItemType type;
    [SerializeField] int sellPrice;
    [SerializeField] int shopKeeperSellPrice;

    [TextArea(15, 20)]
    public string description;


    public string Name 
    { get { return name; } }
    public Sprite ItemSprite 
    { get { return itemSprite; } }

    public int SellPrice 
    { get { return sellPrice; } }

    public int ShopKeeperSellPrice 
    { get { return shopKeeperSellPrice; } }
}

