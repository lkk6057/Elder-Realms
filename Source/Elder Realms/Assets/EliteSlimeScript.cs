using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteSlimeScript : MonoBehaviour
{
    public GameObject source;
    public GameObject Hero;
    public int State = 0;
    public AudioClip death;
    public bool Walking;
    public AudioSource audiosource;
    public bool CanJump;
    public bool IsGrounded;
    public bool CanHit;
    public AudioClip stomp;
    public bool frenzy;
    public bool Rejumping;
    // Use this for initialization
    void Start()
    {
        CanHit = true;
        StartCoroutine(Pace());
        audiosource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        Hero = GameObject.FindGameObjectWithTag("Hero");
        if (Random.Range(0, 2) == 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        }
        frenzy = false;
        CanJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Entity>().Health<=(GetComponent<Entity>().MaxHealth*0.5f)&&!frenzy&&State!=1)
        {
            frenzy = true;
        }
        if (Vector3.Distance(transform.position, Hero.transform.position) > 10f)
        {
            State = 0;
        }
        if (Vector3.Distance(transform.position, Hero.transform.position) <8f&&Mathf.Abs(Hero.transform.position.y-transform.position.y)<=1.5f)
        {
            State = 1;

            if (State == 1)
            {
                if (Hero.transform.position.x < transform.position.x)
                {
                        transform.localScale = new Vector3(-1, 1, 1);
                    if (CanJump)
                    {
                        Jump(-2);
                    }
                }
                if (Hero.transform.position.x > transform.position.x)
                {
                        transform.localScale = new Vector3(1, 1, 1);
                    if (CanJump)
                    {
                        Jump(2);
                    }
                }
            }
        }
    }

    IEnumerator Pace()
    {
        if (State == 0)
        {
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
    public void Die()
    {
        audiosource.GetComponent<AudioSource>().PlayOneShot(death);
        Destroy(gameObject);
    }
    public void Jump(float dir)
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(dir,2),ForceMode2D.Impulse);
        CanJump = false;
        IsGrounded = false;
    }
    IEnumerator Rejump()
    {
        Rejumping = true;
        yield return new WaitForSeconds(0.4f);
        CanJump = true;
        Rejumping = false;
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag=="Ground"&&coll.gameObject.GetComponent<SlimeScript>()==null)
        {
            IsGrounded = true;
            if (coll.transform.tag == "Ground")
            {
                if (!Rejumping)
                {
                    StartCoroutine(Rejump());
                }
            }
            if (!CanJump&&audiosource!=null) {
                audiosource.PlayOneShot(stomp);
            }
        }
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.transform.tag == "Hero" && CanHit)
        {
            StartCoroutine(HitPlayer(Random.Range(25, 45)));
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.transform.tag == "Ground")
        {
            IsGrounded = false;
        }
    }
    IEnumerator HitPlayer(float damage)
    {
        CanHit = false;
        Hero.GetComponent<HeroScript>().Hit(damage);
        yield return new WaitForSeconds(0.75f);
        CanHit = true;
    }
}
