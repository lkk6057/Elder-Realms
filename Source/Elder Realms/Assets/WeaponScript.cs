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
    public HeroScript heroscript;
    public ItemManager itemmanager;
    public Sprite[] WeaponSprites;
    public NotificationManagerScript noty;
	// Use this for initialization
	void Start () {
        WeaponId = 0;
        heroscript = Hero.GetComponent<HeroScript>();
        itemmanager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        noty = GameObject.FindGameObjectWithTag("NotificationManager").GetComponent<NotificationManagerScript>();
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
        bool SwordEquipped = false;
        for(int i = 0; i<heroscript.inventory.Count;i++)
        {
            if (heroscript.inventory[i].item.itemtype==1&&heroscript.inventory[i].equipped)
            {
                SetSprite(heroscript.inventory[i].item.itemspriteid);
                WeaponId = heroscript.inventory[i].item.itemspriteid;
                SwordEquipped = true;
            }
        }
        if (SwordEquipped)
        {
            Swinging = true;
            CanSwing = false;
            transform.eulerAngles = new Vector3(0, 0, 30 * -Hero.GetComponent<HeroScript>().dir);
            StartCoroutine(SwingAnim());
        }
        
    }
    public void SetSprite(int id)
    {
        if (id==2)
        {
            GetComponent<SpriteRenderer>().sprite = WeaponSprites[0];
        }
        if (id == 3)
        {
            GetComponent<SpriteRenderer>().sprite = WeaponSprites[1];
        }
    }
    IEnumerator SwingAnim()
    {
            source.PlayOneShot(sounds[Random.Range(0, 2)]);
        for (int i = 0; i<12; i++)
        {
            transform.eulerAngles += new Vector3(0,0,9*Hero.GetComponent<HeroScript>().dir);
            yield return new WaitForSeconds(0.01f);
            
        }
        Swinging = false;
        yield return new WaitForSeconds(0.4f);
        CanSwing = true;

    }
    public float GetWeaponDamage(int id)
    {
        if (id==2)
        {
            return Random.Range(375f, 450f);
        }
        if (id==3)
        {
            return Random.Range(40,70);
        }
        return 50;
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Entity>()!=null)
        {
            float damage = GetWeaponDamage(WeaponId);
            coll.GetComponent<Entity>().Hit(damage);
            noty.SpawnNotification(coll.transform.position, Mathf.Round(damage).ToString(),Color.red);
            
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
