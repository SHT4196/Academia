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
    /// <param name="data">�������� �г� �ݱ�</param>
    public void OnPointerDown(PointerEventData data)
    {
        OptionTrigger.GetComponent<OptionTrigger>().GameQuitPanelClose_Btn();
        Debug.Log("touch");

    }

    
}
