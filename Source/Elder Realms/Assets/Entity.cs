using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Entity : MonoBehaviour {
    public Vector3 UiOffset;
    public string EntityName;
    public int EntityLevel;
    public int[] droplist;
    public float[] amountlist;
    public float[] dropchance;
    public GameObject EntityUi;
    public GameObject HealthBar;
    public GameObject BackBar;
    public GameObject EntityText;
    public ItemManager itemmanager;
    public QuestUiScript QuestUi;
    public float Health;
    public float MaxHealth;
    public float BarScale;
    public bool Dead;
    public HeroScript Hero;
    public float ExpReward;
    public int EntityId;
	// Use this for initialization
	void Start () {
        itemmanager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        Hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<HeroScript>();
        QuestUi = GameObject.FindGameObjectWithTag("QuestUi").GetComponent<QuestUiScript>();
        EntityId = itemmanager.entities.FindIndex(x=>x.GetComponent<Entity>().EntityName==EntityName);
	}
	
	// Update is called once per frame
	void Update () {
        BackBar.transform.localScale = new Vector3((MaxHealth / 100)*(1+BarScale), 1, 1);
        EntityUi.transform.localScale = transform.localScale;
        HealthBar.transform.localScale = new Vector3((Health/MaxHealth),1,1);
        EntityText.GetComponent<Text>().text = EntityName + " Lvl " + EntityLevel;
        if (Health <= 0&&!Dead)
        {
            Die();
        }
    }
    public void Hit(float Damage)
    {
        if (GetComponent<FlowerBossScript>() != null)
        {
            GetComponent<FlowerBossScript>().Activate();
        }
        if (Health>=Damage)
        {
            Health -= Damage;
            return;
        }
        if (Health < Damage)
        {
            Health = 0;
            return;
        }
    }
    public void Die()
    {
        if (GetComponent<SlimeScript>()!=null)
        {
            GetComponent<SlimeScript>().Die();
        }
        if (GetComponent<SnekzardScript>() != null)
        {
            GetComponent<SnekzardScript>().Die();
        }
        if (GetComponent<FlowerBossScript>()!=null)
        {
            GetComponent<FlowerBossScript>().Die();
        }
        if (GetComponent<EliteSlimeScript>() != null)
        {
            GetComponent<EliteSlimeScript>().Die();
        }
        for (int i = 0; i<droplist.Length;i++)
        {
            if (dropchance[i]>0)
            {
                if (Random.Range(0f,100f)<=dropchance[i])
                {
                    itemmanager.SpawnItem(droplist[i],Mathf.Round(Random.Range(1,amountlist[i])),transform.position);
                }
            }
        }
        Hero.Exp += ExpReward;
        Dead = true;
        List<Quest> quests = QuestUi.Quests.FindAll(x => x.objectiveid == 1 && x.objectiveid == EntityId);
        foreach (Quest quest in quests)
        {
            quest.progress += 1;
            if (QuestUi.gameObject.GetComponent<Canvas>().enabled)
            {
                QuestUi.RefreshQuests();
            }
        }
    }
}
