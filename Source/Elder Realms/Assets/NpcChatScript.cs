using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcChatScript : MonoBehaviour {
    public GameObject Hero;
    public DialogManager dialogmanager;
    public string[] dialogs;
    public string[] randomdialogs;
    public int dialogmode; //0 dialogue path 1 random from list
    public bool quest;
    // Use this for initialization
    void Start()
    {
        Hero = GameObject.FindGameObjectWithTag("Hero");
        dialogmanager = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Hero.transform.position) <= 0.5f && Input.GetKeyDown("c") && !dialogmanager.InDialog && !dialogmanager.InTrade)
        {
            if (dialogmode == 0)
            {
                dialogmanager.ShowDialog(dialogs, 2, gameObject);
            }
            if(dialogmode == 1)
            {
                string tempdialog = randomdialogs[Random.Range(0, randomdialogs.Length)];
                dialogmanager.ShowDialog(new string[] { tempdialog }, 2, gameObject);
            }
        }
    }
}
