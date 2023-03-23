using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameQuitPanelHandler : MonoBehaviour, IPointerDownHandler
{

    public GameObject OptionTrigger;
   
    /// <summary>
    /// ����� ��ü�� ���� �� Ư�� �Լ��� ���
    /// </summary>
    /// <param name="quitpanelclose">�������� �г� �ݱ�</param>
    public void OnPointerDown(PointerEventData quitpanelclose)
    {
        OptionTrigger.GetComponent<OptionTrigger>().GameQuitPanelClose_Btn();
     
        Debug.Log("touch");

    }

    
}
