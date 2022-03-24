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

    private string _text;
    /// <summary>
    /// choice id
    /// </summary>
    public string id { get; set; }

    /// <summary>
    /// 각 Choice에 보여지는 Text
    /// </summary>
    public string text
    {
        get { return _text; }
        private set 
        {
            string[] val = value.Split('=');
            if (val.Length == 1) _text = val[0];
            else
            {
                _text = val[1];
                need = val[0].Split('&').ToList(); // need 구별
            }
            
        }
    }
    /// <summary>
    /// 각 Choice들을 선택하였을 때 이어지는 Script ID
    /// </summary>
    public string next { get; set; }

    /// <summary>
    /// 해당 Choice를 선택하기 위해 필요한 능력치 string List
    /// </summary>
    public List<string> need { get; private set; }
}
