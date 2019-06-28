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
            else
            {
                Physics2D.IgnoreCollision(Hero.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }
        else
        {
            Physics2D.IgnoreCollision(Hero.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Ground").Length; i++)
        {
            if (GameObject.FindGameObjectsWithTag("Ground")[i].GetComponent<EliteSlimeScript>() != null)
            {
                if (GameObject.FindGameObjectsWithTag("Ground")[i].transform.position.y<center.transform.position.y)
                {
                    Physics2D.IgnoreCollision(GameObject.FindGameObjectsWithTag("Ground")[i].GetComponent<Collider2D>(), GetComponent<Collider2D>());
                }
                else
                {
                    Physics2D.IgnoreCollision(GameObject.FindGameObjectsWithTag("Ground")[i].GetComponent<Collider2D>(), GetComponent<Collider2D>(),false);
                }
                if (GameObject.FindGameObjectsWithTag("Ground")[i].transform.position.y>Hero.transform.position.y&& GameObject.FindGameObjectsWithTag("Ground")[i].GetComponent<EliteSlimeScript>().frenzy)
                {
                    Physics2D.IgnoreCollision(GameObject.FindGameObjectsWithTag("Ground")[i].GetComponent<Collider2D>(), GetComponent<Collider2D>());
                }
            }
        }
    }
}
