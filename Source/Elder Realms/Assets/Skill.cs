using System;
using UnityEngine;
[Serializable]
public class Skill {
    public string SkillName;
    public string SkillDescription;
    public string SkillKey;
    public float ManaCost;
    public float Cooldown;
    public int SkillId;
    public int Soundid;
    public float SkillLevel;
    public float SkillMax;
    public bool Unlocked;
    public float Cost;
    public Skill(string skillname, string skilldescription, string skillkey, float manacost, float cooldown,int soundid, int skillid,float skilllevel, float skillmax,bool unlocked,float cost)
    {
        SkillName = skillname;
        SkillDescription = skilldescription;
        SkillKey = skillkey;
        ManaCost = manacost;
        Cooldown = cooldown;
        Soundid = soundid;
        SkillId = skillid;
        SkillLevel = skilllevel;
        SkillMax = skillmax;
        Unlocked = unlocked;
        Cost = cost;
    }
}
