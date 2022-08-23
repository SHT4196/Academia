using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNextID : MonoBehaviour
{
    /// <summary>
    /// 다음 Script 설정
    /// </summary>
    public void Next()
    {
        if (gameObject.name == "RANDOM") // 랜덤이벤트일 경우
        {
            if(PlayerPrefs.GetInt("ME_interval") != 0 && !Player.instance.isAdmin)
                MainEventController.instance.SetInterval(PlayerPrefs.GetInt("ME_interval"));
            if (MainEventController.instance.GetInterval() > 0) //Random 풀 진입
            {
                MainEventController.instance.IntervalDecrease();
                int rand = RandomPool.Instance.Rand;
                int temp = rand;
                while (temp == rand) // 연속 같은 스토리 방지
                    temp = Random.Range(0, RandomPool.Instance.RandomPoolList.Count);
                RandomPool.Instance.Rand = temp;
                NextContainer.instance.NextText = RandomPool.Instance.RandomPoolList[temp].id;
                if (Player.instance.isAdmin)
                {
                    return;
                }
                PlayerPrefs.SetInt("ME_interval", MainEventController.instance.GetInterval());
            }
            else 
            {
                NextContainer.instance.NextText = MainEventController.instance.GetNextMe();
                MainEventController.instance.SetME_str(NextContainer.instance.NextText);
            }
        }
        else // 랜덤이벤트가 아닐 경우
        {
            if(gameObject.name[0].Equals('M')) // 메인 이벤트인 경우
            {
                MainEventController.instance.SetME_str(gameObject.name);
                if (!Player.instance.isAdmin)
                {
                    PlayerPrefs.SetString("ME_str", gameObject.name);
                }
                MainEventController.instance.FirstSetInterval();
            }
            NextContainer.instance.NextText = gameObject.name;
        }
    }
    
}
