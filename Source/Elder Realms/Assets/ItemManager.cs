using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    public List<Item> registereditems = new List<Item>();
    public Sprite[] sprites;
    public GameObject itemprefab;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        RegisterItems();
    }
	
	// Update is called once per frame
	void Update () {

	}
    public void RegisterItems()
    {
        registereditems.Add(new Item("Slime", "Sticky green slime.", 1f, 0)); //0
        registereditems.Add(new Item("Snekzard Tail", "Dismembered Snekzard tail", 3f, 1)); //1
    }
    public void SpawnItem(int itemid,float amount,Vector3 pos)
    {
        var item = (GameObject)Instantiate(itemprefab,pos,transform.rotation);
        item.GetComponent<SpriteRenderer>().sprite = sprites[itemid];
        item.GetComponent<DroppedItem>().itemamount = amount;
        item.GetComponent<DroppedItem>().itemid = itemid;
    }
}
