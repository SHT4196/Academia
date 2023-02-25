using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] public GameObject tutorialPanel;
    private int _tutorial;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("AutoTutorial", 1f);
    }

    /// <summary>
    /// 튜토리얼 판단 변수 조절
    /// </summary>
    void Awake()
    {
        _tutorial = PlayerPrefs.GetInt("Tutorial") == 0 ? 0 : PlayerPrefs.GetInt("Tutorial");
    }


    /// <summary>
    ///  튜토리얼 자동 실행
    /// </summary>
    public void AutoTutorial()
    {
        // 튜토리얼을 실행한 적 없다면
        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
            TutorialPanelOpen();
            Debug.Log("튜토리얼 시작");
            PlayerPrefs.SetInt("Tutorial", 1);
            PlayerPrefs.Save();
            

        }

        else if (PlayerPrefs.GetInt("Tutorial") != 0)
        {
            Debug.Log("튜토리얼 했음");
            return;
        }


    }

    public void TutorialPanelOpen()
    {
        
        tutorialPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void TutorialPanelClose()
    {
        
        tutorialPanel.SetActive(false);
        Time.timeScale = 1;

    }

}
