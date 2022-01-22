using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextContainer
{
    private static NextContainer instance;
    public static NextContainer Instance
    {
        get
        {
            if (instance == null) 
            { 
                instance = new NextContainer();
            }
            return instance;
        }
    }
    public NextContainer()
    {
        nextChoice = new List<string>();
        Debug.Log(PlayerPrefs.GetString("ScriptID"));
        if(PlayerPrefs.GetString("ScriptID") == "")
        {
            nextChoice.Add("m1_1");
            nextText = "M1_1";
            MainEventController.Instance.SetME_str("M1_1");
        }
        else
        {
            string currentID = PlayerPrefs.GetString("ScriptID");
            Script currentScript = MakeDialog.Instance.FindScript(currentID);
            for(int i =0; i < currentScript.next.Count; i++)
            {
                nextChoice.Add(currentScript.next[i]);
            }
            nextText = currentID;
        }
    }

    public List<string> nextChoice;
    public string nextText;
}
