using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ZoneScript : MonoBehaviour {
    string[] Zones = {"","","","Oakdale","Oakdale Gates","Your house","Easter Egg","Snake Mire","Sky Garden","Shop","Floral Sanctum","Plains","Tutorial","Tutorial 2"};
    public GameObject Text;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        Text.GetComponent<Text>().color = new Color(1, 1, 1, 0);
    }
	
	// Update is called once per frame
	void Update () {
	}
    public void Announce(int index)
    {
        StartCoroutine(SetText(index));
    }
    IEnumerator SetText(int index)
    {
        Text.GetComponent<Text>().text = Zones[index];
        Text.GetComponent<Text>().color = new Color(1,1,1,1);
        yield return new WaitForSeconds(1);
        for (int i = 0; i<100; i++)
        {
            Text.GetComponent<Text>().color = new Color(1, 1, 1, Text.GetComponent<Text>().color.a-0.01f);
            yield return new WaitForSeconds(0.02f);
        }
    }
    
}
