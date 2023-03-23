using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// 업적 singleton class
/// </summary>
public class Achievement: MonoBehaviour
{
    [SerializeField] public AchievementObject achievementObject;

    [SerializeField] public Text acvName;

    [SerializeField] private Text acvDesc;

    [SerializeField] private Image acvThumbnail;

    [SerializeField] private Text acvPercent;

    [SerializeField] private Image acvPercentageImg;
    
    [SerializeField] private Text acvTime;

    [SerializeField] private Sprite hiddenAcvThumbnail;

    private void Start()
    {
        SetAchivementView();
        Debug.Log(" SetAchivementView");
    }

    /// <summary>
    /// 각 업적의 상태값들을 조정해줌 (이름, 설명, 썸네일, 퍼센트, 상태 등)
    /// </summary>
    public void SetAchivementView()
    {
        if (achievementObject != null)
        {
            if (achievementObject.AchieveState != AchieveState.Hidden)
            {
                if (achievementObject.AchieveState == AchieveState.NotAchieved) 
                {
                    acvName.text = achievementObject.AchievementName;
                    acvDesc.text = achievementObject.AchievementDesc;
                    acvThumbnail.sprite = achievementObject.AchievementThumbnail;
                    acvPercent.text = $"{achievementObject.NowNum} / {achievementObject.MaxNum}";
                    acvPercentageImg.fillAmount = (float)achievementObject.NowNum / achievementObject.MaxNum;
                    
                }
                
                else if (achievementObject.AchieveState == AchieveState.Achieved)
                {
                    acvName.text = achievementObject.AchievementName;
                    acvDesc.text = achievementObject.AchievementDesc;
                    acvThumbnail.sprite = achievementObject.AchievementThumbnail;
                    acvPercentageImg.fillAmount = (float)achievementObject.NowNum / achievementObject.MaxNum;
                    acvPercent.text = "Complete!";
                    acvTime.text = achievementObject.AchievementTime;
                    
                }
            }
            else
            {
                acvName.text = "???";
                acvDesc.text = "???";
                acvThumbnail.sprite = hiddenAcvThumbnail;
                acvPercent.text = "???";
                acvPercentageImg.fillAmount = 0;
            }
        }
    }


    /// <summary>
    /// 업적 저장, 저장 여부는 is_save를 통해 판단
    /// </summary>
    public void SaveAcv()
    {
        PlayerPrefs.SetInt("is_save", 1);
        PlayerPrefs.SetInt("now_val" + achievementObject.Id, achievementObject.NowNum);
        PlayerPrefs.SetInt("max_val" + achievementObject.Id, achievementObject.MaxNum);
        PlayerPrefs.SetString("state_val" + achievementObject.Id, achievementObject.AchieveState.ToString());
        PlayerPrefs.SetString("clear_time" + achievementObject.Id, achievementObject.AchievementTime);
        // PlayerPrefs.SetString("hide_name" + i.ToString(), hide_name[i]);
        // PlayerPrefs.SetString("hide_description" + i.ToString(), hide_description[i]);
    }

    /// <summary>
    /// 저장된 업적이 있을 경우 업적 불러옴
    /// </summary>
    public void LoadAcv()
    {
        if (PlayerPrefs.GetInt("is_save") == 1)
        {
            achievementObject.NowNum = PlayerPrefs.GetInt("now_val" + achievementObject.Id);
            achievementObject.MaxNum = PlayerPrefs.GetInt("max_val" + achievementObject.Id);
            if (PlayerPrefs.GetString("state_val" + achievementObject.Id) == "Hidden")
            {
                achievementObject.AchieveState = AchieveState.Hidden;
            }
            else if (PlayerPrefs.GetString("state_val" + achievementObject.Id) == "NotAchieved")
            {
                achievementObject.AchieveState = AchieveState.NotAchieved;
            }
            else if (PlayerPrefs.GetString("state_val" + achievementObject.Id) == "Achieved")
            {
                achievementObject.AchieveState = AchieveState.Achieved;
            }
            achievementObject.AchievementTime = PlayerPrefs.GetString("clear_time" + achievementObject.Id);
        }
    }


