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

    private GameObject[] a = GameObject.FindGameObjectsWithTag("Acv");
    private GameObject[] p = GameObject.FindGameObjectsWithTag("percent");

    private int[] now;
    private int[] max;
    private int[] state;
    private string[] time;

    private string[] hide_name;
    private string[] hide_description;

    public Achivement()
    {
        now = new int [num];
        max = new int[num] {1,2,3,4,5,6,7,8};  //achive maximum value
        state = new int[num] {0,0,0,0,0,-1,0,0};      //-1: hide, 0 : express, 1 : complete
        time = new string[num];

        hide_name = new string[num];
        hide_description = new string[num];
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
    public void nowupdate(int n, int plus)
    {            //"n"th achivement "plus" value add
        if(this.now[n-1] < this.max[n-1])
        {
            this.now[n-1] += plus;

            if(this.now[n-1] >= this.max[n-1])
            {
                if(this.state[n-1]==-1){
                    express(n-1, this.a[n-1]);
                }
                this.state[n-1] = 1;
                this.now[n-1] = this.max[n-1];
                this.time[n-1] = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
            }
        }
    }
    //graph and text of percent
    public void percentupdate(int n, GameObject per, int now, int max)
    {
        float num = 0.0f;
        Image image = per.GetComponent<Image>();
        Text percent = per.transform.Find("percent").GetComponent<Text>();
        if(this.state[n]!=-1){
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
        for (int i = 0; i < num; i++)
        {
            if (this.state[i]!=-1)
            {
                percentupdate(i, this.p[i], this.now[i], this.max[i]);
                if (this.state[i] == 1)
                {
                    penelmax(this.a[i]);
                    dateupdate(this.p[i], this.time[i]);
                }
            }
            else{
                percentupdate(i, this.p[i], this.now[i], this.max[i]);
            }
        }
    }

    public void hide(int n, GameObject a){
        if(this.state[n] == -1){
            string _name = a.transform.Find("Acv_name").GetComponent<Text>().text;
            string _description = a.transform.Find("Acv_description").GetComponent<Text>().text;
            this.hide_name[n] = _name;
            this.hide_description[n] = _description;

            a.transform.Find("Acv_name").GetComponent<Text>().text = "???";
            a.transform.Find("Acv_description").GetComponent<Text>().text = "???";
        }
    }
    public void hide_save(){
        for(int i = 0; i<num; i++){
            hide(i, this.a[i]);
        }
    }

    public void express(int n, GameObject a){
        if(this.state[n] == -1){
            a.transform.Find("Acv_name").GetComponent<Text>().text = this.hide_name[n];
            a.transform.Find("Acv_description").GetComponent<Text>().text = this.hide_description[n];
        }
    }
}
