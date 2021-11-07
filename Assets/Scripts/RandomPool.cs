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
    public List<Script> RandomPool_List = new List<Script>();
    public int rand = -1;

    public void DeleteFromRandomPool(string _storyID) // BE 한번 발생한 이벤트 랜덤 풀에서 삭제
    {
        int index = 0;
        foreach (Script s in RandomPool_List)
        {
            if (s.id == _storyID)
            {
                RandomPool_List.RemoveAt(index);
                break;
            }
            index++;
        }
    }

}
