using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour {
    public GameObject source;
    public GameObject Hero;
    public AudioClip SlimeDeath;
    public float DamageMin;
    public float DamageMax;
    public bool CanHit = true;
    public float HitCool;
	// Use this for initialization
	void Start () {
        StartCoroutine(Pace());
        source = GameObject.FindGameObjectWithTag("Audio");
        Hero = GameObject.FindGameObjectWithTag("Hero");
        if (Random.Range(0, 2) == 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        }
        if (GameObject.FindGameObjectWithTag("EliteSpawner")!=null) {
            if (GameObject.FindGameObjectWithTag("EliteSpawner").GetComponent<SpawnerScript>().EntityAssigned!=null) {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("EliteSpawner").GetComponent<SpawnerScript>().EntityAssigned.GetComponent<Collider2D>());
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator Pace()
    {
        if (Random.Range(0,2)==0)
        {
            transform.localScale = new Vector3(-transform.localScale.x,1,1);
        }
        StartCoroutine(Step());
        yield return new WaitForSeconds(Random.Range(1f,3f));
        StartCoroutine(Pace());
    }
    IEnumerator Step()
    {
        for (int i = 0; i<Random.Range(50,100); i++)
        {
            transform.position += new Vector3(0.01f* transform.localScale.x, 0,0);
            yield return new WaitForSeconds(0.01f);
        }
    }
    public void Die()
    {
        source.GetComponent<AudioSource>().PlayOneShot(SlimeDeath);
        Destroy(gameObject);
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if (CanHit&&coll.transform.tag=="Hero")
        {
            StartCoroutine(HitPlayer((int)Random.Range(DamageMin,DamageMax)));
        }
    }
    IEnumerator HitPlayer(float damage)
    {
        CanHit = false;
        Hero.GetComponent<HeroScript>().Hit(damage);
        yield return new WaitForSeconds(HitCool);
        CanHit = true;
    }
}
