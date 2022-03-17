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
    // 폰트 크게
    public void bigFontSize() 
    {
        Sample_Text.fontSize = 80;
        storyText.fontSize = 17;
    }

    // 폰트 중간
    public void middleFontSize()
    {
        Sample_Text.fontSize = 70;
        storyText.fontSize = 15;

    }

    // 폰트 작게
    public void smallFontSize()
    {
        Sample_Text.fontSize = 60;
        storyText.fontSize = 13;

    }

    // 줄간격 넓게
    public void wideLineSpace()
    {
        Sample_Text.lineSpacing = 20f;
        storyText.lineSpacing  = 20f;


    }

    // 줄간격 보통
    public void middleLineSpace()
    {
        Sample_Text.lineSpacing = 0f;
        storyText.lineSpacing = 0f;


    }

    // 줄간격 좁게
    public void narrowLineSpace()
    {
        Sample_Text.lineSpacing = -20f;
        storyText.lineSpacing = -20f;


    }


}
