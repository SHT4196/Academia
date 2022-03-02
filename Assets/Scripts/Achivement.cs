using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achivement
{
    private static Achivement acv;
    
    public static Achivement Acv{
        get{
            if (null == acv)
                acv = new Achivement();
            return acv;
        }
    }

    private const int num = 8;     //number of achivement

    private static GameObject[] a;
    private static GameObject[] p;
    
    private static bool scene_change;

    private static int[] now;
    private static int[] max;
    private static int[] state;
    private static string[] time;

    private static string[] hide_name;
    private static string[] hide_description;

    public Achivement()
    {
        now = new int [num];
        max = new int[num] {1,2,3,4,5,6,7,8};  //achive maximum value
        state = new int[num] {0,0,0,0,0,-1,0,0};      //-1: hide, 0 : express, 1 : complete
        time = new string[num];

        hide_name = new string[num];
        hide_description = new string[num];

        scene_change = false;
        load_acv(); 
        get_variable();
    }

    public void _debug()
    {
        if(a.Length != num){
            Debug.Log("Warning! 업적의 총 수를 확인하세요!");
        }
        if(p.Length != num){
            Debug.Log("Warning! 업적의 총 수를 확인하세요!");
        }
    }

    public void get_variable()
    {
        a = GameObject.FindGameObjectsWithTag("Acv");
        p = GameObject.FindGameObjectsWithTag("percent");
    }
    
    public void change_scene()
    {
        scene_change = true;
    }
    public void nowupdate(int n, int plus)
    {            //"n"th achivement "plus" value add
        if(now[n-1] < max[n-1])
        {
            now[n-1] += plus;

            if(now[n-1] >= max[n-1])
            {
                if(state[n-1]==-1){
                    express(n-1, a[n-1]);
                }
                state[n-1] = 1;
                now[n-1] = max[n-1];
                time[n-1] = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
            }
            save_acv();
        }
    }
    //graph and text of percent
    public void percentupdate(int n, GameObject per, int now, int max)
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
    //when achivement clear
    public void penelmax(GameObject acv)
    {
        Image i = acv.GetComponent<Image>();
        i.color = new Color(0,0,0,255);
    }
    public void dateupdate(GameObject p, string d)
    {
        p.transform.Find("time").gameObject.SetActive(true);
        Text date = p.transform.Find("time").GetComponent<Text>();
        date.text = d;
    }
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
            if (state[i]!=-1)
            {
                percentupdate(i, p[i], now[i], max[i]);
                if (state[i] == 1)
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

    public void hide(int n, GameObject a){
        if(state[n] == -1)
        {
            if (!scene_change)
            {
                Text[] texts;
                texts = a.transform.GetComponentsInChildren<Text>();
                string _name = texts[0].text;
                string _description = texts[1].text;
                // string _name = a.transform.GetComponentInChildren<Text>().text;
                // string _description = a.transform.Find("Acv_description").GetComponent<Text>().text;
                hide_name[n] = _name;
                hide_description[n] = _description;
            }

            a.transform.GetComponentsInChildren<Text>()[0].text = "???";
            a.transform.GetComponentsInChildren<Text>()[1].text = "???";
            // a.transform.Find("Acv_name").GetComponent<Text>().text = "???";
            // a.transform.Find("Acv_description").GetComponent<Text>().text = "???";
        }
    }
    public void hide_save(){
        for(int i = 0; i < num; i++){
            hide(i, a[i]);
        }
    }

    public void express(int n, GameObject a){
        if(state[n] == -1){
            a.transform.Find("Acv_name").GetComponent<Text>().text = hide_name[n];
            a.transform.Find("Acv_description").GetComponent<Text>().text = hide_description[n];
        }
    }

    public void save_acv()
    {
        PlayerPrefs.SetInt("is_save", 1);
        for (int i = 0; i < num; i++)
        {
            PlayerPrefs.SetInt("now_val" + i.ToString(), now[i]);
            PlayerPrefs.SetInt("max_val" + i.ToString(), max[i]);
            PlayerPrefs.SetInt("state_val" + i.ToString(), state[i]);
            PlayerPrefs.SetString("clear_time" + i.ToString(), time[i]);
            PlayerPrefs.SetString("hide_name" + i.ToString(), hide_name[i]);
            PlayerPrefs.SetString("hide_description" + i.ToString(), hide_description[i]);
        }
    }

    public void load_acv()
    {
        if (PlayerPrefs.GetInt("is_save") == 1)
        {
            for(int i = 0; i < num; i++)
            {
                now[i] = PlayerPrefs.GetInt("now_val" + i.ToString());
                max[i] = PlayerPrefs.GetInt("max_val" + i.ToString());
                state[i] = PlayerPrefs.GetInt("state_val" + i.ToString());
                time[i] = PlayerPrefs.GetString("clear_time" + i.ToString());
                hide_name[i] = PlayerPrefs.GetString("hide_name" + i.ToString());
                hide_description[i] = PlayerPrefs.GetString("hide_description" + i.ToString());
            }
        }
    }
}
