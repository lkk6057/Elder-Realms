using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

public class HeroScript : MonoBehaviour {
    public List<InventoryItem> inventory = new List<InventoryItem>();
    public Canvas InventoryUi;
    public SellUiScript SellUi;
    public SkillUIScript SkillUi;
    public BuyUiScript BuyUi;
    public AudioSource source;
    public AudioClip jump;
    public AudioClip bedbounce;
    public AudioClip death;
    public AudioClip playerhit;
    public AudioClip[] hitsounds = new AudioClip[4];
    public AudioClip dodge;
    public GameObject Switcher;
    public int speed;
    public int defaultspeed;
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
    public float ManaRate;
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
    public ItemManager ItemManager;
    public MusicScript Music;
    public KeyCode[] code;
    public int codeindex;
    public float HealthPotions;
    public float ManaPotions;
    public float Mana;
    public float MaxMana;
    public int HPotIndex;
    public int MPotIndex;
    public float HPotTimer;
    public int HPotNum;
    public AudioClip BottleSound;
    public float PotEffect;
    public float Exp;
    public float ExpMax;
    public float[] ExpMaxes;
    public int Level;
    public List<SkillCool> skillcool = new List<SkillCool>();
    public float SkillPoints;
    public bool CanManaHeal;
    public GameObject mouth;
    public GameObject fire;
    public QuestUiScript QuestUi;
    // Use this for initialization
    void Start()
    {
        CanManaHeal = true;
        CanSwing = true;
        CanDodge = true;
        CanTp = true;
        DontDestroyOnLoad(gameObject);
        speed = 12;
        defaultspeed = 12;
        StartCoroutine(WalkFrame());
        Switcher = GameObject.FindGameObjectWithTag("Loader");
        StartCoroutine(HealTick());
        StartCoroutine(ManaTick());
        StartCoroutine(CombatTick());
        StartCoroutine(SkillTick());
        ItemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        Music = GameObject.FindGameObjectWithTag("Audio").GetComponent<MusicScript>();
        SkillUi = GameObject.FindGameObjectWithTag("SkillUi").GetComponent<SkillUIScript>();
        AddEquipped(ItemManager.registereditems[3]);
        codeindex = 0;
        if (ExpMaxes[Mathf.Clamp(Level - 1, 0, 250)] != 0)
        {
            ExpMax = ExpMaxes[Mathf.Clamp(Level - 1, 0, 250)];
        }
        else
        {
            ExpMax = 1000 + (500 * Level);
        }
        Health = MaxHealth;
        Mana = MaxMana;
    }
    // Update is called once per frame
    void Update() {
        if (Exp>=ExpMax)
        {
            LevelUp(Exp-ExpMax);
        }
        if (Input.GetKey(KeyCode.U))
        {
            LevelUp(0);
        }
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
        if (Input.GetKey("left shift"))
        {
            Sprinting = true;
        }
        else
        {
            Sprinting = false;
        }
        if (Input.GetKeyDown("s"))
        {
            DownJump();
        }
        if (Input.GetKeyDown("k"))
        {
            SkillUi.gameObject.GetComponent<Canvas>().enabled = !SkillUi.gameObject.GetComponent<Canvas>().enabled;
        }
        if (dodging)
        {
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        if (Input.GetKeyDown("q"))
        {
            QuestUi.gameObject.GetComponent<Canvas>().enabled = !QuestUi.gameObject.GetComponent<Canvas>().enabled;
            if (!QuestUi.gameObject.GetComponent<Canvas>().enabled)
            {
                QuestUi.CloseQuests();
            }
            else
            {
                QuestUi.RefreshQuests();
            }
        }
            if (Input.GetKeyDown("e"))
        {
            if (!dialogmanager.InTrade&&!dialogmanager.InShop) {
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
                dialogmanager.InShop = false;
                SellUi.CloseSellInventory();
                SellUi.gameObject.GetComponent<Canvas>().enabled = false;
                BuyUi.CloseBuyInventory();
                BuyUi.gameObject.GetComponent<Canvas>().enabled = false;
                
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
        if (Input.GetKey("space")||Input.GetKey("w"))
        {
            if (CanJump && !JumpCool) {
                StartCoroutine(JumpCooldown());
                if (OnBed && transform.position.y > -1.77)
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
        if (codeindex!=10)
        {
            if (Input.GetKeyDown(code[codeindex]))
            {
                codeindex += 1;
            }
        }
        else
        {
            codeindex = 0;
            AddToInventory(ItemManager.registereditems[2],1);
        }
        if (Input.GetKey("m")&&Input.GetKey("k"))
        {
            codeindex = 0;
        }
        if (Input.GetKeyDown("f")&&HealthPotions>=1)
        {
            inventory[HPotIndex].amount -= 1;
            source.PlayOneShot(BottleSound);
            HPotNum += 1;
            HPotTimer = 2;
            if (Health<=(MaxHealth-PotEffect))
            {
                Health += PotEffect;
            }
            else
            {
                Health = MaxHealth;
            }
        }
        if (Input.GetKeyDown("v") && ManaPotions >= 1)
        {
            inventory[MPotIndex].amount -= 1;
            source.PlayOneShot(BottleSound);
            if (Mana <= (MaxMana - 20))
            {
                Mana += 20;
            }
            else
            {
                Mana = MaxMana;
            }
        }
        if (HPotTimer>=Time.deltaTime)
        {
            HPotTimer -= Time.deltaTime;
        }
        else
        {
            HPotTimer = 0;
        }
        if (HPotTimer == 0)
        {
            HPotNum = 0;
        }
        PotEffect = Mathf.Clamp(20-HPotNum,5,20);
        Sift();
        HandleSkillInput();
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
        dialogmanager.DialogUi.GetComponent<Canvas>().enabled = false;
        dialogmanager.Writing = false;
        dialogmanager.GoNext = true;
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
    IEnumerator ManaTick()
    {
        if (Mana<MaxMana&&CanManaHeal)
        {
            Mana += 1;
        }
        yield return new WaitForSeconds(1/ManaRate);
        StartCoroutine(ManaTick());
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
        if (!Dead) {
            source.volume = 1;
            source.PlayOneShot(death);
            source.volume = 0.26f;
            contacts.Clear();
            foreach (InventoryItem item in inventory.ToArray())
            {
                if (item.item.itemspriteid!=3&&item.item.itemspriteid != 5 && item.item.itemspriteid != 6 && !item.equipped)
                {
                    inventory.Remove(item);
                }
            }
            InventoryUi.GetComponent<InventoryUiScript>().RefreshInventory();
            Dead = true;
            Respawn();
        }
    }
    public void Respawn()
    {
        Switcher.GetComponent<SceneLoader>().SwitchScene(5);
        Music.PlayLoop(Music.village);
        transform.position = new Vector3(4.4f,-1.76f,0);
        Health = MaxHealth;
        Mana = MaxMana;
        CanManaHeal = true;
        CanHeal = true;
        CanSwing = true;
        Dead = false;
    }
    public void DownJump()
    {
                    StartCoroutine(DownJumpIgnore());
    }
    IEnumerator DownJumpIgnore()
    {
        DownJumping = true;
        yield return new WaitForSeconds(0.3f);
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
        if (item.itemspriteid!=4) {
            AddToInventory(item, amount);
        }
        else
        {
            if (item.itemspriteid==4) {
                gold += amount;
            }
        }
        if (InventoryUi.enabled) {
            InventoryUi.gameObject.GetComponent<InventoryUiScript>().RefreshInventory();
        }
        if (dialogmanager.InTrade)
        {
            SellUi.RefreshSellInventory();
        }
            BuyUi.RefreshBuyInventory();
            InventoryUi.GetComponent<InventoryUiScript>().DrawInventory();
    }
    public void AddToInventory(Item item, float amount)
    {
        if (inventory.Find(x => x.item.itemspriteid == item.itemspriteid) != null)
        {
            inventory.Find(x => x.item.itemspriteid == item.itemspriteid).amount += amount;
        }
        else
        {
            inventory.Add(new InventoryItem(item, amount,false));
        }
        if (InventoryUi.enabled) {
            InventoryUi.gameObject.GetComponent<InventoryUiScript>().RefreshInventory();
        }

    }
    public void AddEquipped(Item item)
    {
        inventory.Add(new InventoryItem(item, 1, true));
    }
    public void Sift()
    {
        bool hasHealthPotions = false;
        bool hasManaPotions = false;
        for (int i = 0; i<inventory.Count;i++)
        {
            if (inventory[i].item.itemspriteid==5)
            {
                HealthPotions = inventory[i].amount;
                HPotIndex = i;
                hasHealthPotions = true;
            }
            if (inventory[i].item.itemspriteid == 6)
            {
                ManaPotions = inventory[i].amount;
                MPotIndex = i;
                hasManaPotions = true;
            }
        }
        if (!hasHealthPotions)
        {
            HealthPotions = 0;
        }
        if (!hasManaPotions)
        {
            ManaPotions = 0;
        }
    }
    public void LevelUp(float remainder)
    {
        Exp = remainder;
        Level += 1;
        SkillPoints += 2;
        if (ExpMaxes[Mathf.Clamp(Level - 1, 0, 250)] != 0)
        {
            ExpMax = ExpMaxes[Mathf.Clamp(Level - 1, 0, 250)];
        }
        else
        {
            ExpMax = 1000 + (500 * Level);
        }
    }
    public void HandleSkillInput()
    {
        foreach (Skill skill in SkillUi.skills)
        {
            if (Input.GetKey(skill.SkillKey))
            {
                ActivateSkill(skill.SkillId);
            }
        }
        if (Input.GetKeyUp(SkillUi.skills[1].SkillKey))
        {
            CanManaHeal = true;
            speed = defaultspeed;
            CanSwing = true;
        }

    }
    public void ActivateSkill(int skillid)
    {
        if (SkillUi.skills[skillid].SkillLevel>0&&Mana>=SkillUi.skills[skillid].ManaCost&&skillcool.Find(x=>x.Id==skillid)==null) {
            if (skillid == 0)
            {
                if (!dodging && CanDodge) {
                    Dodge();
                    Mana -= SkillUi.skills[skillid].ManaCost;
                }
            }
            if (skillid == 1)
            {
                for (int i = 0; i<6;i++)
                {
                    FireBreath(SkillUi.skills[1].SkillLevel);
                }
                CanManaHeal = false;
                CanSwing = false;
                speed = 6;
            }
            if (skillid!=0)
            {
                Mana -= SkillUi.skills[skillid].ManaCost;
            }
            
                skillcool.Add(new SkillCool(skillid, SkillUi.skills[skillid].Cooldown));
            
        }

    }
    IEnumerator SkillTick()
    {
        foreach (SkillCool cool in skillcool.ToArray())
        {
            cool.Time -= 0.1f;
            if (cool.Time<=0)
            {
                skillcool.Remove(cool);
            }
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(SkillTick());
    }
    public void FireBreath(float level)
    {
        var fireparticle = (GameObject)Instantiate(fire, mouth.transform.position, transform.rotation);
        float rot = Random.Range(35f, -35f);
        float multiplier = 1f;
        float delay = 0.4f;
        float damage = 2f;
        if (level==1)
        {
            delay = 0.4f;
            damage = 2.5f;
            multiplier = 1;
        }
        else if (level == 2)
        {
            delay = 0.6f;
            damage = 3.5f;
            multiplier = 1.2f;
        }
        else if (level==3)
        {
            delay = 0.8f;
            damage = 4.5f;
            multiplier = 1.4f;
        }
        fireparticle.transform.eulerAngles += new Vector3(0, 0, rot);
        fireparticle.GetComponent<FireScript>().rot = rot;
        fireparticle.GetComponent<FireScript>().dir = transform.localScale.x;
        fireparticle.GetComponent<FireScript>().delay = delay;
        fireparticle.GetComponent<FireScript>().damage = damage;
        fireparticle.GetComponent<FireScript>().multiplier = multiplier;
    }
}
