using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [FormerlySerializedAs("NamePanel")] [SerializeField] private Image namePanel;
    [FormerlySerializedAs("AdminCanvas")] [SerializeField] private Canvas adminCanvas;
    [SerializeField] private Button adminButton;
    [SerializeField] private TMP_InputField nameInputField;

    [SerializeField] private Text infoText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoInGame()
    {

        Player.instance.isAdmin = false;
       // StaticCoroutine.is_play = false; //씬 전환시 코루틴 종료

        if (PlayerPrefs.GetString("ScriptID") == "")
        {
            infoText.text += $"\n{namePanel.gameObject.activeSelf}";
            // namePanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    public void SetNameAndStartGame()
    {
        Player.instance.SetPlayerName(nameInputField.text);
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }

    public void OpenAdminCanvas()
    {
        adminCanvas.gameObject.SetActive(true);
        adminCanvas.GetComponent<Admin>().idPanel.SetActive(true);
    }
}
