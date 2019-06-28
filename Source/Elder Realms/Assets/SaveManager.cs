using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveManager : MonoBehaviour {
    FileStream file;
    BinaryFormatter bf = new BinaryFormatter();
    public Save save;
    string path;
    public HeroScript hero;
    public SkillUIScript skillui;
    public SceneLoader loader;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        save = new Save();
        path = Directory.GetCurrentDirectory() + "/The Elder Realms_Data/game.save";
    }
	
	// Update is called once per frame
	void Update () {
        
	}
    public void Save()
    {
            file = File.Create(path);
        hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<HeroScript>();
        if (hero != null)
        {
            save.inventory = hero.inventory;
            save.Gold = hero.gold;
            save.MaxHealth = hero.MaxHealth;
            save.MaxMana = hero.MaxMana;
            save.Level = hero.Level;
            save.Exp = hero.Exp;
            save.SkillPoints = hero.SkillPoints;
            save.Scene = loader.currscene;
            save.PlayerX = hero.gameObject.transform.position.x;
            save.PlayerY = hero.gameObject.transform.position.y;
        }
        skillui = GameObject.FindGameObjectWithTag("SkillUi").GetComponent<SkillUIScript>();
        if (skillui != null)
        {
            save.skills = skillui.skills;
        }
        bf.Serialize(file,save);
        file.Close();
    }
    public bool GetSave()
    {
        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Load()
    {
        if (!File.Exists(path))
        {
            file = File.Create(path);
            save.Scene = 12;
            save.PlayerX = -6;
            save.PlayerY = -2.36f;
            bf.Serialize(file,save);
        }
        else
        {
            file = File.Open(path, FileMode.Open);
            save = (Save)bf.Deserialize(file);
            hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<HeroScript>();
            if (hero != null)
            {
                hero.inventory = save.inventory;
                hero.gold = save.Gold;
                hero.MaxHealth = save.MaxHealth;
                hero.MaxMana = save.MaxMana;
                hero.Level = save.Level;
                hero.Exp = save.Exp;
                hero.SkillPoints = save.SkillPoints;
                loader.SwitchScene(save.Scene);
                hero.gameObject.transform.position = new Vector3(save.PlayerX,save.PlayerY,0);
            }
            skillui = GameObject.FindGameObjectWithTag("SkillUi").GetComponent<SkillUIScript>();
            if (skillui != null)
            {
                skillui.skills = save.skills;
                skillui.UpdateSkillUi();
            }
        }

        file.Close();
    }
    void OnApplicationQuit()
    {
        Save();
        file.Close();
    }
}
