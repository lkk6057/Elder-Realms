using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NotificationScript : MonoBehaviour {
    public Text notificationtext;
	// Use this for initialization
	void Start () {
        Destroy(gameObject,1f);
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    public void SetText(string text,Color color)
    {
        notificationtext.text = text;
        notificationtext.color = color;
    }
}
