using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PortalScript : MonoBehaviour {
    public GameObject Hero;
    public Vector3 Tploc;
    public Vector3 Offset;
    public int Tpscene;
    // Use this for initialization
    void Start () {
        Hero = GameObject.FindGameObjectWithTag("Hero");
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(Offset,Hero.transform.position)<=0.5f)
        {
            Hero.GetComponent<HeroScript>().CheckTp(Tploc,Tpscene);
        }
	}
}
