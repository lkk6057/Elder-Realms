using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillScript : MonoBehaviour {
    public Image image;
    public bool over;
    public int SkillId;
    public Text description;
    public SkillUIScript skillmanager;
    public Image bars;
	// Use this for initialization
	void Start () {
        description = GameObject.FindGameObjectWithTag("SkillDescription").GetComponent<Text>();
        skillmanager = GameObject.FindGameObjectWithTag("SkillUi").GetComponent<SkillUIScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1)&&over&&skillmanager.skills[SkillId].Unlocked&&skillmanager.gameObject.GetComponent<Canvas>().enabled)
        {
            skillmanager.UpgradeSkill(SkillId);
        }
	}
    public void SetColor(int colorid)
    {
        if (colorid==0)
        {
            over = false;
            image.color = new Color(1,1,1,1);
            description.text = "";
        }
        if (colorid ==1)
        {
            over = true;
            image.color = new Color(0.8f,0.8f,0.8f,1);
            description.text = skillmanager.skills[SkillId].SkillDescription;
        }
    }
}
