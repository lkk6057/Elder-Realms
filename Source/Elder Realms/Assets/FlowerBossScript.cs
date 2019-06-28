using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBossScript : MonoBehaviour {
    public GameObject TriggerPortal;
    public Sprite[] sprites;
    public bool Activated;
    public bool Loop;
    public GameObject Player;
    public int dir;
    public GameObject Fireball;
    public AudioClip FireballSound;
    public Transform Mouth;
    public SceneLoader loader;
    public MusicScript music;
	// Use this for initialization
	void Start () {
        
        dir = -1;
        Player = GameObject.FindGameObjectWithTag("Hero");
        loader = GameObject.FindGameObjectWithTag("Loader").GetComponent<SceneLoader>();
        music = GameObject.FindGameObjectWithTag("Audio").GetComponent<MusicScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Activated) {
            if (Player.transform.position.x <= transform.position.x && Player != null)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
	}
    public void Activate()
    {
        if (!Activated)
        {
            Activated = true;
            StartCoroutine(AiLoop());
            music.PlayLoop(loader.musics[1]);
        }
    }
    public void Die()
    {
        Destroy(gameObject);
        TriggerPortal.GetComponent<PortalScript>().enabled = true;
        TriggerPortal.GetComponent<SpriteRenderer>().enabled = true;
    }
    IEnumerator Step()
    {
        if (Random.Range(0, 2) == 0)
        {
            dir = -dir;
        }
        for (int i = 0; i < Random.Range(50, 100); i++)
        {
            transform.position += new Vector3(0.02f * dir, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
    public IEnumerator AiLoop()
    {
        if (Activated)
        {
            Loop = !Loop;
            if (Loop)
            {
                StartCoroutine(Flurry());
                if (Random.Range(0, 2) == 0)
                {
                    StartCoroutine(Step());
                }

            }
            else
            {
                StartCoroutine(Step());
            }
        }
        yield return new WaitForSeconds(Random.Range(2.0f,3.0f));
        StartCoroutine(AiLoop());
    }
    public IEnumerator Flurry()
    {
        for (int i = 0; i<5;i++) {
            GetComponent<SpriteRenderer>().sprite = sprites[1];
            Attack();
            yield return new WaitForSeconds(0.2f);
            GetComponent<SpriteRenderer>().sprite = sprites[0];
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void Attack()
    {
        var ball = (GameObject)Instantiate(Fireball,Mouth.position,transform.rotation);
        ball.GetComponent<FireballScript>().target = new Vector3(Player.transform.position.x, Player.transform.position.y+0.4f, Player.transform.position.z);
        GetComponent<AudioSource>().PlayOneShot(FireballSound);
    }
}
