using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> container = new List<InventorySlot>();

    public void AddItem(ItemObject item, int amount) 
    {
        bool hasItem = false;
        for (int i = 0; i < container.Count; i++) 
        {
            if (container[i].item == item) 
            {
                container[i].AddAmount(amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem) 
        {
            container.Add(new InventorySlot(item, amount));
        }
    }
}


[System.Serializable]
public class InventorySlot 
{
    public ItemObject item;
    public int amount;
    public InventorySlot(ItemObject invItem, int invAmt) 
    {
        item = invItem;
        amount = invAmt;
    }

    public void AddAmount(int value) 
    {
        amount += value; 
    }
}
