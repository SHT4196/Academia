using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum AchieveState
{
    Hidden,
    NotAchieved, 
    Achieved
}

[CreateAssetMenu(fileName = "Achievement Data", menuName = "Scriptable Object/Achievement Data", order = int.MaxValue)]
public class AchievementObject : ScriptableObject
{
    /// <summary>
    /// 업적의 이름
    /// </summary>
    [SerializeField] private string achievementName;
    public string AchievementName
    {
        get { return achievementName; }
    }

    /// <summary>
    /// 업적 상세 설명
    /// </summary>
    [SerializeField] private string achievementDesc;
    public string AchievementDesc
    {
        get { return achievementDesc; }
    }

    /// <summary>
    /// 업적의 Id
    /// </summary>
    [SerializeField] private int id;
    public int Id
    {
        get { return id; }
    }

    /// <summary>
    /// 업적의 달성 정도
    /// </summary>
    [SerializeField] private int nowNum;
    public int NowNum
    {
        get { return nowNum; }
        set { nowNum = value; }
    }

    /// <summary>
    /// 업적 달성 완료 기준
    /// </summary>
    [SerializeField] private int maxNum;
    public int MaxNum
    {
        get { return maxNum; }
        set { maxNum = value; }
    }

    /// <summary>
    /// 업적의 상태
    /// </summary>
    [SerializeField] private AchieveState achieveState;
    public AchieveState AchieveState
    {
        get { return achieveState; }
        set { achieveState = value; }
    }

    /// <summary>
    /// 업적 썸네일
    /// </summary>
    [SerializeField] private Sprite achievementThumbnail;
    public Sprite AchievementThumbnail
    {
        get { return achievementThumbnail; }
    }

    /// <summary>
    /// 업적 달성 시간
    /// </summary>
    [SerializeField] private string _achievementTime;

    public string AchievementTime
    {
        get { return _achievementTime; }
        set { _achievementTime = value; }
    }

    /// <summary>
    /// 해당 업적이 연결되어 있는 업적 prefab
    /// </summary>
    private Achievement _achievementGameobject;
    public Achievement Achievement_GameObject
    {
        get { return _achievementGameobject; }
        set { _achievementGameobject = value; }
    }
    
    /// <summary>
    /// 업적 달성 시 호출
    /// </summary>
    /// <param name="amount">achievement의 nownum set</param>
    public bool Achieve(int amount)
    {
        NowNum += amount;
        if (MaxNum <= NowNum) // 달성했을때
        {
            NowNum = MaxNum;
            AchieveState = AchieveState.Achieved;
            AchievementTime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
            DatabaseManager.Instance.SaveAchievementInfo(SystemInfo.deviceUniqueIdentifier, id, nowNum ,achieveState, AchievementTime);
            return true;
        }
        DatabaseManager.Instance.SaveAchievementInfo(SystemInfo.deviceUniqueIdentifier, id, nowNum ,achieveState);
        return false;
    }
}