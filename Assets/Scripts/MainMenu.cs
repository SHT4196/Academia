using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public GameObject namePanel;
    [SerializeField] public GameObject resetPanel;
    [SerializeField] public GameObject noDataPanel;
    [SerializeField] private Canvas adminCanvas;
    [SerializeField] private Button adminButton;
    [SerializeField] private TMP_InputField nameInputField;
   

    [SerializeField] private Text infoText;
    [SerializeField] private GameObject warningText;
    public GameObject Second;


    // Start is called before the first frame update

    /// <summary>
    /// 튜토리얼 판단 변수 조절
    /// </summary>
    void Awake()
    {
       
    }

    public void GoLoginScene()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void GoMainMenu()
    {
        SceneManager.LoadSceneAsync(3);

    }
   
    /// <summary>
    ///  튜토리얼 자동 실행
    /// </summary>

    public void NewGame()
    {

        Player.instance.isAdmin = false;
       // StaticCoroutine.is_play = false; //씬 전환시 코루틴 종료

       namePanel = gameObject.transform.GetChild(2).gameObject;
       resetPanel = gameObject.transform.GetChild(3).gameObject;

        if (PlayerPrefs.GetString("ScriptID") == "")
        {
           
            namePanel.SetActive(true);
          
            Time.timeScale = 0;
        }
        else
        {
            resetPanel.SetActive(true);

            Time.timeScale = 0;
        }
    }

    public void LoadGame()
    {

        Player.instance.isAdmin = false;
        // StaticCoroutine.is_play = false; //씬 전환시 코루틴 종료

        resetPanel = gameObject.transform.GetChild(3).gameObject;
        noDataPanel = gameObject.transform.GetChild(4).gameObject;

        if (PlayerPrefs.GetString("ScriptID") == "")
        {

            noDataPanel.SetActive(true);

            Time.timeScale = 0;
        }
        else
        {
            SceneManager.LoadSceneAsync(4);
        }
    }
    public void Reset()
    {
        
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Tutorial", 1);

    }

    public void SetNameAndStartGame()
    {
        if ((nameInputField.text.Length < 7) && (nameInputField.text.Length > 0))
        {
            Player.instance.SetPlayerName(nameInputField.text);
            SceneManager.LoadSceneAsync(4);
            Time.timeScale = 1;
        }
        else
        {
            warningText.SetActive(true);
        }
    }

    public void NamePanelOpen()
    {
        namePanel = gameObject.transform.GetChild(2).gameObject;
        namePanel.SetActive(true);
        Time.timeScale = 0;

    }

    public void NamePanelClose()
    {
        namePanel = gameObject.transform.GetChild(2).gameObject;
        namePanel.SetActive(false);
        Time.timeScale = 1;

    }

    public void ResetPanelOpen()
    {
        resetPanel = gameObject.transform.GetChild(3).gameObject;
        resetPanel.SetActive(true);
        Time.timeScale = 0;

    }


    public void ResetPanelClose()
    {
        resetPanel = gameObject.transform.GetChild(3).gameObject;
        resetPanel.SetActive(false);
        Time.timeScale = 1;

    }

    public void NoDatePanelClose()
    {
        noDataPanel = gameObject.transform.GetChild(4).gameObject;
        noDataPanel.SetActive(false);
        Time.timeScale = 1;

    }

    public void OpenAdminCanvas()
    {
        adminCanvas.gameObject.SetActive(true);
        adminCanvas.GetComponent<Admin>().idPanel.SetActive(true);
    }

    
}
