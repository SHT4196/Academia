using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNextID : MonoBehaviour
{
    
    public void Next()
    {
        if (gameObject.name == "RANDOM")
        {
            //Script next = MakeDialog.Instance.FindScript()
            if (MainEventController.Instance.GetInterval() > 0) //Random 풀 진입
            {
                MainEventController.Instance.IntervalDecrease();
                int rand = RandomPool.Instance.rand;
                int temp = rand;
                while (temp == rand) // 연속 같은 스토리 방지
                    temp = Random.Range(0, RandomPool.Instance.RandomPool_List.Count);
                RandomPool.Instance.rand = temp;
                NextContainer.Instance.nextText = RandomPool.Instance.RandomPool_List[temp].id;
            }
            else 
            {
                NextContainer.Instance.nextText = MainEventController.Instance.getNextME();
                MainEventController.Instance.SetME_str(NextContainer.Instance.nextText);
            }
        }
        else
        {
            if(gameObject.name[0].Equals('M'))
            {
                MainEventController.Instance.SetME_str(gameObject.name);
                MainEventController.Instance.FirstSetInterval();
            }
            NextContainer.Instance.nextText = gameObject.name;
        }
    }
    
}
