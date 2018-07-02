using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUiScript : MonoBehaviour {
    public HeroScript player;
    public GameObject Inventory;
    public GameObject InventoryObject;
    public GameObject InventoryIcon;
    public ItemManager ItemManager;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        ItemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
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
            var invenobject = (GameObject)Instantiate(InventoryObject,Inventory.transform.position,transform.rotation);
            invenobject.transform.SetParent(Inventory.transform);
            invenobject.transform.localPosition = new Vector3(20,320-(i*20),0);
            invenobject.GetComponent<Text>().text = player.inventory[i].item.itemname + "(" + player.inventory[i].amount + ")    " + player.inventory[i].item.itemvalue;
            var invenicon = (GameObject)Instantiate(InventoryIcon,Inventory.transform.position,transform.rotation);
            invenicon.transform.SetParent(Inventory.transform);
            invenicon.GetComponent<Image>().sprite = ItemManager.sprites[player.inventory[i].item.itemspriteid];
            invenicon.transform.localPosition = new Vector3(-80,325-(i*20),0);

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
}
