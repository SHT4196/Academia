using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    private static AchievementManager instance = null;
    

    public static AchievementManager Instance
    {
        get
        {
            if (null == instance)
                return null;
            return instance;
        }
    }
    
    [SerializeField] private List<Achievement> achievements;
    private List<AchievementObject> _achievementObjects;
    [SerializeField] private GameObject achieveAlarm;
    [FormerlySerializedAs("AlarmElement")] [SerializeField] private GameObject alarmElement;
    private GameObject alarmGameObject;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            AchievementSyncDB();
            InitialAchievementObjects();
            
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    void Start() 
    {
        
        AchievementManager.Instance.UpdateAchievements();
        Debug.Log("업적 최신화");

    }

    /// <summary>
    /// Firebase Database와 동기화 (게임 시작 시 실행)
    /// </summary>
    public void AchievementSyncDB()
    {
        DatabaseManager.Instance.Reference.Child(SystemInfo.deviceUniqueIdentifier).Child("Achievement").GetValueAsync()
            .ContinueWithOnMainThread(
                task =>
                {
                    if (task.IsFaulted)
                    {
                        Debug.Log($"업적 정보를 불러오는 것을 실패했습니다. : {task.Exception}");
                    }
                    else if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;
                        if (snapshot != null)
                        {
                            foreach (var data in snapshot.Children)
                            {
                                int index = Int32.Parse(data.Key);
                                switch (data.Child("AchieveState").Value)
                                {
                                    case "Achieved":
                                        _achievementObjects[index].AchieveState = AchieveState.Achieved;
                                        break;
                                    case "Hidden":
                                        _achievementObjects[index].AchieveState = AchieveState.Hidden;
                                        break;
                                    case "NotAchieved":
                                        _achievementObjects[index].AchieveState = AchieveState.NotAchieved;
                                        break;
                                }
                                _achievementObjects[index].NowNum = Int32.Parse(data.Child("nowNum").Value.ToString());
                                _achievementObjects[index].AchievementTime = data.Child("timeStamp").Value.ToString();
                            }
                        }
                    }
                });
    }

    /// <summary>
    /// 업적 달성
    /// </summary>
    /// <param name="achievementId">달성하고자 하는 업적의 id</param>
    /// <param name="amount">nowNum 증가 시킬 value</param>
    public void Achieve_achievement(int achievementId, int amount)
    {
        bool checkAchieved = _achievementObjects[achievementId].Achieve(amount);
        if (checkAchieved)
        {
            AchieveAlarm(_achievementObjects[achievementId].AchievementName);
        }
    }

    public void AchieveAlarm(string achieveName)
    {
        if (alarmGameObject != null)
        {
            GameObject alarmElem = Instantiate(alarmElement, alarmGameObject.GetComponentInChildren<VerticalLayoutGroup>().transform);
            alarmElem.GetComponentInChildren<TextMeshProUGUI>().text = $"{achieveName} - 업적을 달성하였습니다.";
            Destroy(alarmElem, 1.5f);
        }
        else
        {
            alarmGameObject = Instantiate(achieveAlarm);
            GameObject alarmElem = Instantiate(alarmElement, alarmGameObject.GetComponentInChildren<VerticalLayoutGroup>().transform);
            alarmElem.GetComponentInChildren<TextMeshProUGUI>().text = $"{achieveName} - 업적을 달성하였습니다.";
            Destroy(alarmElem, 1.5f);
        }
    }

    
    public void InitialAchievementObjects()
    {
        _achievementObjects = new List<AchievementObject>();
        foreach (var element in achievements)
        {
            _achievementObjects.Add(element.achievementObject);
            element.achievementObject.Achievement_GameObject = element;
        }
    }

    public void UpdateAchievements()
    {
        foreach (var element in achievements)
        {
            element.SetAchivementView();
        }
    }
}
