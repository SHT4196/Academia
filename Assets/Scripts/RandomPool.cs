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
    /// RandomPool�� �� �ִ� Script List
    /// </summary>
    public readonly List<Script> RandomPoolList = new List<Script>();
    
    /// <summary>
    /// random �� �ߺ� �̺�Ʈ ������ ���� ����
    /// </summary>
    public int Rand = -1;

    /// <summary>
    /// BE �ѹ� �߻��� �̺�Ʈ ���� Ǯ���� ����
    /// </summary>
    /// <param name="storyID">BE ���丮 id</param>
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
