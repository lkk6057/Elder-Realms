using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public GameObject Hero;
    public DialogManager dialogmanager;
    string[] dialogs = {"Looking for some sturdy gear?","My swords are the finest in all the realm!","Need some steel?"};
    // Use this for initialization
    void Start()
    {
        Hero = GameObject.FindGameObjectWithTag("Hero");
        dialogmanager = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Hero.transform.position) <= 0.5f && Input.GetKeyDown("c") && !dialogmanager.InDialog && !dialogmanager.InTrade&&!dialogmanager.InShop)
        {
            string tempdialog = dialogs[Random.Range(0, dialogs.Length)];
            dialogmanager.ShowDialog(new string[] { tempdialog }, 1, gameObject);
        }
    }
}
