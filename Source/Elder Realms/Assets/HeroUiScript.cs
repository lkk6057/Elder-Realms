using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeroUiScript : MonoBehaviour {
    public GameObject Hero;
    public GameObject HealthBar;
    public GameObject HealthText;
    public Text GoldText;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        HealthBar.transform.localScale = new Vector3(Hero.GetComponent<HeroScript>().Health/Hero.GetComponent<HeroScript>().MaxHealth,1,1);
        HealthText.GetComponent<Text>().text = Hero.GetComponent<HeroScript>().Health.ToString() + "/" + Hero.GetComponent<HeroScript>().MaxHealth.ToString();
        GoldText.text = Hero.GetComponent<HeroScript>().gold.ToString()+"G";
	}
}
