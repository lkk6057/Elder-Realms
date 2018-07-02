using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroScript : MonoBehaviour {
    public List<InventoryItem> inventory = new List<InventoryItem>();
    public Canvas InventoryUi;
    public SellUiScript SellUi;
    public AudioSource source;
    public AudioClip jump;
    public AudioClip bedbounce;
    public AudioClip death;
    public AudioClip playerhit;
    public AudioClip[] hitsounds = new AudioClip[4];
    public AudioClip dodge;
    public GameObject Switcher;
    public int speed;
    public bool CanJump = true;
    public bool Sprinting = false;
    public bool Moving = false;
    public bool MoveFrame = false;
    public bool InUi;
    public Sprite[] sprites = new Sprite[8];
    public Sprite Frame1;
    public Sprite Frame2;
    public GameObject Particle;
    public float dir;
    public GameObject Weapon;
    public GameObject HeroUi;
    public float Health;
    public float MaxHealth;
    public bool Invulnerable = false;
    public float HealRate;
    public bool CanHeal;
    public float CombatTime;
    public float RemainingCombat;
    public bool InCombat;
    public bool OnBed;
    public bool Dead;
    public bool dodging;
    public bool CanSwing;
    public bool CanDodge;
    public bool CanTp;
    public bool DodgeFrame;
    public bool DownJumping;
    public bool JumpCool;
    public float Constant;
    public GameObject Feet;
    public float gold;
    public List<Collider2D> ignored = new List<Collider2D>();
    public List<GameObject> contacts = new List<GameObject>();
    public List<GameObject> contactstemp = new List<GameObject>();
    public DialogManager dialogmanager;
    // Use this for initialization
    void Start() {
        CanSwing = true;
        CanDodge = true;
        CanTp = true;
        DontDestroyOnLoad(gameObject);
        speed = 12;
        StartCoroutine(WalkFrame());
        Switcher = GameObject.FindGameObjectWithTag("Loader");
        StartCoroutine(HealTick());
        StartCoroutine(CombatTick());
    }

    // Update is called once per frame
    void Update() {
        Constant = Time.deltaTime * 60;
        if (contacts.Count<=0)
        {
            CanJump = false;
        }
        if (Health<=0)
        {
            Die();
        }
        if (Input.GetKey("return")&&!Weapon.GetComponent<WeaponScript>().Swinging && Weapon.GetComponent<WeaponScript>().CanSwing&&CanSwing)
        {
            Weapon.GetComponent<WeaponScript>().Swing();
        }
        if (Input.GetKey("left shift")&&Weapon.GetComponent<WeaponScript>().CanSwing)
        {
            Sprinting = true;
        }
        else
        {
            Sprinting = false;
        }
        if (Input.GetKeyDown("left ctrl")&&!dodging&&CanDodge)
        {
            Dodge();
        }
        if (Input.GetKey("s"))
        {
            DownJump();
        }
        if (dodging)
        {
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        if (Input.GetKeyDown("e"))
        {
            if (!dialogmanager.InTrade) {
                InventoryUi.gameObject.GetComponent<InventoryUiScript>().Inventory.transform.localPosition = new Vector3(0, -200, 0);
                if (InventoryUi.enabled == false)
                {
                    InventoryUi.gameObject.GetComponent<InventoryUiScript>().RefreshInventory();
                }
                else
                {
                    InventoryUi.gameObject.GetComponent<InventoryUiScript>().CloseInventory();
                }
                InventoryUi.enabled = !InventoryUi.enabled;
            }
            else
            {
                dialogmanager.InTrade = false;
                SellUi.CloseSellInventory();
                SellUi.gameObject.GetComponent<Canvas>().enabled = false;
                
            }
        }
        if (!InUi) {
            if (Input.GetKey("a") && !Input.GetKey("d") && !dodging)
            {
                dir = 1;
                if (CanJump)
                {
                    Moving = true;
                }
                if (Sprinting && CanJump)
                {
                    GetComponent<Rigidbody2D>().AddForce(-transform.right * speed * 1.5f * Constant, ForceMode2D.Force);
                }
                else
                {
                    GetComponent<Rigidbody2D>().AddForce(-transform.right * speed * Constant, ForceMode2D.Force);
                }
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (Input.GetKey("d") && !Input.GetKey("a") && !dodging)
            {
                dir = -1;
                if (CanJump)
                {
                    Moving = true;
                }
                if (Sprinting && CanJump)
                {
                    GetComponent<Rigidbody2D>().AddForce(transform.right * speed * 1.5f, ForceMode2D.Force);
                }
                else
                {
                    GetComponent<Rigidbody2D>().AddForce(transform.right * speed, ForceMode2D.Force);
                }
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        if (Input.GetKey("space") && CanJump && !JumpCool)
        {
            StartCoroutine(JumpCooldown());
            if (OnBed&&transform.position.y>-1.77)
            {
                GetComponent<Rigidbody2D>().AddForce(transform.up * 20, ForceMode2D.Impulse);
                source.PlayOneShot(bedbounce);
            }
            else
            {
                GetComponent<Rigidbody2D>().AddForce(transform.up * 13, ForceMode2D.Impulse);
                source.clip = jump;
                source.Play();
            }
            CanJump = false;
            OnBed = false;
        }
        if (!CanJump)
        {
            GetComponent<Rigidbody2D>().AddForce(-transform.up * 12, ForceMode2D.Force);
            Moving = false;
        }
        if (!Moving)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        if (!Input.GetKey("a") && !Input.GetKey("d"))
        {
            Moving = false;
        }
        if (RemainingCombat>0)
        {
            InCombat = true;
        }
        else
        {
            InCombat = false;
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.GetComponent<Entity>() != null)
        {
            if (dodging)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), coll.transform.GetComponent<Collider2D>());
                ignored.Add(coll.transform.GetComponent<Collider2D>());
            }
                
            }
        if (coll.transform.GetComponent<TongueScript>())
        {
            Hit(Random.Range(18, 24));
        }



        if (coll.transform.tag == "Ground")
        {
            contacts.Add(coll.gameObject);
            CanJump = true;
            if (coll.transform.name=="Bed")
            {
                OnBed = true;
            }
        }
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.transform.tag == "Ground")
        {
            CanJump = true;
        }
        if (coll.transform.GetComponent<Entity>() != null)
        {
            if (dodging)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), coll.transform.GetComponent<Collider2D>());
                ignored.Add(coll.transform.GetComponent<Collider2D>());
            }
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.transform.tag == "Ground")
        {
            CanJump = false;
            contacts.Remove(coll.gameObject);
        }
    }
    IEnumerator WalkFrame()
    {
        if (Moving)
        {
            GetComponent<SpriteRenderer>().sprite = Frame1;
        }
        
        if (Sprinting&&Moving)
        {
            yield return new WaitForSeconds(0.2f);
            DirtParticle();
            DirtParticle();
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
        }
        if (Moving)
        {
            GetComponent<SpriteRenderer>().sprite = Frame2;
        }

        if (Sprinting&&Moving)
        {
            yield return new WaitForSeconds(0.2f);
            DirtParticle();
            DirtParticle();
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
        }
        StartCoroutine(WalkFrame());
    }
    public void DirtParticle()
    {
        var DirtPart = (GameObject)Instantiate(Particle,new Vector3(transform.position.x,transform.position.y-0.5f,transform.position.z),transform.rotation);
        DirtPart.GetComponent<Rigidbody2D>().AddForce(transform.up*Random.Range(2f,4.5f), ForceMode2D.Impulse);
        DirtPart.GetComponent<Rigidbody2D>().AddForce(transform.right * dir*Random.Range(1f,2.5f), ForceMode2D.Impulse);
        Destroy(DirtPart,0.5f);
    }
    public void CheckTp(Vector3 Tploc, int scene)
    {
        if (Input.GetKeyDown("c")&&CanTp)
        {
            ClearAllUi();
            StartCoroutine(PortalFrames());
            Switcher.GetComponent<SceneLoader>().SwitchScene(scene);
            transform.position = Tploc;
        }
    }
    public void ClearAllUi()
    {
        SellUi.CloseSellInventory();
        SellUi.gameObject.GetComponent<Canvas>().enabled = false;
        InventoryUi.GetComponent<InventoryUiScript>().CloseInventory();
        InventoryUi.GetComponent<Canvas>().enabled = false;
        dialogmanager.InTrade = false;
        dialogmanager.Writing = false;
        dialogmanager.DialogUi.GetComponent<Canvas>().enabled = false;
        dialogmanager.DialogIndex = 0;
    }
    public void Dodge()
    {
        StartCoroutine(DodgeFrames());
        source.PlayOneShot(dodge);
    }
    IEnumerator DodgeFrames()
    {
        CanDodge = false;
        CanSwing = false;
        DodgeFrame = true;
        dodging = true;
        for (int i = 0; i<20; i++)
        {
            transform.position += new Vector3(-dir/10,0,0);
            yield return new WaitForSeconds(0.01f);
        }
        foreach (Collider2D collider in ignored)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collider, false);
        }
        ignored.Clear();
        dodging = false;
        CanSwing = true;
        yield return new WaitForSeconds(0.3f);
        DodgeFrame = false;
        yield return new WaitForSeconds(0.3f);
        CanDodge = true;



    }
    IEnumerator PortalFrames()
    {
        CanTp = false;
        Invulnerable = true;
        yield return new WaitForSeconds(0.2f);
        CanTp = true;
        yield return new WaitForSeconds(0.3f);
        Invulnerable = false;

    }
    public void Hit(float Damage)
    {
        if (!Invulnerable&&!dodging&&!DodgeFrame) {
            CombatCool();
            if (Health >= Damage)
            {
                if (Damage < 30)
                {
                    source.PlayOneShot(hitsounds[Random.Range(0,3)]);
                }
                else
                {
                    source.PlayOneShot(hitsounds[3]);
                }
                Health -= Damage;
                return;
            }
            if (Health < Damage)
            {
                if (Damage < 30)
                {
                    source.PlayOneShot(hitsounds[Random.Range(0, 3)]);
                }
                else
                {
                    source.PlayOneShot(hitsounds[3]);
                }
                Health = 0;
                return;
            }
        }
    }
    IEnumerator HealTick()
    {
        if (Health < MaxHealth&&CanHeal&&!InCombat)
        {
            Health += 1;
        }
        yield return new WaitForSeconds(1/HealRate);
        StartCoroutine(HealTick());
    }
    public void CombatCool()
    {
        RemainingCombat = CombatTime;
    }
    IEnumerator CombatTick()
    {
        if (RemainingCombat>0.5f)
        {
            RemainingCombat -= 0.5f;
        }
        else
        {
            RemainingCombat = 0;
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(CombatTick());
    }
    public void Die()
    {
        source.volume = 1;
        source.PlayOneShot(death);
        source.volume = 0.26f;
        contacts.Clear();
        Respawn();
    }
    public void Respawn()
    {
        Switcher.GetComponent<SceneLoader>().SwitchScene(5);
        transform.position = new Vector3(4.4f,-1.76f,0);
        Health = MaxHealth;
    }
    public void DownJump()
    {
        CanJump = false;
        contactstemp = contacts;
        foreach(GameObject contact in contactstemp.ToList())
        {
            if (contact.GetComponent<PlatformScript>() != null)
            {
                StartCoroutine(DownJumpIgnore(contact));
            }
        }
    }
    IEnumerator DownJumpIgnore(GameObject contact)
    {
        contacts.Remove(contact);
        GetComponent<Rigidbody2D>().AddForce(-transform.up * 5, ForceMode2D.Impulse);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), contact.GetComponent<Collider2D>());
        DownJumping = true;
        yield return new WaitForSeconds(0.3f);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), contact.GetComponent<Collider2D>(), false);
        DownJumping = false;
    }
    IEnumerator JumpCooldown()
    {
        JumpCool = true;
        yield return new WaitForSeconds(0.3f);
        JumpCool = false;
    }
    public void PickUpItem(Item item, float amount)
    {
        if (inventory.Find(x => x.item==item)!=null)
        {
            inventory.Find(x => x.item == item).amount += amount;
        }
        else
        {
            inventory.Add(new InventoryItem(item,amount));
        }
        if (InventoryUi.enabled) {
            InventoryUi.gameObject.GetComponent<InventoryUiScript>().RefreshInventory();
        }
        if (dialogmanager.InTrade)
        {
            SellUi.RefreshSellInventory();
        }
    }
    
}
