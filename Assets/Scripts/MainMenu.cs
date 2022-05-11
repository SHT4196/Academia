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
        StaticCoroutine.is_play = false; //씬 전환시 코루틴 종료
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
}
