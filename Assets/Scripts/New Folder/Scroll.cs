using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float pos;
    private float elapsedTime = 0;
    public bool IsScroll = false;
    private bool first = true;


    void Update()
    {
        if (IsScroll)
        {
            scroll();
        }
    }
    public void scroll()
    {
        RectTransform set = gameObject.GetComponent<RectTransform>();
        while (IsScroll)
        {
            Vector3 nowpos = set.localPosition;
            Vector3 endpos;
            if (first)
            {
                endpos = new Vector3(392, pos, 0);
                first = false;
            }
            else
            {
                endpos = new Vector3(392 * 2, pos, 0);
            }
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / 3f;
            set.localPosition = Vector3.Lerp(nowpos, endpos, Mathf.SmoothStep(0, 1, percentageComplete));
            if (Vector3.Distance(set.localPosition, endpos) < 0.1f)
            {
                IsScroll = false;
            }
        }
        elapsedTime = 0;
    }
}

