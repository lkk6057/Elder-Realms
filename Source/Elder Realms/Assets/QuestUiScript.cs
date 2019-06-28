using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestUiScript : MonoBehaviour {
    public float spacing;
    public GameObject QuestText;
    public List<Quest> Quests =  new List<Quest>();
    public GameObject QuestList;
    public ItemManager itemmanager;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        itemmanager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        Quests.Add(new Quest(1, 1, 10, 2));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void RefreshQuests()
    {
        CloseQuests();
        DrawQuests();
    }
    public void DrawQuests()
    {
        for (int i = 0; i<Quests.Count;i++)
        {
            var Text = (GameObject)Instantiate(QuestText,transform.position,transform.rotation);
            Text.transform.SetParent(QuestList.transform);
            Text.transform.localPosition = new Vector3(0,300-(i*spacing),0);
            string plural="";
            if (Quests[i].objectiveamount>1)
            {
                plural = "s";
            }
            if (Quests[i].type ==0)
            {
                Text.GetComponent<Text>().text = "Obtain "+Quests[i].objectiveamount+" "+itemmanager.registereditems[Quests[i].objectiveid].itemname+plural+" ("+Quests[i].progress+"/"+Quests[i].objectiveamount+")";
            }
            if (Quests[i].type == 1)
            {
                Text.GetComponent<Text>().text = "Kill " + Quests[i].objectiveamount + " " + itemmanager.entities[Quests[i].objectiveid].GetComponent<Entity>().EntityName + plural + " (" + Quests[i].progress + "/" + Quests[i].objectiveamount + ")";
            }
            if (Quests[i].progress>=Quests[i].objectiveamount)
            {
                Text.GetComponent<Text>().text = "<color=green>" + Text.GetComponent<Text>().text + "</color>";
            }
        }
    }
    public void CloseQuests()
    {
        foreach (GameObject quest in GameObject.FindGameObjectsWithTag("Quest"))
        {
            Destroy(quest);
        }
    }
}
