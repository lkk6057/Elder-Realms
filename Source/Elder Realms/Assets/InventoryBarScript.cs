using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventoryBarScript : MonoBehaviour
{
    public SellUiScript SellUi;
    public int itemindex;
    public float TotalValue;
    public float EachValue;
    public bool over;
	// Use this for initialization
	void Start () {
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
        SellUi = GameObject.FindGameObjectWithTag("SellUi").GetComponent<SellUiScript>();
    }
	
	// Update is called once per frame
	void Update () {
        if (over)
        {
            if (Input.GetKeyDown("c"))
            {
                if (SellUi.player.inventory[itemindex].amount>0) {
                    SellUi.AddGold(EachValue);
                    SellUi.SellOne(itemindex);
                    SellUi.RefreshSellInventory();
                }
            }
            if (Input.GetMouseButtonDown(1))
            { 
                    SellUi.AddGold(TotalValue);
                    SellUi.SellAll(itemindex);
                    SellUi.RefreshSellInventory();
                
            }
        }
	}
    public void SetColor(float color)
    {
        GetComponent<Image>().color = new Color(1, 1, 1, color);
        if (color==0)
        {
            over = false;
        }
        if (color==1)
        {
            over = true;
        }
    }
}
