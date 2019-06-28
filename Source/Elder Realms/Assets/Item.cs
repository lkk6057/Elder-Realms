using System;
using System.Collections;
using System.Collections.Generic;
[Serializable]
public class Item{
    public string itemname;
    public string itemdesc;
    public float itemvalue;
    public int itemspriteid;
    public int itemtype;
    public Item(string itemname,string itemdesc, float itemvalue, int itemspriteid,int itemtype) //0 normal 1 weapon 2 armor
    {
        this.itemname = itemname;
        this.itemdesc = itemdesc;
        this.itemvalue = itemvalue;
        this.itemspriteid = itemspriteid;
        this.itemtype = itemtype;
    }
}
