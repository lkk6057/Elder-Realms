using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUiScript : MonoBehaviour {
    public HeroScript player;
    public GameObject Inventory;
    public GameObject InventoryObject;
    public GameObject InventoryIcon;
    public GameObject InventoryBar;
    public ItemManager ItemManager;
    public Text Description;
    public float spacing;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        ItemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
            RefreshInventory();
            CloseInventory();
        GetComponent<Canvas>().enabled = true;
        GetComponent<Canvas>().enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
	}
    public void RefreshInventory()
    {
        CloseInventory();
        DrawInventory();
    }
    public void DrawInventory()
    {
        for (int i = 0; i<player.inventory.Count;i++)
        {
            var invenbar = (GameObject)Instantiate(InventoryBar, Inventory.transform.position, transform.rotation);
            invenbar.transform.SetParent(Inventory.transform);
            invenbar.transform.localPosition = new Vector3(0, 300 - (i * spacing), 0);
            invenbar.GetComponent<InventoryBarScript>().itemindex = i;
            invenbar.GetComponent<InventoryBarScript>().isSell = false;
            invenbar.GetComponent<InventoryBarScript>().isInventory = true;
            var invenobject = (GameObject)Instantiate(InventoryObject,Inventory.transform.position,transform.rotation);
            invenobject.transform.SetParent(Inventory.transform);
            invenobject.transform.localPosition = new Vector3(20,300-(i*spacing),0);
            if (player.inventory[i].equipped)
            {
                invenobject.GetComponent<Text>().text = "<color=green>" + player.inventory[i].item.itemname + "</color>(" + player.inventory[i].amount + ") ";
            }
            else
            {
                invenobject.GetComponent<Text>().text = player.inventory[i].item.itemname + "(" + player.inventory[i].amount + ") ";
            }
            if (player.inventory[i].item.itemvalue>0)
            {
                invenobject.GetComponent<Text>().text += "<color=yellow>" + player.inventory[i].item.itemvalue + "G</color> each";
            }
            else
            {
                invenobject.GetComponent<Text>().text += "<color=red>Unsellable</color>";
            }
            var invenicon = (GameObject)Instantiate(InventoryIcon,Inventory.transform.position,transform.rotation);
            invenicon.transform.SetParent(Inventory.transform);
            invenicon.GetComponent<Image>().sprite = ItemManager.sprites[player.inventory[i].item.itemspriteid];
            invenicon.transform.localPosition = new Vector3(-80,300-(i*spacing),0);

        }
    }
    public void CloseInventory()
    {
        foreach (GameObject invenobject in GameObject.FindGameObjectsWithTag("InventoryObject"))
        {
            Destroy(invenobject);
        }
    }
    public void Reposition()
    {
        transform.position += new Vector3(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"), 0);
    }
    public void ToggleEquip(int itemindex)
    {
        if (player.inventory[itemindex].item.itemtype==1) {
            foreach (InventoryItem item in player.inventory)
            {
                if (item.item.itemtype == 1&&item!= player.inventory[itemindex])
                {
                    item.equipped = false;
                }
            }
            player.inventory[itemindex].equipped = !player.inventory[itemindex].equipped;
            }
        RefreshInventory();
    }
}
