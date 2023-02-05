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
        Invoke("GoLoginScene", 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 게임 시작
    public void GoLoginScene()
    {
        SceneManager.LoadScene(1);
    }

  

   

}
