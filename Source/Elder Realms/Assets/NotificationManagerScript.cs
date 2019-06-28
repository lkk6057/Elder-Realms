using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManagerScript : MonoBehaviour {
    public GameObject notification;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SpawnNotification(Vector3 position,string text,Color color)
    {
        var noty = (GameObject)Instantiate(notification, new Vector3(position.x + Random.Range(-1f, 1f), position.y + Random.Range(0f, 1f), 0), transform.rotation);
        noty.GetComponent<NotificationScript>().SetText(text,color);
        noty.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-2f,2f),Random.Range(4f,6f)),ForceMode2D.Impulse);
    }
}
