using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Serialization;
using DG.Tweening;


public class GameStart : MonoBehaviour
{
    [SerializeField] private Image logoImg;
    [SerializeField] private TextMeshProUGUI logoTxt;
    private GameObject logo;

    public GameObject TabtoStart_Btn;

    private void Awake()
    {
        logo = GameObject.Find("logo");
        logoTxt = logo.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        logoImg = logo.transform.GetChild(1).GetComponent<Image>();
    }
    // ���� ��ư ����
    void Start()
    {
        logoImg.DOFade(1f, 1.8f);
        logoTxt.DOFade(1f, 1.8f);
        Invoke("GoSecondScene", 2.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ���� ����
    public void GoSecondScene()
    {
        SceneManager.LoadScene(1);
    }

  

   

}
