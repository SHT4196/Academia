using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [FormerlySerializedAs("NamePanel")] [SerializeField] private GameObject namePanel;
    [FormerlySerializedAs("AdminCanvas")] [SerializeField] private Canvas adminCanvas;
    [SerializeField] private Button adminButton;
    [SerializeField] private TMP_InputField nameInputField;
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
        if (PlayerPrefs.GetString("ScriptID") == "")
        {
            namePanel.SetActive(true);
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
    }

    public void OpenAdminCanvas()
    {
        adminCanvas.gameObject.SetActive(true);
        adminCanvas.GetComponent<Admin>().idPanel.SetActive(true);
    }
}
