using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNextID : MonoBehaviour
{
    /// <summary>
    /// ���� Script ����
    /// </summary>
    public void Next()
    {
        if (gameObject.name == "RANDOM") // �����̺�Ʈ�� ���
        {
            if(PlayerPrefs.GetInt("ME_interval") != 0)
                MainEventController.instance.SetInterval(PlayerPrefs.GetInt("ME_interval"));
            if (MainEventController.instance.GetInterval() > 0) //Random Ǯ ����
            {
                MainEventController.instance.IntervalDecrease();
                int rand = RandomPool.Instance.Rand;
                int temp = rand;
                while (temp == rand) // ���� ���� ���丮 ����
                    temp = Random.Range(0, RandomPool.Instance.RandomPoolList.Count);
                RandomPool.Instance.Rand = temp;
                NextContainer.instance.NextText = RandomPool.Instance.RandomPoolList[temp].id;
                PlayerPrefs.SetInt("ME_interval", MainEventController.instance.GetInterval());
            }
            else 
            {
                NextContainer.instance.NextText = MainEventController.instance.GetNextMe();
                MainEventController.instance.SetME_str(NextContainer.instance.NextText);
            }
        }
        else // �����̺�Ʈ�� �ƴ� ���
        {
            if(gameObject.name[0].Equals('M')) // ���� �̺�Ʈ�� ���
            {
                MainEventController.instance.SetME_str(gameObject.name);
                PlayerPrefs.SetString("ME_str", gameObject.name);
                MainEventController.instance.FirstSetInterval();
            }
            NextContainer.instance.NextText = gameObject.name;
        }
    }
    
}
