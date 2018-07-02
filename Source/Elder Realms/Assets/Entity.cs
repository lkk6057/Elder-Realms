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
    public float Health;
    public float MaxHealth;
    public bool Dead;
	// Use this for initialization
	void Start () {
        itemmanager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
	}
	
	// Update is called once per frame
	void Update () {
        BackBar.transform.localScale = new Vector3(MaxHealth / 100, 1, 1);
        EntityUi.transform.localScale = transform.localScale;
        HealthBar.transform.localScale = new Vector3(Health/MaxHealth,1,1);
        EntityText.GetComponent<Text>().text = EntityName + " Lvl " + EntityLevel;
        if (Health <= 0&&!Dead)
        {
            Die();
        }
    }
    public void Hit(float Damage)
    {
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
        Dead = true;
    }
}
