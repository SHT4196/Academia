using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivementManager : MonoBehaviour
{
    private static bool is_first = true;  
    void Start() {
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
    void Update() {
        if(Achivement.Acv != null)
        {
            Achivement.Acv.drawpenel();
        }
    }
}
