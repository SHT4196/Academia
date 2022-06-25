using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FontManager : MonoBehaviour
{
    public TextMeshProUGUI Sample_Text;
    public TextMeshProUGUI storyText;
    public TextMeshProUGUI Text_Size;
    public TextMeshProUGUI Line_Space;
    // public TextMeshProUGUI OptionClose_Text;
    private int textSize;
    // public Text sizeBtn;
    private float lineSpace;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("text_size"))
        {
            textSize = PlayerPrefs.GetInt("text_size");
            Sample_Text.fontSize = textSize;
            Text_Size.text = textSize.ToString();
        }
        else
        {
            textSize = 14;
            Text_Size.text = textSize.ToString();
        }
        if (PlayerPrefs.HasKey("line_space"))
        {
            lineSpace = PlayerPrefs.GetFloat("line_space");
            Sample_Text.lineSpacing = lineSpace;
            Line_Space.text = lineSpace.ToString();
        }
        else
        {
            lineSpace = 0f;
            Line_Space.text = lineSpace.ToString();
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
        }
        Sample_Text.fontSize = textSize;
        storyText.fontSize = textSize;
        Text_Size.text = textSize.ToString();
        PlayerPrefs.SetInt("text_size", textSize);
        Debug.Log("+1");
        // Debug.Log("complete");
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
        if (textSize > sizeMin) {
            textSize --;
        }
        Sample_Text.fontSize = textSize;
        storyText.fontSize = textSize;
        Text_Size.text = textSize.ToString();
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
        }
        Sample_Text.lineSpacing = lineSpace;
        storyText.lineSpacing = lineSpace;
        Line_Space.text = lineSpace.ToString();
        PlayerPrefs.SetFloat("line_space", lineSpace);
        Debug.Log("line space +1");
        // Debug.Log("complete");
    }

    /// <summary>
    /// �ٰ��� ����
    /// </summary>
    // public void middleLineSpace()
    // {
    //     Sample_Text.lineSpacing = 0f;
    //     storyText.lineSpacing = 0f;
    // }

    /// <summary>
    /// �ٰ��� ����
    /// </summary>
    // public void narrowLineSpace()
    // {
    //     Sample_Text.lineSpacing = -20f;
    //     storyText.lineSpacing = -20f;
    // }
// }
    public void lineSpaceNarrower()
    {
        float lineMin = -20f;
        if (lineSpace > lineMin) {
            lineSpace -= 10f;
        }
        Sample_Text.lineSpacing = lineSpace;
        storyText.lineSpacing = lineSpace;
        Line_Space.text = lineSpace.ToString();
        PlayerPrefs.SetFloat("line_space", lineSpace);
        Debug.Log("line space -1");

    }
}