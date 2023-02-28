using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] public GameObject tutorialPanel;
    public GameObject[] tutorialImage;
    private int _tutorial;
    private int tutorialIndex;



    // Start is called before the first frame update
    void Start()
    {
        Invoke("AutoTutorial", 1f);
        tutorialIndex = 0;
    }

    /// <summary>
    /// Ʃ�丮�� �Ǵ� ���� ����
    /// </summary>
    void Awake()
    {
        _tutorial = PlayerPrefs.GetInt("Tutorial") == 0 ? 0 : PlayerPrefs.GetInt("Tutorial");
    }


    /// <summary>
    ///  Ʃ�丮�� �ڵ� ����
    /// </summary>
    public void AutoTutorial()
    {
        // Ʃ�丮���� ������ �� ���ٸ�
        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
            TutorialPanelOpen();
            Debug.Log("Ʃ�丮�� ����");
            PlayerPrefs.SetInt("Tutorial", 1);
            PlayerPrefs.Save();
            

        }

        else if (PlayerPrefs.GetInt("Tutorial") != 0)
        {
            Debug.Log("Ʃ�丮�� ����");
            return;
        }


    }

    public void TutorialPanelOpen()
    {
        
        tutorialPanel.gameObject.SetActive(true);
        tutorialImage[0].gameObject.SetActive(true);
        
    }

    public void TutorialPanelClose()
    {
        
        tutorialPanel.SetActive(false);
       

    }

    public void Next() 
    {
        if (tutorialIndex < 12) 
        {
            tutorialIndex += 1;
            for (int i = 0; i < tutorialImage.Length; i++)
            {
                tutorialImage[i].gameObject.SetActive(false);
                tutorialImage[tutorialIndex].gameObject.SetActive(true);
            }
        }
        if (tutorialIndex >= 12)
        {
            TutorialPanelClose();
            tutorialIndex = 0;
        }
    }

}
