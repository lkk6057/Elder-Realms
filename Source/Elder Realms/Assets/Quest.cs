using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest {
    public int type; //0 item 1 kill
    public int objectiveid;
    public int objectiveamount;
    public int progress;
    public Quest(int type,int objectiveid, int objectiveamount,int progress)
    {
        this.type = type;
        this.objectiveid = objectiveid;
        this.objectiveamount = objectiveamount;
        this.progress = progress;
    }
}
