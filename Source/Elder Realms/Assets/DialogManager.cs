using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour {
    public GameObject DialogUi;
    public int DialogIndex;
    public bool GoNext;
    public bool InDialog;
    public bool InTrade;
    public bool InShop;
    public bool Writing;
    public Text DialogText;
    public string currdialog;
    public string[] currdialogs;
    public int DialogType; // 0 sell 1 buy 2 small talk 3 quest;
    public GameObject sender;
    public SellUiScript SellUi;
    public BuyUiScript BuyUi;
    public InventoryUiScript InventoryUi;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(DialogUi);
	}
	
	// Update is called once per frame
	void Update () {
        if (SellUi==null&&GameObject.FindGameObjectWithTag("SellUi")!=null)
        {
            SellUi = GameObject.FindGameObjectWithTag("SellUi").GetComponent<SellUiScript>();
        }
        if (BuyUi == null && GameObject.FindGameObjectWithTag("BuyUi") != null)
        {
            BuyUi = GameObject.FindGameObjectWithTag("BuyUi").GetComponent<BuyUiScript>();
        }
        if (DialogUi.GetComponent<Canvas>().enabled)
        {
            InDialog = true;
        }
        else
        {
            InDialog = false;
        }
        if (InDialog&&Input.GetKeyDown("c"))
        {
            Skip();
        }
        if (InDialog&&currdialog!=null) {
            if (GoNext && DialogIndex >= currdialogs.Length)
            {
                EndDialog();
            }
        }
	}
    public void ShowDialog(string[] dialogs,int dialogtype,GameObject sender)
    {
        DialogType = dialogtype;
        this.sender = sender;
        DialogUi.GetComponent<Canvas>().enabled = true;
        DialogIndex = 0;
        currdialogs = dialogs;
        StartCoroutine(DrawDialog(dialogs));
    }
    IEnumerator DrawDialog(string[] dialogs)
    {
        for (int i = 0; i<dialogs.Length;i++)
        {
            yield return new WaitUntil(() => GoNext);
            StartCoroutine(DisplayDialog(dialogs[i]));
        }
    }
     IEnumerator DisplayDialog(string dialog)
    {
        currdialog = dialog;
        Writing = true;
        GoNext = false;
        for (int i = 0; i < dialog.ToCharArray().Length; i++) {
            string tempdialog = "";
            for (int x  = 0; x<i+1;x++)
            {
                tempdialog += dialog[x];
            }
            DialogText.text = tempdialog;
            yield return new WaitForSeconds(2*Time.deltaTime);
            if (!Writing)
            {
                i = dialog.ToCharArray().Length;
            }
        }
        Writing = false;
    }
    public void Skip()
    {
        DialogText.text = currdialog;
        if (!Writing)
        {
            DialogIndex += 1;
            GoNext = true;
        }
        Writing = false;
    }
    public void EndDialog()
    {
        if (DialogType==0)
        {
            InTrade = true;
            SellUi.gameObject.GetComponent<Canvas>().enabled = true;
            InventoryUi.CloseInventory();
            SellUi.RefreshSellInventory();
            InventoryUi.gameObject.GetComponent<Canvas>().enabled = false;
        }
        if (DialogType == 1)
        {
            InShop = true;
            BuyUi.gameObject.GetComponent<Canvas>().enabled = true;
            InventoryUi.CloseInventory();
            BuyUi.RefreshBuyInventory();
            InventoryUi.gameObject.GetComponent<Canvas>().enabled = false;
        }
        DialogUi.GetComponent<Canvas>().enabled = false;
        StartCoroutine(AntiDialogSpam());
        DialogIndex = 0;
    }
    IEnumerator AntiDialogSpam()
    {
        yield return new WaitForSeconds(0.2f);
        InDialog = false;
    }
}
