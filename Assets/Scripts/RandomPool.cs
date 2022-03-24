using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPool
{
    private static RandomPool instance;

    public static RandomPool Instance
    {
        get
        {
            if (instance == null)
                instance = new RandomPool();
            return instance;
        }
    }
    /// <summary>
    /// RandomPool에 들어가 있는 Script List
    /// </summary>
    public readonly List<Script> RandomPoolList = new List<Script>();
    
    /// <summary>
    /// random 시 중복 이벤트 방지를 위한 변수
    /// </summary>
    public int Rand = -1;

    /// <summary>
    /// BE 한번 발생한 이벤트 랜덤 풀에서 삭제
    /// </summary>
    /// <param name="storyID">BE 스토리 id</param>
    public void DeleteFromRandomPool(string storyID) 
    {
        int index = 0;
        foreach (Script s in RandomPoolList)
        {
            if (s.id == storyID)
            {
                RandomPoolList.RemoveAt(index);
                break;
            }
            index++;
        }
    }

}
