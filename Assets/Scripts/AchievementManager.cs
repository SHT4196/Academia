using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            InitialAchievementObjects();
        }
        else
        {
            Destroy(this.gameObject);
        }
        
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
        GameObject alarmGameObject = Instantiate(achieveAlarm);
        alarmGameObject.GetComponentInChildren<TextMeshProUGUI>().text = $"{achieveName} - 업적을 달성하였습니다.";
        Destroy(alarmGameObject, 1.5f);
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
    
    /*
    public const int num = 8;
    private static bool is_first = true;
    private static int n = 0;

    /// <summary>
    /// 업적 달성 시 동작
    /// </summary>
    public static void acv_clear(int n)
    {
        if (StaticCoroutine.is_play == true)  //동시에 여러개의 업적 달성될 경우 대응
        {
            Achivement.is_fail = true;
            return;
        }
        GameObject acv = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefab/Acv"));
        RectTransform acv_rect = acv.GetComponent<RectTransform>();
        float acv_height = 172.5f;
        float CanvasHeight;
        try
        {
            acv.transform.SetParent(GameObject.Find("Canvas").transform, true);
        }
        catch
        {
            acv.transform.SetParent(GameObject.Find("MainCanvas").transform, true);
        }
        try
        {
            CanvasHeight = GameObject.Find("MainCanvas").GetComponent<RectTransform>().rect.height;
        }
        catch
        {
            CanvasHeight = GameObject.Find("Canvas").GetComponent<Rect Transform>().rect.height;
        }
        acv_rect.localScale = Vector3.one;
        acv_rect.sizeDelta = new Vector2(0, acv_height);
        acv_rect.anchoredPosition = new Vector2(0, CanvasHeight * 0.5f + acv_height * 0.5f);
        acv.transform.Find("Acv_pic").transform.Find("Acv_name").GetComponent<Text>().text = "업적 달성!";
        acv.transform.Find("Acv_pic").transform.Find("Acv_description").GetComponent<Text>().text = Achivement.name[n];
        acv.transform.Find("Acv_pic").GetComponent<Image>().sprite = Achivement.image[n];
        Destroy(acv.transform.Find("Acv_percent").gameObject);

        StaticCoroutine.DoCoroutine(AchivementManager.ClearAction(acv, acv_height, acv_rect));

    }

    /// <summary>
    /// 코루틴 동작
    /// </summary>
    public static IEnumerator ClearAction(GameObject acv, float acv_height, RectTransform acv_rect)
    {
        StaticCoroutine.is_play = true;
        int velocity = 60;  //업적 클리어창 내려오는 속도
        while (n < velocity)
        {
            n++;
            yield return acv_rect.anchoredPosition -= new Vector2(0, acv_height/velocity);
        }
        n = 0;
        yield return new WaitForSeconds(1f);
        while (n < velocity)
        {
            n++;
            yield return acv_rect.anchoredPosition += new Vector2(0, acv_height/velocity);
        }
        n = 0;
        Destroy(acv);
        StaticCoroutine.is_play = false;
    }

    void Start() {
        /// <summary>
        /// 처음 main_menu 씬에 들어왔을 때의 동작
        /// </summary>
        if (is_first)
        {
            Achivement.Acv.hide_save();  //Save the information about the achievements that you want to hide
            if (PlayerPrefs.GetInt("is_save") != 1)  //check if achivement information is saved
            {
                Achivement.Acv.nowupdate(5,2);
                Achivement.Acv.nowupdate(3, 3);
            }
            Achivement.Acv._debug();
            is_first = false;
        }
    }

    /// <summary>
    /// penel에 표시되는 값 갱신
    /// </summary>
    void Update() {
        if(Achivement.Acv != null)
        {
            Achivement.Acv.drawpenel();
        }
    }
    */
}
