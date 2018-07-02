using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderScript : MonoBehaviour {
    public GameObject Hero;
    public DialogManager dialogmanager;
    string[] dialogs = { "One man's trash is another man's treasure!", "Sell your unwanted items for the best rates in all the realm!", "Have anything to sell?", "Looking for extra gold?" };
    // Use this for initialization
    void Start () {
        Hero = GameObject.FindGameObjectWithTag("Hero");
        dialogmanager = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, Hero.transform.position) <= 0.5f&&Input.GetKeyDown("c")&&!dialogmanager.InDialog&&!dialogmanager.InTrade)
        {
            string tempdialog = dialogs[Random.Range(0, dialogs.Length)];
            dialogmanager.ShowDialog(new string[] {tempdialog},0,gameObject);
        }
    }
}
