using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Scroll : MonoBehaviour
{
    private static Scroll _instance = null;

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
    [SerializeField] public float scrollAmount;

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
        yield return set.anchoredPosition += new Vector2(0, scrollAmount * 0.01f);
        // Debug.Log($"content: {set.anchoredPosition.y}, pos: {pos}");
        if(set.anchoredPosition.y >= pos)
        {
            set.anchoredPosition.Set(set.anchoredPosition.x, pos);
            isScroll = false;
            StopCoroutine(Scrollroutine());
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