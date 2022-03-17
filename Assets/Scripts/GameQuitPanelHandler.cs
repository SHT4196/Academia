using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameQuitPanelHandler : MonoBehaviour, IPointerDownHandler
{

    public GameObject OptionTrigger;
    public void OnPointerDown(PointerEventData data)
    {
        OptionTrigger.GetComponent<OptionTrigger>().GameQuitPanelClose_Btn();
        Debug.Log("touch");
    }
    
}