    /*
    private static Achivement acv;
    
    public static Achivement Acv{
        get{
            if (null == acv)
                acv = new Achivement();
            return acv;
        }
    }
    /// <summary>
    /// 업적의 총 개수
    /// </summary>
    private const int num = 8;
    /// <summary>
    /// 업적의 prefab array
    /// </summary>    
    private static GameObject[] a;
    /// <summary>
    /// 업적 prefab 중 퍼센트를 담당하는 부분 array
    /// </summary>
    private static GameObject[] p;
    /// <summary>
    /// MainMenu에서 Scene이 전환되었는지 여부
    /// </summary>
    private static bool scene_change;
    /// <summary>
    /// 업적의 현재값 array
    /// </summary>
    private static int[] now;
    /// <summary>
    /// 업적의 최댓값(달성값) array
    /// </summary>
    private static int[] max;
    /// <summary>
    /// 업적의 상태의 초기값
    /// </summary>
    private static int[] init_state;
    /// <summary>
    /// 업적의 현재 상태 array --> -1: 숨겨진 업적, 0: 달성되지 않고 숨겨지지 않은 업적, 1: 달성된 업적
    /// </summary>
    private static int[] state;
    /// <summary>
    /// 업적 달성 시간 array
    /// </summary>
    private static string[] time;
    /// <summary>
    /// 숨겨진 업적의 이름 -> ???로 표시하기 위함
    /// </summary>
    private static string[] hide_name;
    /// <summary>
    /// 숨겨진 업적의 설명 -> ???로 표시하기 위함
    /// </summary>
    private static string[] hide_description;
    /// <summary>
    /// 업적의 이름 저장
    /// </summary>
    public static string[] name;
    /// <summary>
    /// 업적의 그림 저장
    /// </summary>
    public static Sprite[] image;
    /// <summary>
    /// 함수 실행 성공 여부
    /// </summary>
    public static bool is_fail = false;
    /// <summary>
    /// 업적 코루틴 실행 대기열
    /// </summary>
    public static List<int> acv_delay = new List<int>();

    public Achivement()
    {
        /// <summary>
        /// 각 업적의 최댓값 및 상태 설정
        /// </summary>
        now = new int [num];
        max = new int[num] {1,2,3,4,5,6,7,8};
        state = new int[num] {0,0,0,0,0,-1,0,0};
        time = new string[num];
        hide_name = new string[num];
        hide_description = new string[num];
        scene_change = false;
        name = new string[num];
        image = new Sprite[num];

        init_state = state;

        /// <summary>
        /// 저장된 업적 불러옴
        /// </summary>
        load_acv();
        /// <summary>
        /// 씬에 존재하는 prefab들 불러옴
        /// </summary>
        get_variable();
    }

    /// <summary>
    /// 지정한 업적 수와 실제 업적 수 비교
    /// </summary>
    public void _debug()
    {
        if(a.Length != num){
            Debug.Log("Warning! 업적의 총 수를 확인하세요!");
        }
        if(p.Length != num){
            Debug.Log("Warning! 업적의 총 수를 확인하세요!");
        }
    }

    /// <summary>
    /// 씬에 존재하는 업적 prefab을 변수에 저장
    /// </summary>
    public void get_variable()
    {
        a = GameObject.FindGameObjectsWithTag("Acv");
        p = GameObject.FindGameObjectsWithTag("percent");
        for (int i = 0; i < num; i++)
        {
            name[i] = a[i].transform.Find("Acv_pic").transform.Find("Acv_name").GetComponent<Text>().text;
            image[i] = a[i].transform.Find("Acv_pic").GetComponent<Image>().sprite;
        }
    }

    /// <summary>
    /// 씬 전환시 scene_change 바꿈
    /// </summary>
    public void change_scene()
    {
        scene_change = true;
    }

    /// <summary>
    /// 지정한 업적의 now값 update 및 업적 저장
    /// </summary>
    /// <param name="n">값을 바꿀 n번째 업적</param>
    /// <param name="plus">더할 값</param>
    public void nowupdate(int n, int plus)
    {
        if(now[n-1] < max[n-1])
        {
            now[n-1] += plus;

            if(now[n-1] >= max[n-1])
            {
                state[n-1] = 1;
                now[n-1] = max[n-1];
                time[n-1] = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
                AchivementManager.acv_clear(n-1);
                if (is_fail)
                {
                    acv_delay.Add(n-1);
                    Debug.Log(acv_delay[0]);
                    is_fail = false;
                }
            }
            else if(now[n-1] < 0)
            {
                now[n-1] = 0;
            }
            Debug.Log((n) + "번째 업적에 " + (plus) + " 더함 --> 결과 : " + (now[n-1]));
            save_acv();
        }
    }

    /// <summary>
    /// 업적 달성 정도에 따른 percent값 계산 및 표시
    /// </summary>
    /// <param name="n">percent값을 표시할 n번째 업적</param>
    /// <param name="per">값을 표시할 GameObject</param>
    /// <param name="now">n번째 업적의 now값</param>
    /// <param name="max">n번째 업적의 max값</param>
    private void percentupdate(int n, GameObject per, int now, int max)
    {
        float num = 0.0f;
        Image image = per.GetComponent<Image>();
        Text percent = per.transform.Find("percent").GetComponent<Text>();
        if(state[n]!=-1){
            num = (now * 1.0f) / max;
            image.fillAmount = num;
            percent.text = now + " / " + max;
        }
        else {
            image.fillAmount = 0.0f;
            percent.text = "???";
        }
    }

    /// <summary>
    /// 업적 달성시 업적 prefab의 색 변화
    /// </summary>
    /// <param name="acv">색을 바꿀 업적 prefab</param>
    private void penelmax(GameObject acv)
    {
        Image i = acv.GetComponent<Image>();
        i.color = new Color(0,0,0,255);
    }

    /// <summary>
    /// 업적 달성시 업적 달성한 시간 표시
    /// </summary>
    /// <param name="p">시간을 표시시킬 percent prefab</param>
    /// <param name="d">업적을 달성한 시간</param>
    private void dateupdate(GameObject p, string d)
    {
        p.transform.Find("time").gameObject.SetActive(true);
        Text date = p.transform.Find("time").GetComponent<Text>();
        date.text = d;
    }

    /// <summary>
    /// 업적에 관련된 값들을 penel에 실시간으로 표시
    /// </summary>
    public void drawpenel()
    {
        if (scene_change)
        {
            get_variable();
            hide_save();
            save_acv();
            scene_change = false;
        }
        for (int i = 0; i < num; i++)
        {
            if (state[i] == 1 && init_state[i] == -1)
                express(i, a[i]);
            if (state[i]!=-1)  //check if (i)th achivement is hidden
            {
                percentupdate(i, p[i], now[i], max[i]);
                if (state[i] == 1)  //check if (i)th achivement is cleared
                {
                    penelmax(a[i]);
                    dateupdate(p[i], time[i]);
                }
            }
            else{
                percentupdate(i, p[i], now[i], max[i]);
            }
        }
    }

    /// <summary>
    /// 숨길 업적의 이름과 설명 저장 및 ???로 대체
    /// </summary>
    /// <param name="n">숨길 n번째 업적</param>
    /// <param name="a">숨길 업적 prefab</param>
    private void hide(int n, GameObject a)
    {
        if(state[n] == -1)
        {
            if (!scene_change)
            {
                Text[] texts;
                texts = a.transform.GetComponentsInChildren<Text>();
                string _name = texts[0].text;
                string _description = texts[1].text;
                hide_name[n] = _name;
                hide_description[n] = _description;
            }

            a.transform.GetComponentsInChildren<Text>()[0].text = "???";
            a.transform.GetComponentsInChildren<Text>()[1].text = "???";
        }
    }

    /// <summary>
    /// state가 -1인 모든 업적에 hide 적용
    /// </summary>
    public void hide_save()
    {
        for(int i = 0; i < num; i++)
        {
            hide(i, a[i]);
        }
    }

    /// <summary>
    /// 숨겨진 업적이 달성되었을 때 업적 이름 및 설명 표시
    /// </summary>
    /// <param name="n">표시할 n번째 업적</param>
    /// <param name="a">표시할 업적 prefab</param>
    public void express(int n, GameObject a)
    {
        a.transform.Find("Acv_name").GetComponent<Text>().text = hide_name[n];
        a.transform.Find("Acv_description").GetComponent<Text>().text = hide_description[n];
    }


}

public class StaticCoroutine : MonoBehaviour 
{
 
    private static StaticCoroutine mInstance = null;
    public static bool is_play = false;
 
    private static StaticCoroutine instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = GameObject.FindObjectOfType(typeof(StaticCoroutine)) as StaticCoroutine;
 
                if (mInstance == null)
                {
                    mInstance = new GameObject("StaticCoroutine").AddComponent<StaticCoroutine>();
                }
            }
            return mInstance;
        }
    }
 
    void Awake()
    {
        if (mInstance == null)
        {
            mInstance = this as StaticCoroutine;
        }
    }
 
    IEnumerator Perform(IEnumerator coroutine)
    {
        yield return StartCoroutine(coroutine);
        Die();
    }
 
    public static void DoCoroutine(IEnumerator coroutine)
    {
        instance.StartCoroutine(instance.Perform(coroutine));    
    }
    
    void Die()
    {
        mInstance = null;
        Destroy(gameObject);
    }
 
    void OnApplicationQuit()
    {
        mInstance = null;
    }

    void Update() 
    {
        if (is_play == false && Achivement.acv_delay.Count >= 1)
        {
            AchivementManager.acv_clear(Achivement.acv_delay[0]);
            Achivement.acv_delay.RemoveAt(0);
        }
    }
    */
}
