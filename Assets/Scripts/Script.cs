using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Script
{


    public Script(string __id, string __text, List<string> __next, List<string> __result, string __sprite, List<string> __acvUpdate, int __interval, string __afterInterval = "-", int __probability = 100)

    {
        id = __id;
        text = __text;
        next = __next;
        result = __result;
        sprite = __sprite;
        interval = __interval;
        afterInterval = __afterInterval;
        probability = __probability;
        acvUpdate = __acvUpdate;

    }


    /// <summary>
    /// Script ID
    /// </summary>
    public string id { get; set; }

    /// <summary>
    /// Script 내용 - Text
    /// </summary>
    public string text { get; set; }

    /// <summary>
    /// Script 선택지 List
    /// </summary>
    public List<string> next { get; private set; }

    private void Awake() 
    {
        next = new List<string>();
    }
    /// <summary>
    /// Script가 나타나면 플레이어에게 주는 결과 값 - 스탯 변화
    /// </summary>
    public List<string> result { get; set; }

    /// <summary>
    /// 해당 Script와 같이 나타나는 sprite 이름 string
    /// </summary>
    public string sprite { get; set; }

    /// <summary>
    /// interval 존재 시: 랜덤 이벤트 진입 후 다시 메인 이벤트로 돌아올 간격 (interval 만큼의 SE script 후에 돌아옴)
    /// </summary>
    public int interval { get; set; }
    
    /// <summary>
    /// interval을 모두 지난 후 돌아올 Main Event의 id값
    /// </summary>
    public string afterInterval { get; set; }

    
    /// <summary>
    /// PME를 위한 확률형 Main Event
    /// </summary>
    public int probability { get; set; }


    /// <summary>
    /// Script가 나타나면 변화할 업적의 번지 수 및 업적의 값
    /// </summary>
    public List<string> acvUpdate { get; set; }

}
