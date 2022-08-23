using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEventController
{
    /// <summary>
    /// Main Event id string
    /// </summary>
    private string ME_str = "";
    /// <summary>
    /// Main Event story id
    /// </summary>
    private int ME_storyID = 0;
    /// <summary>
    /// Main Event interval
    /// </summary>
    private int ME_interval = 0;
    
    /// <summary>
    /// ME_str 저장
    /// </summary>
    private string last_ME_str = "";

    private static MainEventController _instance;

    public static MainEventController instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = new MainEventController();
            }
            return _instance;
        }
    }
    /// <summary>
    /// Set ME_str, PlayerPrefs 저장
    /// </summary>
    /// <param name="_ME">현재 ME id 값</param>
    public void SetME_str(string _ME)
    {
        ME_str = _ME;
        ME_storyID = _ME[1] - '0';
        last_ME_str = ME_str;
        if (Player.instance.isAdmin)
        {
            return;
        }

        PlayerPrefs.SetInt("ME_storyID", ME_storyID);
    }
    /// <summary>
    /// Main Event str Get
    /// </summary>
    /// <returns>현재 Main Event Str</returns>
    public string GetME_str()
    {
        return ME_str;
    }
    /// <summary>
    /// 다음 MainEvent 계산
    /// </summary>
    /// <returns></returns>
    public string GetNextMe()
    {
        if(!Player.instance.isAdmin)
            ME_str = PlayerPrefs.GetString("ME_str"); // 현재 ME_str 저장
        else
        {
            ME_str = last_ME_str;
        }
        // 현재 ME_str의 script의 afterInterval 불러옴
        Script script = MakeDialog.instance.FindScript(ME_str);
        return script.afterInterval;
    }
    /// <summary>
    /// interval 불러옴
    /// </summary>
    public void FirstSetInterval()
    {
        if (PlayerPrefs.GetInt("ME_interval") == 0 || Player.instance.isAdmin) // 저장된 interval 존재 x
            ME_interval = MakeDialog.instance.FindScript(ME_str).interval;
        else
            ME_interval = PlayerPrefs.GetInt("ME_interval");
    }
    /// <summary>
    /// interval 감소
    /// </summary>
    public void IntervalDecrease()
    {
        ME_interval--;
    }
    /// <summary>
    /// returns ME_interval
    /// </summary>
    /// <returns></returns>
    public int GetInterval()
    {
        return ME_interval;
    }
    /// <summary>
    /// interval set
    /// </summary>
    /// <param name="value">interval</param>
    public void SetInterval(int value)
    {
        ME_interval = value;
    }
}
