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
        Invoke("Start_Btn", 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ���� ����
    public void GoMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void Start_Btn()
    {
        TabtoStart_Btn.SetActive(true);

    }

}
