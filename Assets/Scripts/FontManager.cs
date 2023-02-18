using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FontManager : MonoBehaviour
{
    public TextMeshProUGUI Sample_Text;
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
            Sample_Text.fontSize = textSize;
        }
        else
        {
            textSize = 14;
        }
        if (PlayerPrefs.HasKey("line_space"))
        {
            lineSpace = PlayerPrefs.GetFloat("line_space");
            LineSpaceSlider.value = PlayerPrefs.GetFloat("line_space");
            Sample_Text.lineSpacing = lineSpace;
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
    /// ��Ʈ ũ��
    /// </summary>


    public void fontBigger()
    {
        int sizeMax = 18;
        if (textSize < sizeMax)
        {
            textSize ++;
            TextSizeSlider.value ++;
        }
        Sample_Text.fontSize = textSize;
        storyText.fontSize = textSize;
        // TextSizeSlider.value = textSize;
        PlayerPrefs.SetInt("text_size", textSize);
        Debug.Log("+1");
    }

    /// <summary>
    ///   ��Ʈ �߰�
    /// </summary>


    /// <summary>
    /// ��Ʈ �۰�
    /// </summary>    

    public void fontSmaller()
    {
        int sizeMin = 11;
        if (textSize > sizeMin)
        {
            textSize --;
            TextSizeSlider.value --;
        }
        Sample_Text.fontSize = textSize;
        storyText.fontSize = textSize;
        PlayerPrefs.SetInt("text_size", textSize);
        Debug.Log("-1");

    }

    /// <summary>
    /// �ٰ��� �а�
    /// </summary>

    // public void wideLineSpace()
    // {
    //     Sample_Text.lineSpacing = 20f;
    //     storyText.lineSpacing  = 20f;
    // }

    public void lineSpaceWider()
    {
        float lineMax = 20f;
        if (lineSpace < lineMax)
        {
            lineSpace += 10f;
            LineSpaceSlider.value += 10;
        }
        Sample_Text.lineSpacing = lineSpace;
        storyText.lineSpacing = lineSpace;
        // Line_Space.text = lineSpace.ToString();
        PlayerPrefs.SetFloat("line_space", lineSpace);
        Debug.Log("line space +1");
        // Debug.Log("complete");
    }

    public void lineSpaceNarrower()
    {
        float lineMin = -20f;
        if (lineSpace > lineMin)
        {
            lineSpace -= 10f;
            LineSpaceSlider.value -= 10;
        }
        Sample_Text.lineSpacing = lineSpace;
        storyText.lineSpacing = lineSpace;
        // Line_Space.text = lineSpace.ToString();
        PlayerPrefs.SetFloat("line_space", lineSpace);
        Debug.Log("line space -1");

    }

    public void ResetFontSettings()
    {
        //reset text size values
        textSize = 14;

        //reset line space values
        lineSpace = 0f;
    }
}