using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{

    public GameObject TabtoStart_Btn;

    // 시작 버튼 지연
    void Start()
    {
        Invoke("Start_Btn", 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 게임 시작
    public void GoMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void Start_Btn()
    {
        TabtoStart_Btn.SetActive(true);

    }

}
