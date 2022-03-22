using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PockerManager : MonoBehaviour
{
    
    public GameObject pocketpreview_panel;
    public GameObject pocket_panel;
    public Text pocket_text;
    public Text ability_text; 
    public Text item_text;
    public Text state_text;

    public Text pocketPre_text1;
    public Text pocketPre_text2;
    public Text pocketPre_text3;

    public GameObject sortbutton1;
    public GameObject sortbutton2;
    private bool sortbuttonBool;
    private string[] tmplist = new string[3]; 
    // Start is called before the first frame update
    void Start()
    {
        //Player pocket setup for test
        Player.Test();
        SetPreviewUIpanel();
    }
    void SetPreviewUIpanel()
    {
        Player.instance.SetPreviewStr().CopyTo(tmplist);
        int length = tmplist.Length;

        if (length >= 1)
            pocketPre_text1.text = tmplist[0];
        if (length >= 2)
            pocketPre_text2.text = tmplist[1];
        if (length >= 3)
            pocketPre_text3.text = tmplist[2];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenPanel()
    {
        //UI panel setup
        pocketpreview_panel.SetActive(false);
        pocket_panel.SetActive(true);

        //panel text setup
        pocket_text.text = Player.instance.SetStr(0);
        ability_text.text = Player.instance.SetStr(1);
        item_text.text = Player.instance.SetStr(2);
        state_text.text = Player.instance.SetStr(3);
    }
    public void ClosePanel()
    {
        //UI panel setup
        pocket_panel.SetActive(false);
        pocketpreview_panel.SetActive(true);

        //preview text setup
        SetPreviewUIpanel();
    }
    public void RestartAdventure()
    {
        //To Do: go back to first story
        Debug.Log("Play Again");

        //Player Stats setup
        Player.instance.SetAgain();
    }
    public void SortButton()
    {
        Player.instance.Sortbytime = !Player.instance.Sortbytime;
        sortbuttonBool = Player.instance.Sortbytime;
        sortbutton1.SetActive(sortbuttonBool);
        sortbutton2.SetActive(!sortbuttonBool);


        //panel text setup
        pocket_text.text = Player.instance.SetStr(0);
        ability_text.text = Player.instance.SetStr(1);
        item_text.text = Player.instance.SetStr(2);
        state_text.text = Player.instance.SetStr(3);
    }
}
