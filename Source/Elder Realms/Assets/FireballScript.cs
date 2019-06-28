using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour {
    public Vector3 target;
    public Vector3 force;
    // Use this for initialization
    void Start() {
        Fire();
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(),GameObject.FindGameObjectWithTag("FlowerBoss").GetComponent<BoxCollider2D>());
    }

    // Update is called once per frame
    void Update() {
    }
    public void Fire()
    {
        force = new Vector3((target.x - transform.position.x)*1.5f, 2, 0);
        GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Ground"|| coll.transform.tag == "Bounds"|| coll.transform.tag == "Sword")
        {
            Destroy(gameObject);
        }
        if(coll.transform.tag == "Hero")
        {
            coll.transform.GetComponent<HeroScript>().Hit(25);
            Destroy(gameObject);
        }
    }
    
    
}
