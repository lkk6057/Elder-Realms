using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeroUiScript : MonoBehaviour {
    public GameObject Hero;
    public HeroScript HeroScript;
    public GameObject HealthBar;
    public GameObject HealthText;
    public GameObject ManaBar;
    public GameObject ManaText;
    public GameObject ExpBar;
    public Text HealthPotionText;
    public Text ManaPotionText;
    public Text GoldText;
    public Text ExpText;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        HeroScript = Hero.GetComponent<HeroScript>();
	}
	
	// Update is called once per frame
	void Update () {
        HealthBar.transform.localScale = new Vector3(HeroScript.Health/HeroScript.MaxHealth,1,1);
        ManaBar.transform.localScale = new Vector3(HeroScript.Mana/HeroScript.MaxMana,1,1);
        ExpBar.transform.localScale = new Vector3(HeroScript.Exp/HeroScript.ExpMax,1,1);
        HealthText.GetComponent<Text>().text = HeroScript.Health.ToString() + "/" + HeroScript.MaxHealth.ToString();
        GoldText.text = HeroScript.gold.ToString()+"G";
        HealthPotionText.text = "x"+HeroScript.HealthPotions.ToString();
        ManaPotionText.text = "x"+HeroScript.ManaPotions.ToString();
        ManaText.GetComponent<Text>().text = HeroScript.Mana.ToString() + "/" + HeroScript.MaxMana.ToString();
        ExpText.text = "Lvl " + HeroScript.Level.ToString() + " " + HeroScript.Exp.ToString() + "/" + HeroScript.ExpMax;

	}
}
