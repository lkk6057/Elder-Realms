using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyUiScript : MonoBehaviour
{
    public HeroScript player;
    public GameObject Inventory;
    public GameObject InventoryObject;
    public GameObject InventoryIcon;
    public GameObject InventoryBar;
    public ItemManager ItemManager;
    public List<Item> items = new List<Item>();
    public AudioClip CoinSound;
    public AudioSource audiosource;
    public float spacing;
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        ItemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        audiosource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RefreshBuyInventory()
    {
        CloseBuyInventory();
        DrawBuyInventory();
    }
    public void DrawBuyInventory()
    {
        for (int i = 0; i < items.Count; i++)
        {
            var invenbar = (GameObject)Instantiate(InventoryBar, Inventory.transform.position, transform.rotation);
            invenbar.transform.SetParent(Inventory.transform);
            invenbar.transform.localPosition = new Vector3(0, 320 - (i * spacing), 0);
            invenbar.GetComponent<InventoryBarScript>().itemindex = i;
            invenbar.GetComponent<InventoryBarScript>().isSell = false;
            var invenobject = (GameObject)Instantiate(InventoryObject, Inventory.transform.position, transform.rotation);
            invenobject.transform.SetParent(Inventory.transform);
            invenobject.transform.localPosition = new Vector3(20, 320 - (i * spacing), 0);
            invenobject.GetComponent<Text>().text = items[i].itemname + "    <color=yellow>" + items[i].itemvalue + "G</color> each";
            var invenicon = (GameObject)Instantiate(InventoryIcon, Inventory.transform.position, transform.rotation);
            invenicon.transform.SetParent(Inventory.transform);
            invenicon.GetComponent<Image>().sprite = ItemManager.sprites[items[i].itemspriteid];
            invenicon.transform.localPosition = new Vector3(-80, 325 - (i * spacing), 0);

        }
    }
    public void CloseBuyInventory()
    {
        foreach (GameObject invenobject in GameObject.FindGameObjectsWithTag("InventoryObject"))
        {
                Destroy(invenobject);
        }
    }
    public void Buy(int itemindex)
    {
        if (player.gold>=items[itemindex].itemvalue) {
            player.AddToInventory(items[itemindex], 1);
            player.gold -= items[itemindex].itemvalue;
            audiosource.PlayOneShot(CoinSound);
        }
    }
    public void BuyFive(int itemindex)
    {
        if (player.gold >= items[itemindex].itemvalue*5)
        {
            player.AddToInventory(items[itemindex], 5);
            player.gold -= items[itemindex].itemvalue*5;
            audiosource.PlayOneShot(CoinSound);
        }
    }
}
