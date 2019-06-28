using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Save
{
    public List<InventoryItem> inventory;
    public List<Skill> skills;
    public float Gold;
    public float MaxHealth;
    public float MaxMana;
    public int Level;
    public float Exp;
    public float SkillPoints;
    public int Scene;
    public bool TutorialDone;
    public float PlayerX;
    public float PlayerY;
}