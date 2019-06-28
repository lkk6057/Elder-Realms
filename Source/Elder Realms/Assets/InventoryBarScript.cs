using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventoryBarScript : MonoBehaviour
{
    public SellUiScript SellUi;
    public BuyUiScript BuyUi;
    public InventoryUiScript InventoryUi;
    public int itemindex;
    public float TotalValue;
    public float EachValue;
    public bool over;
    public bool isSell;
    public bool isInventory;
	// Use this for initialization
	void Start () {
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
        SellUi = GameObject.FindGameObjectWithTag("SellUi").GetComponent<SellUiScript>();
        BuyUi = GameObject.FindGameObjectWithTag("BuyUi").GetComponent<BuyUiScript>();
        InventoryUi = GameObject.FindGameObjectWithTag("InventoryUi").GetComponent<InventoryUiScript>();
    }
	
	// Update is called once per frame
	void Update () {
        if (over)
        {
            if (!isInventory) {
                if (isSell && SellUi.player.inventory[itemindex].item.itemvalue>0) {
                    if (Input.GetKeyDown("c"))
                    {
                        if (SellUi.player.inventory[itemindex].amount > 0) {
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
                else
                {
                        if (Input.GetKeyDown("c"))
                        {
                            BuyUi.BuyFive(itemindex);
                        }
                        if (Input.GetMouseButtonDown(1))
                        {
                            BuyUi.Buy(itemindex);

                        }
                }
            }
            else
            {
                
                if (Input.GetMouseButtonDown(1))
                {
                    InventoryUi.ToggleEquip(itemindex);
                }
            }
        }
        else
        {

        }
	}
    public void SetColor(float color)
    {
        GetComponent<Image>().color = new Color(1, 1, 1, color);
        if (color==0)
        {
            over = false;
            InventoryUi.Description.text = "";
        }
        if (color==1)
        {
            over = true;
            if (isInventory)
            {
                InventoryUi.Description.text = SellUi.player.inventory[itemindex].item.itemdesc;
                if (SellUi.player.inventory[itemindex].equipped)
                {
                    InventoryUi.Description.text += "\n<color=green>Equipped</color>";
                }
            }
        }
    }
}
