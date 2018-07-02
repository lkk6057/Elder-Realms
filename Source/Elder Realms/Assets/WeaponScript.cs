using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {
    public bool Swinging = false;
    public GameObject Hero;
    public bool CanSwing = true;
    public AudioClip[] sounds;
    public AudioSource source;
    public int WeaponId;
	// Use this for initialization
	void Start () {
        WeaponId = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Swinging == false)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<PolygonCollider2D>().enabled = true;
        }
	}
    public void Swing()
    {
        Swinging = true;
        CanSwing = false;
        transform.eulerAngles = new Vector3(0,0,30*-Hero.GetComponent<HeroScript>().dir);
        StartCoroutine(SwingAnim());
    }
    IEnumerator SwingAnim()
    {
        if (WeaponId == 0)
        {
            source.clip = sounds[Random.Range(0, 2)];
            source.Play();
        }
        for (int i = 0; i<12; i++)
        {
            transform.eulerAngles += new Vector3(0,0,9*Hero.GetComponent<HeroScript>().dir);
            yield return new WaitForSeconds(0.01f);
            
        }
        Swinging = false;
        yield return new WaitForSeconds(0.4f);
        CanSwing = true;

    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Entity>()!=null)
        {
            coll.GetComponent<Entity>().Hit(50);
            if (coll.GetComponent<SlimeScript>()!=null)
            {
                coll.GetComponent<Rigidbody2D>().AddForce(new Vector3(-Hero.GetComponent<HeroScript>().dir,2,0),ForceMode2D.Impulse);
            }
            if (coll.GetComponent<SnekzardScript>() != null)
            {
                coll.GetComponent<Rigidbody2D>().AddForce(new Vector3(-Hero.GetComponent<HeroScript>().dir*1.5f, 2, 0), ForceMode2D.Impulse);
            }
        }
    }
}
