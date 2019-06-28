using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class InventoryItem{
    public Item item;
    public float amount;
    public bool equipped;
    public InventoryItem(Item item, float amount,bool equipped)
    {
        this.item = item;
        this.amount = amount;
        this.equipped = equipped;
    }
}
