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
    // public TextMeshProUGUI OptionClose_Text;
    private int textSize;
    // public Text sizeBtn;


    // Start is called before the first frame update
    void Start()
    {
        textSize = 14;
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
        Debug.Log("-1");

    }

    /// <summary>
    /// �ٰ��� �а�
    /// </summary>

    public void wideLineSpace()
    {
        Sample_Text.lineSpacing = 20f;
        storyText.lineSpacing  = 20f;
    }

    /// <summary>
    /// �ٰ��� ����
    /// </summary>
    public void middleLineSpace()
    {
        Sample_Text.lineSpacing = 0f;
        storyText.lineSpacing = 0f;
    }

    /// <summary>
    /// �ٰ��� ����
    /// </summary>
    public void narrowLineSpace()
    {
        Sample_Text.lineSpacing = -20f;
        storyText.lineSpacing = -20f;
    }


}
