using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{

    public GameObject TabtoStart_Btn;

    // ���� ��ư ����
    void Start()
    {
        Invoke("GoLoginScene", 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ���� ����
    public void GoLoginScene()
    {
        SceneManager.LoadScene(1);
    }

  

   

}
