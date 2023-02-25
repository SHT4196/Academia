using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using DG.Tweening;

public class OptionTrigger : MonoBehaviour
{

    public GameObject Option_Canvas;
    public GameObject Setting_Canvas;
    public GameObject Credit_Canvas;
    public GameObject Reset_Canvas;
    public GameObject GameQuit_Canvas;
    public GameObject Achievement_Canvas;
    public GameObject Option_Panel;
    public GameObject Achievement_Panel;
    public GameObject Like_Canvas;
    

    /// <summary>
    /// 옵션창 열기
    /// </summary>
    public void Option_Btn()
    {
        Option_Canvas.SetActive(true);
        Option_Panel.transform.DOMoveX(Screen.width / 2f, 0.5f);   

    }
    /// <summary>
    /// 옵션창 닫기
    /// </summary>
    public void OptionClose_Btn()
    {

        Option_Panel.transform.DOMoveX(-Screen.width / 2f, 0.5f).OnComplete(()=>
        {
            Option_Canvas.SetActive(false);
        });

    }

    /// <summary>
    /// 셋팅창 열기
    /// </summary>
    public void Setting_Btn()
    {
        Setting_Canvas.SetActive(true);
    }
    /// <summary>
    /// 세팅창 닫기
    /// </summary>
    public void SettingClose_Btn()
    {
        Setting_Canvas.SetActive(false);
    }

    /// <summary>
    /// 폰트 세팅값, 사운드 세팅값 리셋하는 함수(초기화 버튼에 붙임)
    /// </summary>
    public void ResetSetting_Btn()
    {
        FontManager.instance.ResetFontSettings();
        SoundManger.instance.ResetSoundSettings();
    }
    /// <summary>
    /// 업적창 열기
    /// </summary>
    public void Achieve_Btn()
    {
        Achievement_Canvas.SetActive(true);
        AchievementManager.Instance.UpdateAchievements();
        Achievement_Panel.transform.DOMoveX(Screen.width / 2f, 0.5f);
    }
    /// <summary>
    /// 업적창 닫기
    /// </summary>
    public void AchieveClose_Btn()
    {
        Achievement_Panel.transform.DOMoveX(Screen.width * 1.5f, 0.5f).OnComplete(() =>
        {
            Achievement_Canvas.SetActive(false);
        });
    }

    /// <summary>
    /// 셋팅창 열기
    /// </summary>
    public void Credit_Btn()
    {
        Credit_Canvas.SetActive(true);
    }
    /// <summary>
    /// 세팅창 닫기
    /// </summary>
    public void CreditClose_Btn()
    {
        Credit_Canvas.SetActive(false);
    }

    /// <summary>
    /// 게임 종료창 열기
    /// </summary>
    public void GameQuitPanelOpen_Btn()
    {
        GameQuit_Canvas.SetActive(true);
        Time.timeScale = 0;
    }
    /// <summary>
    /// 게임 종료창 열기
    /// </summary>
    public void GameQuitPanelClose_Btn()
    {
        GameQuit_Canvas.SetActive(false);
        Time.timeScale = 1;
    }
    /// <summary>
    /// 게임 종료
    /// </summary>
    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
    /// <summary>
    /// 호감도 창 열기
    /// </summary>
    public void LikeCanvasOpen_Btn()
    {
        Like_Canvas.SetActive(true);
    }
    /// <summary>
    /// 호감도 창 닫기
    /// </summary>
    public void LikeCanvasClose_Btn()
    {
        Like_Canvas.SetActive(false);
    }

    /// <summary>
    /// 인게임 리셋 창 열기
    /// </summary>
    public void ResetCanvasOpen_Btn()
    {
        Reset_Canvas.SetActive(true);
    }
    /// <summary>
    /// 인게임 리셋 창 닫기
    /// </summary>
    public void ResetCanvasClose_Btn()
    {
        Reset_Canvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 게임종료창이 닫혀 있을때 뒤로가기
            if (GameQuit_Canvas.activeSelf == false)
            {
                GameQuitPanelOpen_Btn();
                Debug.Log("open");
            }

            // 게임 종료창이 열려 있을때 뒤로가기
            else
            {
                GameQuitPanelClose_Btn();
                Debug.Log("close");
            }
        }
    }

}
