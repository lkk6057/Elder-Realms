using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DroppedItem : MonoBehaviour {
    public int itemid;
    public float itemamount;
    public GameObject player;
    public ItemManager itemmanager;
    public Text itemtext;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Hero");
        itemmanager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(),player.GetComponent<Collider2D>());
        SetText();
        foreach (GameObject gobject in GameObject.FindGameObjectsWithTag("Ground"))
        {
            if (gobject.GetComponent<EliteSlimeScript>()!=null)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(),gobject.GetComponent<Collider2D>());
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position,player.transform.position)<0.72f)
        {
            PickUp();
        }
	}
    public void SetText()
    {
        if (itemamount == 1)
        {
            itemtext.text = itemmanager.registereditems[itemid].itemname;
        }
        else
        {
            itemtext.text = itemmanager.registereditems[itemid].itemname+" x"+itemamount;
        }
    }
    public void PickUp()
    {
        player.GetComponent<HeroScript>().PickUpItem(itemmanager.registereditems[itemid],itemamount);
        Destroy(gameObject);
    }
}
