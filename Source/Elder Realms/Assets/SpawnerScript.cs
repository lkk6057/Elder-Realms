using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {
    public GameObject EntityPrefab;
    public GameObject EntityAssigned;
    public bool CanSpawn;
    public float CooldownMin;
    public float CooldownMax;
	// Use this for initialization
	void Start () {
        var Entity = (GameObject)Instantiate(EntityPrefab,transform.position,transform.rotation);
        EntityAssigned = Entity;
        CanSpawn = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (EntityAssigned == null&&CanSpawn)
        {
            StartCoroutine(Respawn());
        }
	}
    IEnumerator Respawn()
    {
        CanSpawn = false;
        yield return new WaitForSeconds(Random.Range(CooldownMin,CooldownMax));
        var Entity = (GameObject)Instantiate(EntityPrefab, transform.position, transform.rotation);
        EntityAssigned = Entity;
        CanSpawn = true;
    }
}
