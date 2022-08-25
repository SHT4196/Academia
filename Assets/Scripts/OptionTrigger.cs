using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using DG.Tweening;

public class OptionTrigger : MonoBehaviour
{

    public GameObject Option_Canvas;
    public GameObject GameQuit_Canvas;
    public GameObject Achievement_Canvas;
    public GameObject Option_Panel;
    public GameObject Achievement_Panel;

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
