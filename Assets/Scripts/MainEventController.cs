using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEventController
{
    private string ME_str = "";
    private int ME_storyID = 0;
    private int ME_interval = 0;

    private static MainEventController instance;

    public static MainEventController Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new MainEventController();
            }
            return instance;
        }
    }

    public void SetME_str(string _ME)
    {
        ME_str = _ME;
        ME_storyID = _ME[1] - '0';
    }
    public string GetME_str()
    {
        return ME_str;
    }
    public string getNextME()
    {
        ME_storyID++;
        return "M" + ME_storyID + "_1";
    }
    public void FirstSetInterval()
    {
        ME_interval = MakeDialog.Instance.FindScript(ME_str).interval;
        
    }
    public void IntervalDecrease()
    {
        ME_interval--;
    }
    public int GetInterval()
    {
        return ME_interval;
    }
}
