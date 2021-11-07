using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Image health_img;
    public Image mental_img;
    public Image money_img;
    public Sprite three;
    public Sprite two;
    public Sprite one;
    public Sprite zero;
    private Sprite spr;

    private Sprite[,] arr_img = new Sprite[3, 4];
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            spr = Resources.Load<Sprite>("Images/health" + i.ToString()) as Sprite;
            arr_img[0, i] = spr;
        }
        for (int i = 0; i < 4; i++)
        {
            spr = Resources.Load<Sprite>("Images/mental" + i.ToString()) as Sprite;
            arr_img[1, i] = spr;
        }
        for (int i = 0; i < 4; i++)
        {
            spr = Resources.Load<Sprite>("Images/money" + i.ToString()) as Sprite;
            arr_img[2, i] = spr;
        }
        imgSet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void imgChange(int state, int value) // Change Images by state & value
    {
        //state 0: health, 1: mental, 2: money
        if (state == 0)
            health_img.sprite = arr_img[0, value];
        else if (state == 1)
            mental_img.sprite = arr_img[1, value];
        else if (state == 2)
            money_img.sprite = arr_img[2, value];
    }
    public void imgSet()
    {
        health_img.sprite = arr_img[0, 3];
        mental_img.sprite = arr_img[1, 3];
        money_img.sprite = arr_img[2, 2];
    }
}

