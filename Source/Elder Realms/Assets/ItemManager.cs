using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    public List<Item> registereditems = new List<Item>();
    public Sprite[] sprites;
    public GameObject[] registeredentities;
    public List<GameObject> entities = new List<GameObject>();
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
        registereditems.Add(new Item("Slime", "Sticky green slime. Tastes a little sour.", 1f, 0,0)); //0
        registereditems.Add(new Item("Snekzard Tail", "Dismembered Snekzard tail, good for stew.", 3f, 1,0)); //1
        registereditems.Add(new Item("Sword of Divine Acclaim", "A legendary sword forged by the Elder Spirits. 375-450 damage.", 4500f, 2, 1)); //2
        registereditems.Add(new Item("Dull Blade", "A common sword carried by many adventurers. 40-70 damage.", -1f, 3, 1)); //3
        registereditems.Add(new Item("Gold Coin", "Shiny!", 1f, 4, 0)); //4
        registereditems.Add(new Item("Health Potion", "Restores 20 HP. Effect diminishes when used too quickly. F to consume.", 5f, 5, 0)); //5
        registereditems.Add(new Item("Mana Potion", "Restores 20 MP. V to consume.", 5f, 6, 0)); //6
        foreach (GameObject entity in registeredentities)
        {
            entities.Add(entity);
        }
    }
    public void SpawnItem(int itemid,float amount,Vector3 pos)
    {
        var item = (GameObject)Instantiate(itemprefab,pos,transform.rotation);
        item.GetComponent<SpriteRenderer>().sprite = sprites[itemid];
        item.GetComponent<DroppedItem>().itemamount = amount;
        item.GetComponent<DroppedItem>().itemid = itemid;
    }
}
