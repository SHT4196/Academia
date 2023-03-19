using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Serialization;
using DG.Tweening;

public class Second : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI startText;
    private GameObject GameStartBtn;
    public GameObject TabtoStart_Btn;

    static private TextMeshProUGUI StartText;
    static Sequence sequenceFadeInOut;


    private void Awake()
    {
        //GameStartBtn = GameObject.Find("TabtoStart_Btn");
        StartText = startText;

        


    }
    // ���� ��ư ����
    void Start()
    {

        sequenceFadeInOut = DOTween.Sequence()
        .SetAutoKill(false) // DoTween Sequence는 기본적으로 일회용임. 재사용하려면 써주자.

        .Append(StartText.DOFade(0f, 1f))
        .Append(StartText.DOFade(1f, 1f))
        .SetLoops(-1, LoopType.Restart);
        
       

        FadeInOut();

    }

    static private void FadeInOut()
    {
        sequenceFadeInOut.Restart(); // Play()로 하면, 한번 밖에 실행 안 됨.
    }


}
