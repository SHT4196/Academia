using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionTrigger : MonoBehaviour
{

    public GameObject Option_Canvas;

    public void Option_Btn()
    {
        Option_Canvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void OptionClose_Btn()
    {
        Option_Canvas.SetActive(false);
        Time.timeScale = 1;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
