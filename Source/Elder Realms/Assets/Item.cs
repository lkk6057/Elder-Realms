using System.Collections;
using System.Collections.Generic;
public class Item{
    public string itemname;
    public string itemdesc;
    public float itemvalue;
    public int itemspriteid;
    public Item(string itemname,string itemdesc, float itemvalue, int itemspriteid)
    {
        this.itemname = itemname;
        this.itemdesc = itemdesc;
        this.itemvalue = itemvalue;
        this.itemspriteid = itemspriteid;
    }
}
