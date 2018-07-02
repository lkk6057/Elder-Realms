using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {
    public GameObject Hero;
    public GameObject center;
	// Use this for initialization
	void Start () {
        Hero = GameObject.FindGameObjectWithTag("Hero");
    }
	
	// Update is called once per frame
	void Update () {
        if (Hero.GetComponent<HeroScript>().Feet.transform.position.y>center.transform.position.y)
        {
            if (!Hero.GetComponent<HeroScript>().DownJumping)
            {
                Physics2D.IgnoreCollision(Hero.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
            }
        }
        else
        {
            Physics2D.IgnoreCollision(Hero.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
	}
}
