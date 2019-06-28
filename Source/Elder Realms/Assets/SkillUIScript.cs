using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillUIScript : MonoBehaviour {
    public List<Skill> skills = new List<Skill>();
    public Sprite[] skillbarsprites;
    public Text skillpoints;
    public HeroScript hero;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        RegisterSkills();
        UpdateSkillUi();
	}
	
	// Update is called once per frame
	void Update () {
        skillpoints.text = "Skill Points: "+hero.SkillPoints.ToString();

    }
    public void RegisterSkills()
    {
        skills.Add(new Skill("Dodge","Evade enemy attacks. 2 SP to unlock. 20 MP Cost. Left ctrl.","left ctrl",20,1f,0,0,0,1,true,2)); //0
        skills.Add(new Skill("Firebreath", "Unleash fiery breath onto your enemies. 3 SP to unlock. 20 MP Cost per second. Up Arrrow", "up", 1, 0, 0, 1, 0, 3, true, 3)); //1
    }
    public void UpgradeSkill(int id)
    {
        if (skills[id].SkillLevel<skills[id].SkillMax&&hero.SkillPoints>=skills[id].Cost)
        {
            skills[id].SkillLevel += 1;
            hero.SkillPoints -= skills[id].Cost;
            UpdateSkillUi();
        }
    }
    public void UpdateSkillUi()
    {
        foreach (GameObject skill in GameObject.FindGameObjectsWithTag("Skill"))
        {
            int id = skill.GetComponent<SkillScript>().SkillId;
            Image image = skill.GetComponent<Image>();
            Image bars = skill.GetComponent<SkillScript>().bars;
            if (skills[id].Unlocked)
            {
                bars.enabled = false;
            }
            else
            {
                bars.enabled = true;
            }
            if (skills[id].SkillLevel==0&& skills[id].SkillMax == 1)
            {
                image.sprite = skillbarsprites[0];
            }
            if (skills[id].SkillLevel == 0 && skills[id].SkillMax == 2)
            {
                image.sprite = skillbarsprites[1];
            }
            if (skills[id].SkillLevel == 0 && skills[id].SkillMax == 3)
            {
                image.sprite = skillbarsprites[2];
            }
            if (skills[id].SkillLevel == 1 && skills[id].SkillMax == 1)
            {
                image.sprite = skillbarsprites[3];
            }
            if (skills[id].SkillLevel == 1 && skills[id].SkillMax == 2)
            {
                image.sprite = skillbarsprites[4];
            }
            if (skills[id].SkillLevel == 1 && skills[id].SkillMax == 3)
            {
                image.sprite = skillbarsprites[5];
            }
            if (skills[id].SkillLevel == 2 && skills[id].SkillMax == 2)
            {
                image.sprite = skillbarsprites[6];
            }
            if (skills[id].SkillLevel == 2 && skills[id].SkillMax == 3)
            {
                image.sprite = skillbarsprites[7];
            }
            if (skills[id].SkillLevel == 3 && skills[id].SkillMax == 3)
            {
                image.sprite = skillbarsprites[8];
            }
        }
    }
}
