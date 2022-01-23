using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private static Scroll instance = null;

    void Awake()
    {
        if(null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static Scroll Instance
    {
        get 
        { 
            if (null == instance) return null;
            return instance;

        }
    }

    public float pos;
    private float elapsedTime = 0;
    public bool IsScroll = false;
    private bool first = true;
    public GameObject content;
    [SerializeField] public float scrollAmount = 0f;

    Vector2 endpos;

    void Update()
    {
        if (IsScroll && GameObject.Find("Content").activeInHierarchy)
        {
            StartCoroutine(Scrollroutine());
        }
    }
    public IEnumerator Scrollroutine()
    {
        RectTransform set = content.gameObject.GetComponent<RectTransform>();
        yield return set.anchoredPosition += new Vector2(0, scrollAmount * 0.01f);
        if(set.anchoredPosition.y >= pos)
        {
            set.anchoredPosition.Set(set.anchoredPosition.x, pos);
            IsScroll = false;
            StopCoroutine(Scrollroutine());
        }
    }
    public void ScrollReset()
    {
        RectTransform set = content.gameObject.GetComponent<RectTransform>();
        set.localPosition = new Vector3(set.localPosition.x, 0);
    }
}