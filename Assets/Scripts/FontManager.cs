using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FontManager : MonoBehaviour
{
    public TextMeshProUGUI Sample_Text;
    public TextMeshProUGUI storyText;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 폰트 크게
    /// </summary>
    public void bigFontSize() 
    {
        Sample_Text.fontSize = 16;
        storyText.fontSize = 16;
    }

    /// <summary>
    ///   폰트 중간
    /// </summary>
    public void middleFontSize()
    {
        Sample_Text.fontSize = 14;
        storyText.fontSize = 14;

    }

    /// <summary>
    /// 폰트 작게
    /// </summary>    

    public void smallFontSize()
    {
        Sample_Text.fontSize = 12;
        storyText.fontSize = 12;

    }

    /// <summary>
    /// 줄간격 넓게
    /// </summary>

    public void wideLineSpace()
    {
        Sample_Text.lineSpacing = 20f;
        storyText.lineSpacing  = 20f;


    }

    /// <summary>
    /// 줄간격 보통
    /// </summary>
    public void middleLineSpace()
    {
        Sample_Text.lineSpacing = 0f;
        storyText.lineSpacing = 0f;


    }

    /// <summary>
    /// 줄간격 좁게
    /// </summary>
    public void narrowLineSpace()
    {
        Sample_Text.lineSpacing = -20f;
        storyText.lineSpacing = -20f;


    }


}
