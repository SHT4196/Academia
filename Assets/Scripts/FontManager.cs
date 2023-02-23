using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FontManager : MonoBehaviour
{
    public TextMeshProUGUI sampleText;
    public TextMeshProUGUI storyText;

    private int textSize;
    private float lineSpace;
    public Slider TextSizeSlider;
    public Slider LineSpaceSlider;

    public static FontManager instance;


    void Awake()
    {
        if (FontManager.instance == null)
        {
            FontManager.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("text_size"))
        {
            textSize = PlayerPrefs.GetInt("text_size");
            TextSizeSlider.value = PlayerPrefs.GetInt("text_size");
            sampleText.fontSize = textSize;
        }
        else
        {
            textSize = 14;
        }
        if (PlayerPrefs.HasKey("line_space"))
        {
            lineSpace = PlayerPrefs.GetFloat("line_space");
            LineSpaceSlider.value = PlayerPrefs.GetFloat("line_space");
            sampleText.lineSpacing = lineSpace;
        }
        else
        {
            lineSpace = 0f;
        }
        // Text_Size = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 폰트 크기를 크게 설정하는 함수. 누를 때마다 +1pt
    /// </summary>
    public void fontBigger()
    {
        int sizeMax = 18;
        if (textSize < sizeMax)
        {
            textSize ++;
            TextSizeSlider.value ++;
        }
        UpdateTextSize();
    }



    /// <summary>
    /// 폰트 크기를 작게 설정하는 함수. 누를 때마다 -1pt
    /// </summary>    
    public void fontSmaller()
    {
        int sizeMin = 11;
        if (textSize > sizeMin)
        {
            textSize --;
            TextSizeSlider.value --;
        }
        UpdateTextSize();
    }


    /// <summary>
    /// 줄 간격을 넓게 설정하는 함수. 누를 때마다 +10f
    /// </summary>
    public void lineSpaceWider()
    {
        float lineMax = 20f;
        if (lineSpace < lineMax)
        {
            lineSpace += 10f;
            LineSpaceSlider.value += 10;
        }
        UpdateLineSpace();
    }


    /// <summary>
    /// 줄 간격을 좁게 설정하는 함수. 누를 때마다 -10f
    /// </summary>
    public void lineSpaceNarrower()
    {
        float lineMin = -20f;
        if (lineSpace > lineMin)
        {
            lineSpace -= 10f;
            LineSpaceSlider.value -= 10;
        }
        UpdateLineSpace();
    }


    /// <summary>
    /// 줄 간격 설정 값이 변경된 후 본문 텍스트와 샘플 텍스트에 적용시키는 함수
    /// </summary>
    private void UpdateLineSpace()
    {
        LineSpaceSlider.value = lineSpace;
        sampleText.lineSpacing = lineSpace;
        storyText.lineSpacing = lineSpace;
        PlayerPrefs.SetFloat("line_space", lineSpace);
    }


    /// <summary>
    /// 폰트 크기 설정 값이 변경된 후 본문 텍스트와 샘플 텍스트에 적용시키는 함수
    /// </summary>
    private void UpdateTextSize()
    {
        TextSizeSlider.value = textSize;
        sampleText.fontSize = textSize;
        storyText.fontSize = textSize;
        PlayerPrefs.SetInt("text_size", textSize);
    }


    /// <summary>
    /// 폰트 설정 값을 기본값으로 리셋
    /// </summary>
    public void ResetFontSettings()
    {
        //reset text size values
        textSize = 14;
        UpdateTextSize();

        //reset line space values
        lineSpace = 0f;
        UpdateLineSpace();
    }
}