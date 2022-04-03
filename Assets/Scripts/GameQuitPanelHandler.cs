using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameQuitPanelHandler : MonoBehaviour, IPointerDownHandler
{

    public GameObject OptionTrigger;
    /// <summary>
    /// 연결된 객체가 눌릴 때 특정 함수를 출력
    /// </summary>
    /// <param name="data">게임종료 패널 닫기</param>
    public void OnPointerDown(PointerEventData data)
    {
        OptionTrigger.GetComponent<OptionTrigger>().GameQuitPanelClose_Btn();
        Debug.Log("touch");

    }

    
}
