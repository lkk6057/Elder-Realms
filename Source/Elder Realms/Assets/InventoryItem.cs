using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryItem{
    public Item item;
    public float amount;
    public InventoryItem(Item item, float amount)
    {
        this.item = item;
        this.amount = amount;
    }
}
