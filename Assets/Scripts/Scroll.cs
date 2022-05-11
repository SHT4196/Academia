using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Scroll : MonoBehaviour
{
    private static Scroll _instance = null;
    /// <summary>
    /// 스크롤 되는 총량
    /// </summary>
    private static float scrollquantity = 0f;
    /// <summary>
    /// 코루틴이 실행된 횟수
    /// </summary>
    private static int time = 0;
    /// <summary>
    /// 스크롤의 속도 (시간 개념이므로 값이 클수록 느려짐)
    /// </summary>
    [SerializeField] private static int ScrollTime = 40;

    void Awake()
    {
        if(null == _instance)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            Scroll._instance.InitScrollManager();
        }
    }
    public static Scroll instance
    {
        get 
        { 
            if (null == _instance) return null;
            return _instance;

        }
    }

    public float pos;
    [FormerlySerializedAs("_elapsedTime")] [SerializeField] private float elapsedTime = 0;
    [FormerlySerializedAs("IsScroll")] public bool isScroll = false;
    [FormerlySerializedAs("_first")] [SerializeField] private bool first = true;
    public GameObject content;
    [SerializeField] private float scrollAmount;

    Vector2 endpos;

    void Update()
    {
        if(content == null)
            content = GameObject.Find("Content");
        if (isScroll && GameObject.Find("Content").activeInHierarchy)
        {
            StartCoroutine(Scrollroutine());
        }
    }
    /// <summary>
    /// 스크롤 함수
    /// </summary>
    /// <returns></returns>
    public IEnumerator Scrollroutine()
    {
        RectTransform set = content.gameObject.GetComponent<RectTransform>();
        if (time == 0)
        {
            scrollquantity = pos - set.anchoredPosition.y;
        }
        time++;
        scrollAmount = scrollquantity/(Mathf.Pow(ScrollTime, 2)) * (Mathf.Pow(time - ScrollTime - 1, 2) - Mathf.Pow(time - ScrollTime, 2));
        // Debug.Log($"content: {set.anchoredPosition.y}, pos: {pos}");
        if(time >= ScrollTime + 1)
        {
            if (set.anchoredPosition.y < pos)
            {
                yield return set.anchoredPosition += new Vector2(0, Mathf.Abs(pos - set.anchoredPosition.y));
            }
            time = 0;
            isScroll = false;
            StopCoroutine(Scrollroutine());
        }
        else
        {
            yield return set.anchoredPosition += new Vector2(0, scrollAmount);
        }
    }
    /// <summary>
    /// 스크립트가 새로 생성될 때 실행
    /// </summary>
    public void ScrollReset()
    {
        RectTransform set = content.gameObject.GetComponent<RectTransform>();
        set.localPosition = new Vector3(set.localPosition.x, 0);
    }
    /// <summary>
    /// ScrollManager 초기화
    /// </summary>
    public void InitScrollManager()
    {
        content = GameObject.Find("Content");
    }
}