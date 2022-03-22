using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class OptionTrigger : MonoBehaviour
{

    public GameObject Option_Canvas;
    public GameObject GameQuit_Canvas;

    public void Option_Btn()
    {
        Option_Canvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void OptionClose_Btn()
    {
        Option_Canvas.SetActive(false);
        Time.timeScale = 1;

    }

    public void GameQuitPanel_Btn()
    {
        GameQuit_Canvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void GameQuitPanelClose_Btn()
    {
        GameQuit_Canvas.SetActive(false);
        Time.timeScale = 1;
    }

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
            if (GameQuit_Canvas.activeSelf == false)
            {
                GameQuitPanel_Btn();
                Debug.Log("open");
            }
            else
            {
                GameQuitPanelClose_Btn();
                Debug.Log("close");
            }
        }

        
    }

}
