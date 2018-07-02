using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SellUiScript : MonoBehaviour
{
    public HeroScript player;
    public GameObject Inventory;
    public GameObject InventoryObject;
    public GameObject InventoryIcon;
    public GameObject InventoryBar;
    public ItemManager ItemManager;
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        ItemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void RefreshSellInventory()
    {
        CloseSellInventory();
        DrawSellInventory();
    }
    public void DrawSellInventory()
    {
        for (int i = 0; i < player.inventory.Count; i++)
        {
            var invenbar = (GameObject)Instantiate(InventoryBar, Inventory.transform.position, transform.rotation);
            invenbar.transform.SetParent(Inventory.transform);
            invenbar.transform.localPosition = new Vector3(0, 320 - (i * 20), 0);
            invenbar.GetComponent<InventoryBarScript>().TotalValue = player.inventory[i].amount * player.inventory[i].item.itemvalue;
            invenbar.GetComponent<InventoryBarScript>().EachValue = player.inventory[i].item.itemvalue;
            invenbar.GetComponent<InventoryBarScript>().itemindex = i;
            var invenobject = (GameObject)Instantiate(InventoryObject, Inventory.transform.position, transform.rotation);
            invenobject.transform.SetParent(Inventory.transform);
            invenobject.transform.localPosition = new Vector3(20, 320 - (i * 20), 0);
            invenobject.GetComponent<Text>().text = player.inventory[i].item.itemname + "(" + player.inventory[i].amount + ")    <color=yellow>" + player.inventory[i].item.itemvalue+"G</color> each"+"("+player.inventory[i].amount*player.inventory[i].item.itemvalue+"G)";
            var invenicon = (GameObject)Instantiate(InventoryIcon, Inventory.transform.position, transform.rotation);
            invenicon.transform.SetParent(Inventory.transform);
            invenicon.GetComponent<Image>().sprite = ItemManager.sprites[player.inventory[i].item.itemspriteid];
            invenicon.transform.localPosition = new Vector3(-80, 325 - (i * 20), 0);

        }
    }
    public void CloseSellInventory()
    {
        foreach (GameObject invenobject in GameObject.FindGameObjectsWithTag("InventoryObject"))
        {
            Destroy(invenobject);
        }
    }
    public void AddGold(float addgold)
    {
        player.gold += addgold;
    }
    public void SellOne(int index)
    {
        player.inventory[index].amount -= 1;
        if (player.inventory[index].amount<=0)
        {
            player.inventory.RemoveAt(index);
        }
    }
    public void SellAll(int index)
    {
        player.inventory.RemoveAt(index);
    }
}
