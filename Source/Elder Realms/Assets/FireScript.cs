using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour {
    public Vector3 direction;
    public float dir;
    public float delay;
    public float rot;
    public float rotquot;
    public float damage;
    public float multiplier;
	// Use this for initialization
	void Start () {
        direction = transform.right;
        rotquot = Random.Range(0.7f,1.3f);
        Destroy(gameObject,delay);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += multiplier*dir*direction / 25;
        transform.localScale += new Vector3(0.6f,0.6f,0)*multiplier;
        transform.Rotate(new Vector3(0,0,rotquot*rot/2));
	}
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.GetComponent<Entity>()!=null)
        {
            coll.GetComponent<Entity>().Hit(damage);
        }
    }
}
