using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextContainer
{
    private static NextContainer _instance;
    public static NextContainer instance
    {
        get
        {
            if (_instance == null || Player.instance.IsPlayerReset) 
            {
                _instance = new NextContainer();
                Player.instance.IsPlayerReset = false;
            }
            return _instance;
        }
    }
    public NextContainer()
    {
        NextChoice = new List<string>();
        // Debug.Log(PlayerPrefs.GetString("ScriptID"));
        if(PlayerPrefs.GetString("ScriptID") == "") // 저장된 값이 없을 때 -> 새로 시작
        {
            NextChoice.Add("m1_1");
            NextText = "M1_1";
            MainEventController.instance.SetME_str("M1_1");
        }
        else // 이어서 하기
        {
            string currentID = PlayerPrefs.GetString("ScriptID");
            Script currentScript = MakeDialog.instance.FindScript(currentID);
            for(int i =0; i < currentScript.next.Count; i++)
            {
                NextChoice.Add(currentScript.next[i]);
            }
            NextText = currentID;
        }
    }

    public List<string> NextChoice;
    public string NextText;
}
