using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Popup : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isBtnDown = false;
    public GameObject Popup_image;
    float timer = 0.0f;
    float waitingTime = 1;

    /// <summary>
    /// info popup button
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        timer += Time.deltaTime;
        if (timer > waitingTime) {
            isBtnDown = false;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        timer += Time.deltaTime;
        if (timer > waitingTime) {
            isBtnDown = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isBtnDown) {
            Popup_image.SetActive(true);
        }
        else
        {
            Popup_image.SetActive(false);
        }
    }
}
