using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueScript : MonoBehaviour {
    public GameObject Hero;
    public GameObject Snek;
    public bool CanHit = true;
	// Use this for initialization
	void Start () {
        Hero = GameObject.FindGameObjectWithTag("Hero");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Hero")
        {
            Hero.GetComponent<HeroScript>().Hit(Random.Range(18, 24));
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
