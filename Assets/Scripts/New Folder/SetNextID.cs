using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNextID : MonoBehaviour
{
    
    public void Next()
    {
        if(gameObject.name == "RANDOM")
        {
            int rand = RandomPool.Instance.rand;
            int temp = rand;
            while(temp == rand) // 연속 같은 스토리 방지
                temp = Random.Range(0, RandomPool.Instance.RandomPool_List.Count);
            RandomPool.Instance.rand = temp;
            NextContainer.Instance.nextText = RandomPool.Instance.RandomPool_List[temp].id;
        }
        else
            NextContainer.Instance.nextText = gameObject.name;
    }
    
}
