using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public GameObject namePanel;
    [SerializeField] private Canvas adminCanvas;
    [SerializeField] private Button adminButton;
    [SerializeField] private TMP_InputField nameInputField;

    [SerializeField] private Text infoText;
    [SerializeField] private GameObject warningText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoMainMenu()
    {
        SceneManager.LoadScene(2);
    }
    public void GoInGame()
    {

        Player.instance.isAdmin = false;
       // StaticCoroutine.is_play = false; //씬 전환시 코루틴 종료

       namePanel = gameObject.transform.GetChild(4).gameObject;
       
        if (PlayerPrefs.GetString("ScriptID") == "")
        {
           
            namePanel.SetActive(true);
          
            Time.timeScale = 0;
        }
        else
        {
            SceneManager.LoadScene(3);
        }
    }

    public void SetNameAndStartGame()
    {
        if (nameInputField.text.Length < 7)
        {
            Player.instance.SetPlayerName(nameInputField.text);
            SceneManager.LoadScene(3);
            Time.timeScale = 1;
        }
        else
        {
            warningText.SetActive(true);
        }
    }

    public void OpenAdminCanvas()
    {
        adminCanvas.gameObject.SetActive(true);
        adminCanvas.GetComponent<Admin>().idPanel.SetActive(true);
    }
}
