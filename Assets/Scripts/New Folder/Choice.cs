using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Choice
{
    public Choice(string __id, string __text, string __next)
    {
        id = __id;
        text = __text;
        next = __next;
    }

    private string _id;
    private string _text;
    private string _next;
    private List<string> _need;

    public string id
    {
        get { return _id; }
        set { _id = value; }
    }
    public string text
    {
        get { return _text; }
        set 
        {
            string[] _val = value.Split('=');
            if (_val.Length == 1) _text = _val[0];
            else
            {
                _text = _val[1];
                _need = _val[0].Split('&').ToList();
            }
            
        }
    }
    public string next
    {
        get { return _next; }
        set { _next = value; }
    }

    public List<string> need
    {
        get { return _need; }
        set { _need = value; }
    }

}
