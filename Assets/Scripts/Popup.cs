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
    float waitingTime = 1;


    /// button up 인식
    public void OnPointerUp(PointerEventData eventData)
    {
<<<<<<< HEAD
        timer += Time.deltaTime;
        if (timer > waitingTime) {
            isBtnDown = false;
        }
=======
        isBtnDown = false;
        Debug.Log(isBtnDown);
        Popup_image.SetActive(false);
>>>>>>> main
    }
    /// button down 인식
    public void OnPointerDown(PointerEventData eventData)
    {
<<<<<<< HEAD
        timer += Time.deltaTime;
        if (timer > waitingTime) {
            isBtnDown = true;
        }
=======
        isBtnDown = true;
        isDelayed = true;
        
    }
    IEnumerator DisplayPopup()
    {
        yield return new WaitForSeconds(1.0f);
        Popup_image.SetActive(true);
>>>>>>> main
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
