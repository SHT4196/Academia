using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivementManager : MonoBehaviour
{  
    void Start() {
        Achivement.Acv.hide_save();
        Achivement.Acv.nowupdate(5,2);
        Achivement.Acv.nowupdate(3, 3);
        Achivement.Acv._debug();
    }
    void Update() {
        Achivement.Acv.drawpenel();
    }
}
