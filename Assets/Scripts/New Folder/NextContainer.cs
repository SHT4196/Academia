using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextContainer
{
    private static NextContainer instance;
    public static NextContainer Instance
    {
        get
        {
            if (instance == null) 
            { 
                instance = new NextContainer();
            }
            return instance;
        }
    }
    public NextContainer()
    {
        nextChoice = new List<string>();
        nextChoice.Add("m1_1");
        nextText = "M1_1";
    }

    public List<string> nextChoice;
    public string nextText;

}
