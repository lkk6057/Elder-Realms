using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OakdaleShop : MonoBehaviour {
    public List<Item> itemshop = new List<Item>();
    public BuyUiScript BuyUi;
    public ItemManager ItemManager;
	// Use this for initialization
	void Start () {
        BuyUi = GameObject.FindGameObjectWithTag("BuyUi").GetComponent<BuyUiScript>();
        ItemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        RegisterItems();
        RefreshShop();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void RegisterItems()
    {
        itemshop.Add(ItemManager.registereditems[5]);
        itemshop.Add(ItemManager.registereditems[6]);
    }
    public void RefreshShop()
    {
        BuyUi.items.Clear();
        foreach (Item item in itemshop)
        {
            BuyUi.items.Add(item);
        }
    }
}
