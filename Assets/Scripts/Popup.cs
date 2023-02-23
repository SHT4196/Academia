using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Popup : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isBtnDown = false;
    private bool isDelayed = false;
    public GameObject Popup_image;
    float timer = 0.0f;
    // float waitingTime = 1;


    /// button up 인식
    public void OnPointerUp(PointerEventData eventData)
    {

        isBtnDown = false;
        Debug.Log(isBtnDown);
        Popup_image.SetActive(false);

    }
    /// button down 인식
    public void OnPointerDown(PointerEventData eventData)
    {

        isBtnDown = true;
        isDelayed = true;
        
    }
    IEnumerator DisplayPopup()
    {
        yield return new WaitForSeconds(0.5f);
        Popup_image.SetActive(true);

    }
    void Update()
    {
        if (isDelayed)
        {
            StartCoroutine(DisplayPopup());
            isDelayed = false;

        }
        else
        {
            if (isBtnDown == false)
            {
                Popup_image.SetActive(false);
            }
        }
    }
}
