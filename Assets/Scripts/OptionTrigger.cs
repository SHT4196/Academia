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
    /// �ɼ�â ����
    /// </summary>
    public void Option_Btn()
    {
        Option_Canvas.SetActive(true);
        Option_Panel.transform.DOMoveX(Screen.width / 2f, 0.5f);   

    }
    /// <summary>
    /// �ɼ�â �ݱ�
    /// </summary>
    public void OptionClose_Btn()
    {

        Option_Panel.transform.DOMoveX(-Screen.width / 2f, 0.5f).OnComplete(()=>
        {
            Option_Canvas.SetActive(false);
        });

    }
    /// <summary>
    /// ����â ����
    /// </summary>
    public void Achieve_Btn()
    {
        Achievement_Canvas.SetActive(true);
        AchievementManager.Instance.UpdateAchievements();
        Achievement_Panel.transform.DOMoveX(Screen.width / 2f, 0.5f);

    }
    /// <summary>
    /// ����â �ݱ�
    /// </summary>
    public void AchieveClose_Btn()
    {
        
        Achievement_Panel.transform.DOMoveX(Screen.width * 1.5f, 0.5f).OnComplete(() =>
        {
            Achievement_Canvas.SetActive(false);
        });

    }


    /// <summary>
    /// ���� ����â ����
    /// </summary>
    public void GameQuitPanelOpen_Btn()
    {
        GameQuit_Canvas.SetActive(true);
        Time.timeScale = 0;
    }
    /// <summary>
    /// ���� ����â �ݱ�
    /// </summary>
    public void GameQuitPanelClose_Btn()
    {
        GameQuit_Canvas.SetActive(false);
        Time.timeScale = 1;
    }
    /// <summary>
    /// ���� ����
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
            // �ڷΰ���, esc ��ư �Է½� ���� ���� �г� ����
            if (GameQuit_Canvas.activeSelf == false)
            {
                GameQuitPanelOpen_Btn();
                Debug.Log("open");
            }

            // �̹� �����ִٸ� �ڷΰ���, esc ��ư���� ���� ���� �г� �ݱ�
            else
            {
                GameQuitPanelClose_Btn();
                Debug.Log("close");
            }
        }
    }

}
