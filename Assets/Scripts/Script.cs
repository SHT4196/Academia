using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Script
{

    public Script(string __id, string __text, List<string> __next, List<string> __result, string __sprite, int __interval)

    {
        id = __id;
        text = __text;
        next = __next;
        result = __result;
        sprite = __sprite;
        interval = __interval;

    }

    private string _id;
    private string _text;
    private List<string> _next;
    private List<string> _result;

    private string _sprite;

    private int _interval;

    private string _ME_save;


    public string id
    {
        get { return _id; }
        set { _id = value; }
    }
    public string text
    {
        get { return _text; }
        set { _text = value; }
    }
    public List<string> next
    {
        get { return _next; }
        set { _next = value; }
    }

    private void Awake() 
    {
        _next = new List<string>();
    }
    public List<string> result
    {
        get { return _result; }
        set { _result = value; }
    }

    public string sprite
    {
        get { return _sprite; }
        set { _sprite = value; }
    }

    public int interval
    {
        get { return _interval; }
        set { _interval = value; }

    }
}
