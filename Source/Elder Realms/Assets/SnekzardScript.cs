using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekzardScript : MonoBehaviour {
    public GameObject source;
    public GameObject Hero;
    public int State = 0;
    public bool Walking;
    public bool Attacking = false;
    public bool AttackCool = false;
    public bool TurnCool = false;
    public GameObject Tongue;
    public Sprite[] sprites;
    public AudioClip death;
    public AudioSource audiosource;
	// Use this for initialization
	void Start () {
        Tongue.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(WalkFrames());
        StartCoroutine(Pace());
        source = GameObject.FindGameObjectWithTag("Audio");
        Hero = GameObject.FindGameObjectWithTag("Hero");
        if (Random.Range(0, 2) == 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Walking && !Attacking)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        if (Vector3.Distance(transform.position, Hero.transform.position) > 5f)
        {
            State = 0;
        }
        if (Vector3.Distance(transform.position, Hero.transform.position) < 2.5f)
        {
            State = 1;

            if (State == 1 && !Attacking)
            {
                if (Vector3.Distance(transform.position, Hero.transform.position) < 0.9f && !AttackCool)
                {
                    StartCoroutine(Attack());
                }
                if (Hero.transform.position.x < transform.position.x)
                {
                    if (!TurnCool)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                        StartCoroutine(TurnCoolToggle());
                    }
                    transform.position += new Vector3(-0.02f, 0, 0);
                }
                if (Hero.transform.position.x > transform.position.x)
                {
                    if (!TurnCool)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                        StartCoroutine(TurnCoolToggle());
                    }
                    transform.position += new Vector3(0.02f, 0, 0);
                }
            }
        }
    }
	
    IEnumerator Pace()
    {
        if (State==0) {
            if (Random.Range(0, 2) == 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            }
            StartCoroutine(Step());
        }
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        StartCoroutine(Pace());
    }
    IEnumerator Step()
    {
        Walking = true;
        for (int i = 0; i < Random.Range(50, 100); i++)
        {
            transform.position += new Vector3(0.01f * transform.localScale.x, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        Walking = false;
    }
    IEnumerator WalkFrames()
    {
        if (Walking && !Attacking)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
            yield return new WaitForSeconds(0.3f);
        if (Walking && !Attacking)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[2];
        }
        
        StartCoroutine(WalkFrames());
    }
    IEnumerator Attack()
    {
        Attacking = true;
        AttackCool = true;
        GetComponent<SpriteRenderer>().sprite = sprites[3];
        Tongue.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.4f);
        Tongue.GetComponent<BoxCollider2D>().enabled = false;
        Attacking = false;
        yield return new WaitForSeconds(0.5f);
        AttackCool = false;
    }
    IEnumerator TurnCoolToggle()
    {
        TurnCool = true;
        yield return new WaitForSeconds(0.3f);
        TurnCool = false;
    }
    public void Die()
    {
        source.GetComponent<AudioSource>().PlayOneShot(death);
        Destroy(gameObject);
    }
}
