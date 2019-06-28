using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {
    public bool over;
    public GameObject[] components;
    public bool drag;
    public Canvas basecanvas;
	// Use this for initialization
	void Start () {
        over = false;
    }
    public void SetEnabled(bool overenabled)
    {
        over = overenabled;
    }
	
	// Update is called once per frame
	void Update () {
        if (drag&&basecanvas.enabled)
        {
            foreach (GameObject component in components) {
                component.transform.position = new Vector3(Mathf.Clamp(Input.mousePosition.x,50,Screen.width-50),Mathf.Clamp(Input.mousePosition.y-140,-100,Screen.height-150),0);
            }
        }
        if (over && Input.GetMouseButtonDown(0))
        {
            drag = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            drag = false;
        }
        if (basecanvas.enabled)
        {
            GetComponent<Image>().raycastTarget = true;
        }
        else
        {
            GetComponent<Image>().raycastTarget = false;
        }
	}
}
